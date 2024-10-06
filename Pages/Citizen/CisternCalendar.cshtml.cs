using CisternasGAMC.Data; // Asegúrate de tener la referencia correcta
using CisternasGAMC.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace CisternasGAMC.Pages.Citizen
{
    public class CisternCalendarModel : PageModel
    {
        private readonly ApplicationDbContext _context; // Añadido el contexto de la base de datos

        [BindProperty(SupportsGet = true)] // Esto permite recibir datos mediante la URL
        public int SelectedOtb { get; set; }

        public string NombreOTB { get; set; }
        public byte NumeroDistrito { get; set; }

        // Constructor que recibe el contexto de la base de datos
        public CisternCalendarModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            // Obtener los datos de la OTB a partir del SelectedOtb
            var otbData = _context.Otbs.FirstOrDefault(o => o.OtbId == SelectedOtb); // Asegúrate de que OtbId sea la clave primaria
            if (otbData != null)
            {
                NombreOTB = otbData.Name;
                NumeroDistrito = otbData.District;
            }
        }
    }

}
