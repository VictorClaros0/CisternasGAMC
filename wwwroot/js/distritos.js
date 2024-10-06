$(document).ready(function () {
    // Cuando el usuario cambie el distrito
    $('#distritoSelect').change(function () {
        var selectedDistrito = $(this).val();
        if (selectedDistrito) {
            // Hacer la petición AJAX para obtener las OTBs
            $.ajax({
                url: '/Citizen/Index?handler=Otbs', // Endpoint al método del backend
                type: 'GET',
                data: { district: selectedDistrito },
                success: function (data) {
                    // Limpiar el combobox de OTB
                    $('#otbSelect').empty();
                    $('#otbSelect').append('<option value="">Seleccione un OTB</option>');

                    // Añadir las nuevas OTBs
                    $.each(data, function (index, otb) {
                        $('#otbSelect').append('<option value="' + otb.otbId + '">' + otb.name + '</option>');
                    });
                }
            });
        } else {
            // Si no se selecciona distrito, limpiamos el select de OTB
            $('#otbSelect').empty();
            $('#otbSelect').append('<option value="">Seleccione un OTB</option>');
        }
    });
});
