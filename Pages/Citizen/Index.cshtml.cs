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

        public IList<Otb> Otbs { get; set; }
        public IList<byte> Distritos { get; set; }

        // Propiedad para mostrar el estado de la cisterna
        public string CisternStatusMessage { get; set; }
        public bool IsCisternAvailable { get; set; }

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
            var cistern = _context.Cisterns.FirstOrDefault();

            if (cistern != null)
            {
                // Evaluar el estado de la cisterna basado en su Status
                if (cistern.Status == 1)
                {
                    CisternStatusMessage = "La Cisterna se encuentra en movimiento";
                    IsCisternAvailable = true;
                }
                else if (cistern.Status == 0)
                {
                    CisternStatusMessage = "La Cisterna no está en operación";
                    IsCisternAvailable = false;
                }
                else
                {
                    CisternStatusMessage = "La Cisterna no se encuentra en servicio";
                    IsCisternAvailable = false;
                }
            }
            else
            {
                CisternStatusMessage = "No se encontró información de la cisterna.";
                IsCisternAvailable = false;
            }
        }

        // Método que se ejecuta al enviar el formulario
        public void OnPost()
        {
            Otbs = _context.Otbs.ToList();
            Distritos = _context.Otbs.Select(o => o.District).Distinct().ToList();
        }
    }
}
