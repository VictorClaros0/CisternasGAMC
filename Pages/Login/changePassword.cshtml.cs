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
                TempData["Message"] = "El formulario no es v�lido.";
                return RedirectToPage("/Admin/index"); // O a la p�gina que est�s manejando.
            }

            var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null || !BCrypt.Net.BCrypt.Verify(ChangePassword.OldPassword, user.Password))
            {
                TempData["Message"] = "La contrase�a antigua es incorrecta.";
                TempData["ErrorField"] = "OldPassword"; // Indicamos cu�l campo tiene el error.
                return RedirectToPage("/Admin/index"); // O a la p�gina que est�s manejando.
            }

            if (ChangePassword.NewPassword != ChangePassword.ConfirmNewPassword)
            {
                TempData["Message"] = "Las nuevas contrase�as no coinciden.";
                TempData["ErrorField"] = "NewPassword"; // Indicamos cu�l campo tiene el error.
                return RedirectToPage("/Admin/index"); // O a la p�gina que est�s manejando.
            }

            // Encriptamos y guardamos la nueva contrase�a
            user.Password = BCrypt.Net.BCrypt.HashPassword(ChangePassword.NewPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // Cerramos la sesi�n despu�s de cambiar la contrase�a.
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["Message"] = "Contrase�a cambiada con �xito.";
            return RedirectToPage("/Admin/index"); // O a la p�gina que est�s manejando.
        }



    }
}
