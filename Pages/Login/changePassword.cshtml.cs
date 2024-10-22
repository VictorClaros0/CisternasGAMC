using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CisternasGAMC.Data;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace CisternasGAMC.Pages.Login
{
    public class ChangePasswordModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ChangePasswordModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PasswordChangeModel ChangePassword { get; set; }

        public class PasswordChangeModel
        {
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
            public string ConfirmNewPassword { get; set; }
        }
        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "El formulario no es válido.";
                return RedirectToPage("/Admin/index"); // O a la página que estés manejando.
            }

            var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null || !BCrypt.Net.BCrypt.Verify(ChangePassword.OldPassword, user.Password))
            {
                TempData["Message"] = "La contraseña antigua es incorrecta.";
                TempData["ErrorField"] = "OldPassword"; // Indicamos cuál campo tiene el error.
                return RedirectToPage("/Admin/index"); // O a la página que estés manejando.
            }

            if (ChangePassword.NewPassword != ChangePassword.ConfirmNewPassword)
            {
                TempData["Message"] = "Las nuevas contraseñas no coinciden.";
                TempData["ErrorField"] = "NewPassword"; // Indicamos cuál campo tiene el error.
                return RedirectToPage("/Admin/index"); // O a la página que estés manejando.
            }

            // Encriptamos y guardamos la nueva contraseña
            user.Password = BCrypt.Net.BCrypt.HashPassword(ChangePassword.NewPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // Cerramos la sesión después de cambiar la contraseña.
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["Message"] = "Contraseña cambiada con éxito.";
            return RedirectToPage("/Admin/index"); // O a la página que estés manejando.
        }



    }
}
