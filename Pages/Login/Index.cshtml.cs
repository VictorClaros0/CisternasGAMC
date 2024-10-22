using CisternasGAMC.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CisternasGAMC.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Propiedades de entrada
        [BindProperty]
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El correo debe tener entre 3 y 100 caracteres.")]
        [EmailAddress(ErrorMessage = "Debe ingresar una dirección de correo válida.")]
        public string Correo { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Password { get; set; }

        public bool LoginFailed { get; set; }

        // Método GET para la página de login
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        // Método POST para login
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Correo);

            if (user != null)
            {
                // En este punto podrías implementar la verificación de la contraseña real
                if (BCrypt.Net.BCrypt.Verify(Password, user.Password))
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                    new Claim(ClaimTypes.Role, user.Role ?? "admin"),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    // Redirigir según el rol
                    if (user.Role == "admin")
                    {
                        return RedirectToPage("/Admin/Index");
                    }
                    else if (user.Role == "driver")
                    {
                        return RedirectToPage("/driver/Index");
                    }
                }

            }

            LoginFailed = true;
            return Page();
        }

        // Método POST para el restablecimiento de contraseña
        public async Task<IActionResult> OnPostSendPasswordResetAsync(string emailReset)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == emailReset);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Correo electrónico no encontrado.");
                return Page();
            }

            // Generar nueva contraseña
            string nuevaContraseña = GenerarContraseñaAleatoria(10);

            // Actualizar la contraseña (puedes agregar hashing aquí si es necesario)
            user.Password = BCrypt.Net.BCrypt.HashPassword(nuevaContraseña);
            await _context.SaveChangesAsync();

            // Enviar la nueva contraseña por correo
            await EnviarCorreoNuevaContraseña(user.Email, nuevaContraseña);

            return RedirectToPage("/Login/Index", new { successMessage = "Se ha enviado una nueva contraseña a tu correo electrónico." });
        }

        // Método para generar una contraseña aleatoria
        private string GenerarContraseñaAleatoria(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder result = new StringBuilder();
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    result.Append(validChars[(int)(num % (uint)validChars.Length)]);
                }
            }
            return result.ToString();
        }

        // Método para enviar la nueva contraseña por correo
        private async Task EnviarCorreoNuevaContraseña(string email, string nuevaContraseña)
        {
            var fromAddress = new MailAddress("davidachagan@gmail.com", "CisternasGAMC");
            var toAddress = new MailAddress(email);
            const string fromPassword = "mdkj ogbv kwjm ztjq"; // Contraseña de aplicación de Gmail

            const string subject = "Tu nueva contraseña";
            string body = $@"
    <div style='font-family: Arial, sans-serif; color: #333;'>
        <h2 style='color: #4CAF50;'>Tu nueva contraseña ha sido generada</h2>
        <p>Hola,</p>
        <p>Nos complace informarte que tu nueva contraseña ha sido creada con éxito:</p>
        <div style='background-color: #f2f2f2; padding: 10px; margin: 10px 0; border-radius: 5px;'>
            <p style='font-size: 18px; color: #333; text-align: center;'><b>{nuevaContraseña}</b></p>
        </div>
        <p>Por favor, <a href='#' style='color: #4CAF50;'>inicia sesión</a> y asegúrate de cambiarla por una que recuerdes.</p>
        <p>Si no solicitaste este cambio, por favor ponte en contacto con nuestro equipo de soporte inmediatamente.</p>
        <br>
        <p>Gracias por confiar en nosotros.</p>
        <p><strong>Equipo de CisternasGAMC</strong></p>
    </div>";

            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    await smtp.SendMailAsync(message);
                }
            }
        }

    }
}
