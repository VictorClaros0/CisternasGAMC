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
            otbs = await _context.Otbs.ToListAsync(); // Asegúrate de cargar los OTBs también
        }

        public IActionResult OnGetIndex(int waterDeliveryId)
        {
            // Aquí puedes hacer lo que necesites con waterDeliveryId,
            // como cargar datos relacionados o redirigir a otra página
            // Por ejemplo, podrías usarlo para cargar información específica

            // Redirigir a la página Index
            return RedirectToPage("Driver/Index" /*, new { waterDeliveryId }*/);
        }
    }
}
