using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CisternasGAMC.Data;
using CisternasGAMC.Model;

namespace CisternasGAMC.Pages.Admin.Cruds.CisternCrud
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
        public Cistern Cistern { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Cisterns.Add(Cistern);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
