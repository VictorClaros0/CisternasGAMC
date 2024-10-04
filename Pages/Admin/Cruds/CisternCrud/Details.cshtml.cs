using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CisternasGAMC.Data;
using CisternasGAMC.Model;

namespace CisternasGAMC.Pages.Admin.Cruds.CisternCrud
{
    public class DetailsModel : PageModel
    {
        private readonly CisternasGAMC.Data.ApplicationDbContext _context;

        public DetailsModel(CisternasGAMC.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Cistern Cistern { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cistern = await _context.Cisterns.FirstOrDefaultAsync(m => m.CisternId == id);
            if (cistern == null)
            {
                return NotFound();
            }
            else
            {
                Cistern = cistern;
            }
            return Page();
        }
    }
}
