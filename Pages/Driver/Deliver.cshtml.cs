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

        public int? WaterDeliveryId { get; private set; }

        [BindProperty]
        public WaterDelivery WaterDelivery { get; set; }

        public DeliverModel(ApplicationDbContext context)
        {
            _context = context;
        }

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

        public async Task<IActionResult> OnPostSetArrivalAsync(int waterDeliveryId)
        {
            var delivery = await _context.WaterDeliveries.FirstOrDefaultAsync(d => d.WaterDeliveryId == waterDeliveryId);

            if (delivery == null)
            {
                return NotFound("Water delivery not found.");
            }

            delivery.ArrivalDate = DateTime.Now;
            delivery.DeliveryStatus = 2;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Error updating the database.");
            }

            return RedirectToPage("/Driver/Deliver");
        }

        public async Task<IActionResult> OnPostDeliverAsync(int waterDeliveryId, float deliveredAmount)
        {
            var delivery = await _context.WaterDeliveries.FirstOrDefaultAsync(d => d.WaterDeliveryId == waterDeliveryId);

            if (delivery == null)
            {
                return NotFound("Water delivery not found.");
            }

            delivery.DepartureDate = DateTime.Now;
            delivery.DeliveredAmount = deliveredAmount;
            delivery.DeliveryStatus = 3;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Error updating the database.");
            }

            return RedirectToPage("/Driver/Index");
        }

        private async Task LoadWaterDeliveryAsync()
        {
            WaterDelivery = await _context.WaterDeliveries
                .Include(wd => wd.Otb)
                .FirstOrDefaultAsync(wd => wd.WaterDeliveryId == WaterDeliveryId);
        }
    }
}
