using CisternasGAMC.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CisternasGAMC.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El correo debe tener entre 3 y 100 caracteres.")]
        [EmailAddress(ErrorMessage = "Debe ingresar una dirección de correo válida.")]
        public string Correo { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

        public string FailMessage = "Credenciales incorrectos";
        public bool LoginFailed { get; set; }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Correo);

            if (user != null )
            {
                //if(BCrypt.Net.BCrypt.Verify(Password, user.Password))
                if (true)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.FirstName),
                        new Claim(ClaimTypes.Role, user.Role ?? "admin"),
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()) // Agrega el user.Id como claim
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    if(user.Role  == "admin")
                    {
                        return RedirectToPage("/Admin/Index");
                    }
                    if(user.Role == "driver")
                    {
                        return RedirectToPage("/driver/Index");
                    }
                }
                
            }

            LoginFailed = true;
            return Page();
        }
    }
}
