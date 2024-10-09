using CisternasGAMC.Data;
using CisternasGAMC.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CisternasGAMC.Pages.Driver
{
    public class seleccionViewModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public seleccionViewModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<WaterDelivery> waterDeliveries { get; set; } = default!;
        public IList<Otb> otbs { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Cargar las entregas de agua y los OTBs desde la base de datos
            waterDeliveries = await _context.WaterDeliveries.ToListAsync();
            otbs = await _context.Otbs.ToListAsync(); // Aseg�rate de cargar los OTBs tambi�n
        }

        public IActionResult OnGetIndex(int waterDeliveryId)
        {
            // Aqu� puedes hacer lo que necesites con waterDeliveryId,
            // como cargar datos relacionados o redirigir a otra p�gina
            // Por ejemplo, podr�as usarlo para cargar informaci�n espec�fica

            // Redirigir a la p�gina Index
            return RedirectToPage("Driver/Index" /*, new { waterDeliveryId }*/);
        }
    }
}
