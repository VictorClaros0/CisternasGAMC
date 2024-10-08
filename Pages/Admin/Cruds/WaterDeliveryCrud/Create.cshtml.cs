using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CisternasGAMC.Data;
using CisternasGAMC.Model;

namespace CisternasGAMC.Pages.Admin.Cruds.WaterDeliveryCrud
{
    public class CreateModel : PageModel
    {
        private readonly CisternasGAMC.Data.ApplicationDbContext _context;

        public CreateModel(CisternasGAMC.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public WaterDelivery WaterDelivery { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.WaterDeliveries.Add(WaterDelivery);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
