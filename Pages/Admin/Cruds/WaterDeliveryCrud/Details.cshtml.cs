using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CisternasGAMC.Data;
using CisternasGAMC.Model;
using Microsoft.AspNetCore.Authorization;

namespace CisternasGAMC.Pages.Admin.Cruds.WaterDeliveryCrud
{
    [Authorize(Roles = "admin")]
    public class DetailsModel : PageModel
    {
        private readonly CisternasGAMC.Data.ApplicationDbContext _context;

        public DetailsModel(CisternasGAMC.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
