using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using CisternasGAMC.Data;
using CisternasGAMC.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace CisternasGAMC.Pages.Driver
{
    public class DeliverModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // Propiedad para almacenar el ID de la entrega de agua
        public int? WaterDeliveryId { get; private set; }

        // Modelo de la entrega de agua vinculado con el formulario
        [BindProperty]
        public WaterDelivery WaterDelivery { get; set; }

        // Constructor que recibe el contexto de la base de datos
        public DeliverModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // M�todo para cargar los detalles de la entrega de agua en la p�gina de inicio
        public async Task<IActionResult> OnGetAsync(int? waterDeliveryId)
        {
            if (waterDeliveryId == null)
            {
                return NotFound();
            }

            WaterDeliveryId = waterDeliveryId;
            await LoadWaterDeliveryAsync();

            if (WaterDelivery == null)
            {
                return NotFound();
            }

            return Page();
        }

        // M�todo para registrar la llegada a la OTB
        public async Task<IActionResult> OnPostSetArrivalAsync(int waterDeliveryId)
        {
            var delivery = await _context.WaterDeliveries.FirstOrDefaultAsync(d => d.WaterDeliveryId == waterDeliveryId);

            if (delivery == null)
            {
                return NotFound("Water delivery not found.");
            }

            delivery.ArrivalDate = DateTime.Now;
            delivery.DeliveryStatus = 2; // Marcamos como entregado

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Error updating the database: {ex.Message}");
            }

            // Redirige a la p�gina DeliverFinished pasando el waterDeliveryId
            return RedirectToPage("/Driver/DeliverFinished", new { waterDeliveryId });
        }

        // M�todo para cargar los detalles de la entrega de agua
        private async Task LoadWaterDeliveryAsync()
        {
            WaterDelivery = await _context.WaterDeliveries
                .Include(wd => wd.Otb)
                .FirstOrDefaultAsync(wd => wd.WaterDeliveryId == WaterDeliveryId);
        }
    }
}
