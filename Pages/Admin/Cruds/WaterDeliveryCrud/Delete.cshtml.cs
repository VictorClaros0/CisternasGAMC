using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CisternasGAMC.Data;
using CisternasGAMC.Model;

namespace CisternasGAMC.Pages.Admin.Cruds.WaterDeliveryCrud
{
    public class DeleteModel : PageModel
    {
        private readonly CisternasGAMC.Data.ApplicationDbContext _context;

        public DeleteModel(CisternasGAMC.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WaterDelivery WaterDelivery { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waterdelivery = await _context.WaterDeliveries.FirstOrDefaultAsync(m => m.WaterDeliveryId == id);

            if (waterdelivery == null)
            {
                return NotFound();
            }
            else
            {
                WaterDelivery = waterdelivery;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waterdelivery = await _context.WaterDeliveries.FindAsync(id);
            if (waterdelivery != null)
            {
                WaterDelivery = waterdelivery;
                _context.WaterDeliveries.Remove(WaterDelivery);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
