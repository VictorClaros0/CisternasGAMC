document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'timeGridWeek',
        headerToolbar: {
            left: '',
            center: 'title',
            right: ''
        },
        allDaySlot: false,
        slotMinTime: "08:00:00",
        slotMaxTime: "11:00:00",
        // Puedes usar el evento 'datesSet' para cambiar el título después de que se renderice el calendario
        datesSet: function () {
            var titleElement = document.querySelector('.fc-toolbar-title');
            if (titleElement) {
                titleElement.textContent = 'Horario Semanal';
            }
            changeSlotLabels();
            changeDayHeaders(); 
        }
    });
    calendar.render();
    function changeSlotLabels() {
        // Seleccionar los elementos de las etiquetas de hora
        var labels = document.querySelectorAll('.fc-timegrid-slot-label-cushion');

        // Verificar si hay etiquetas para evitar errores
        if (labels.length > 0) {
            // Cambiar los textos según la hora
            if (labels[0]) labels[0].textContent = 'Mañana';  // 8am
            if (labels[1]) labels[1].textContent = 'Tarde';   // 9am
            if (labels[2]) labels[2].textContent = 'Noche';   // 10am
        }
    }
    function changeDayHeaders() {
        // Selecciona todos los encabezados de día
        var dayHeaders = document.querySelectorAll('.fc-col-header-cell-cushion');

        // Cambia el texto según el orden que quieres
        if (dayHeaders.length > 0) {
            var headers = ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'];
            dayHeaders.forEach((header, index) => {
                if (headers[index]) {
                    header.textContent = headers[index]; // Cambiar el texto del encabezado
                }
            });
        }
    }
});
