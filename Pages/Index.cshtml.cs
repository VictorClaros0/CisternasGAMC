using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CisternasGAMC.Pages
{
    public class IndexModel : PageModel
    {
        public string UserRole { get; private set; }
        public bool IsAuthenticated { get; private set; }
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var user = User;
            IsAuthenticated = user.Identity.IsAuthenticated;

            if (IsAuthenticated)
            {
                // Obtener el rol del usuario a partir de los claims
                UserRole = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if(UserRole == "admin")
                {
                    RedirectToPage("Admin/Index");
                }
                if(UserRole == "driver")
                {
                    RedirectToPage("Admin/Index");
                }
            }
        }
    }
}
