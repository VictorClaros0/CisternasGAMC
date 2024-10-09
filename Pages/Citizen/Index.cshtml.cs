using CisternasGAMC.Data;
using CisternasGAMC.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CisternasGAMC.Pages.Citizen
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // Propiedades para enlazar los datos
        [BindProperty]
        public byte? SelectedDistrito { get; set; }  // Cambia el tipo según sea necesario

        public IList<Otb> Otbs { get; set; } = new List<Otb>();
        public IList<byte> Distritos { get; set; } = new List<byte>();

        

        // Constructor que recibe el contexto de la base de datos
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método que se ejecuta al cargar la página
        public void OnGet()
        {
            // Cargar todos los OTBs y Distritos al cargar la página por primera vez
            LoadDistrictsAndOtbs();
        }

       

        private void LoadDistrictsAndOtbs()
        {
            Otbs = _context.Otbs.ToList();
            Distritos = _context.Otbs
                .Select(o => o.District)
                .Distinct()
                .ToList();
        }

        
        public JsonResult OnGetOtbs(byte district)
        {
            var filteredOtbs = _context.Otbs
                .Where(o => o.District == district)
                .Select(o => new { o.OtbId, o.Name })  // Asegúrate de devolver estas propiedades
                .ToList();

            return new JsonResult(filteredOtbs);
        }


    }
}
