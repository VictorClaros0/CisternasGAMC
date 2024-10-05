using CisternasGAMC.Data;
using CisternasGAMC.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public IList<Otb> Otbs { get; set; }

        public IList<byte> Distritos { get; set; }

        // Constructor que recibe el contexto de la base de datos
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método que se ejecuta al cargar la página
        public void OnGet()
        {
            // Obtener todos los OTBs de la base de datos
            Otbs = _context.Otbs.ToList();

            // Obtener distritos únicos de los OTBs
            Distritos = _context.Otbs
                .Select(o => o.District) // Asumiendo que 'District' es el atributo que contiene el valor numérico
                .Distinct()
                .ToList();
        }

        // Método que se ejecuta al enviar el formulario
        public void OnPost()
        {
            // Obtener el ID del OTB seleccionado
            var selectedOtbId = Request.Form["selectedOtb"];
            // Aquí puedes manejar la lógica para lo que quieres hacer con el ID seleccionado
        }
    }
}
