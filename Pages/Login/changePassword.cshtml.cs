using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CisternasGAMC.Data;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace CisternasGAMC.Pages.Login
{
    [Authorize(Roles = "admin")]
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
            [Required(ErrorMessage = "La contrase�a antigua es obligatoria.")]
            public string OldPassword { get; set; }

            [Required(ErrorMessage = "La nueva contrase�a es obligatoria.")]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "La nueva contrase�a debe tener al menos 6 caracteres.")]
            [RegularExpression(@"^(?=.*[0-9]).+$", ErrorMessage = "La nueva contrase�a debe contener al menos un n�mero.")]
            public string NewPassword { get; set; }

            [Required(ErrorMessage = "La confirmaci�n de la nueva contrase�a es obligatoria.")]
            [Compare("NewPassword", ErrorMessage = "La confirmaci�n no coincide con la nueva contrase�a.")]
            public string ConfirmNewPassword { get; set; }
        }

        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "El formulario no es v�lido.";
                return Page();
            }

            var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null || !BCrypt.Net.BCrypt.Verify(ChangePassword.OldPassword, user.Password))
            {
                ModelState.AddModelError("ChangePassword.OldPassword", "La contrase�a antigua es incorrecta.");
                return Page();
            }

            if (ChangePassword.NewPassword != ChangePassword.ConfirmNewPassword)
            {
                ModelState.AddModelError("ChangePassword.ConfirmNewPassword", "Las nuevas contrase�as no coinciden.");
                return Page();
            }

            // Encriptamos y guardamos la nueva contrase�a
            user.Password = BCrypt.Net.BCrypt.HashPassword(ChangePassword.NewPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // Cerramos la sesi�n despu�s de cambiar la contrase�a.
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["Message"] = "Contrase�a cambiada con �xito.";
            return RedirectToPage("/Index");
        }
    }
}
