using CisternasGAMC.Data; // Asegúrate de tener la referencia correcta
using CisternasGAMC.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
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

        // Propiedad para almacenar los eventos del calendario
        public List<CalendarEvent> CalendarEvents { get; set; } = new List<CalendarEvent>(); // Asegúrate de agregar esta propiedad

        public List<WaterDelivery> GetWaterDeliveries()
        {
            return _context.WaterDeliveries
                .Where(w => w.OtbId == SelectedOtb) // Filtrar por OTB
                .ToList();
        }

        // Constructor que recibe el contexto de la base de datos
        public CisternCalendarModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            var otbData = _context.Otbs.FirstOrDefault(o => o.OtbId == SelectedOtb);
            if (otbData != null)
            {
                NombreOTB = "Villa Cuchillo";
                NumeroDistrito = otbData.District;
            }

            // Obtener las entregas de agua
            List<WaterDelivery> waterDeliveries = GetWaterDeliveries();

            // Transformar las entregas en eventos del calendario
            CalendarEvents = waterDeliveries.Select(w => new CalendarEvent
            {
                Title = $"Entrega de Agua: {w.DeliveredAmount} L",
                Start = w.DeliveryDate.Date.AddHours(8), // Asignar la hora 8 AM
                End = w.DeliveryDate.Date.AddHours(9),   // Duración de 1 hora
                DayOfWeek = w.DeliveryDate.DayOfWeek.ToString(),
                TimeSlot = GetTimeSlot(w.DeliveryDate.Hour) // Asignar la franja horaria
            }).ToList();
        }

        // Método para determinar la franja horaria
        private string GetTimeSlot(int hour)
        {
            if (hour >= 6 && hour < 12)
                return "Mañana";
            else if (hour >= 12 && hour < 18)
                return "Tarde";
            else
                return "Noche";
        }

        public class CalendarEvent
        {
            public string Title { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public string DayOfWeek { get; set; } // Día de la semana
            public string TimeSlot { get; set; } // Nueva propiedad para definir la franja horaria
        }
    }
}
