using CisternasGAMC.Data;
using CisternasGAMC.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CisternasGAMC.Pages.Driver
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<Otb> otbs { get; set; } = default!;
        public async Task OnGetAsync()
        {
            otbs = await _context.Otbs.ToListAsync();
        }

    }
}
