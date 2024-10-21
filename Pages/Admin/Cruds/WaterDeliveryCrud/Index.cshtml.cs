using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using CisternasGAMC.Data;
using CisternasGAMC.Model;

namespace CisternasGAMC.Pages.Admin.Cruds.WaterDeliveryCrud
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<WaterDelivery> WaterDelivery { get; set; } = default!;

        public async Task OnGetAsync()
        {
            WaterDelivery = await _context.WaterDeliveries
                .Include(w => w.Cistern)
                .Include(w => w.Driver)
                .Include(w => w.Otb)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
