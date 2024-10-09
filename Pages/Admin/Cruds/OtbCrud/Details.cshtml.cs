using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CisternasGAMC.Data;
using CisternasGAMC.Model;

namespace CisternasGAMC.Pages.Admin.Cruds.OtbCrud
{
    public class DetailsModel : PageModel
    {
        private readonly CisternasGAMC.Data.ApplicationDbContext _context;

        public DetailsModel(CisternasGAMC.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Otb Otb { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otb = await _context.Otbs.FirstOrDefaultAsync(m => m.OtbId == id);
            if (otb == null)
            {
                return NotFound();
            }
            else
            {
                Otb = otb;
            }
            return Page();
        }
    }
}
