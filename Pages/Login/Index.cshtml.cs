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
        [Required(ErrorMessage = "El correo electr�nico es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El correo debe tener entre 3 y 100 caracteres.")]
        [EmailAddress(ErrorMessage = "Debe ingresar una direcci�n de correo v�lida.")]
        public string Correo { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "La contrase�a es obligatoria.")]
        public string Password { get; set; }

        public bool LoginFailed { get; set; }

        // M�todo GET para la p�gina de login
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        // M�todo POST para login
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Correo);

            if (user != null)
            {
                // En este punto podr�as implementar la verificaci�n de la contrase�a real
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

                    // Redirigir seg�n el rol
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

        // M�todo POST para el restablecimiento de contrase�a
        public async Task<IActionResult> OnPostSendPasswordResetAsync(string emailReset)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == emailReset);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Correo electr�nico no encontrado.");
                return Page();
            }

            // Generar nueva contrase�a
            string nuevaContrase�a = GenerarContrase�aAleatoria(10);

            // Actualizar la contrase�a (puedes agregar hashing aqu� si es necesario)
            user.Password = BCrypt.Net.BCrypt.HashPassword(nuevaContrase�a);
            await _context.SaveChangesAsync();

            // Enviar la nueva contrase�a por correo
            await EnviarCorreoNuevaContrase�a(user.Email, nuevaContrase�a);

            return RedirectToPage("/Login/Index", new { successMessage = "Se ha enviado una nueva contrase�a a tu correo electr�nico." });
        }

        // M�todo para generar una contrase�a aleatoria
        private string GenerarContrase�aAleatoria(int length)
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

        // M�todo para enviar la nueva contrase�a por correo
        private async Task EnviarCorreoNuevaContrase�a(string email, string nuevaContrase�a)
        {
            var fromAddress = new MailAddress("davidachagan@gmail.com", "CisternasGAMC");
            var toAddress = new MailAddress(email);
            const string fromPassword = "mdkj ogbv kwjm ztjq"; // Contrase�a de aplicaci�n de Gmail

            const string subject = "Tu nueva contrase�a";
            string body = $@"
    <div style='font-family: Arial, sans-serif; color: #333;'>
        <h2 style='color: #4CAF50;'>Tu nueva contrase�a ha sido generada</h2>
        <p>Hola,</p>
        <p>Nos complace informarte que tu nueva contrase�a ha sido creada con �xito:</p>
        <div style='background-color: #f2f2f2; padding: 10px; margin: 10px 0; border-radius: 5px;'>
            <p style='font-size: 18px; color: #333; text-align: center;'><b>{nuevaContrase�a}</b></p>
        </div>
        <p>Por favor, <a href='#' style='color: #4CAF50;'>inicia sesi�n</a> y aseg�rate de cambiarla por una que recuerdes.</p>
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
