

$(document).ready(function () {
 
    $("#formulario-crear-membresia").on('submit', function (e) {
        e.preventDefault();

        if (validarFormulario()) {
            const nombre = $('#nombre').val();
            const cantidadBoletos = parseInt($('#cantidad-boletos').val());
            const cantidadEventos = parseInt($('#cantidad-eventos').val());
            const costoMembresia = parseFloat($('#costo-membresia').val());

            crearMembresia(nombre, costoMembresia, cantidadEventos, cantidadBoletos)
            // Limpiar los campos del formulario después de completar las validaciones
            $("#nombre").val("");
            $("#cantidad-boletos").val("");
            $("#cantidad-eventos").val("");
            $("#costo-membresia").val("");

            // Mostrar el mensaje de éxito
  
        }
    });
});


function validarFormulario() {
    const nombre = document.getElementById('nombre').value.trim();
    const cantidadBoletos = parseInt(document.getElementById('cantidad-boletos').value);
    const cantidadEventos = parseInt(document.getElementById('cantidad-eventos').value);
    const costoMembresia = parseFloat(document.getElementById('costo-membresia').value);


    const alphanumericRegex = /^[a-zA-Z0-9]+$/;

    if (nombre === '' || !alphanumericRegex.test(nombre)) {
        messageError('Error', 'Por favor, ingrese un nombre de membresía válido. Solo se permiten letras y números.');
        return false;
    }

    if (!cantidadBoletos || cantidadBoletos <= 0) {
        messageError('Error', 'Por favor, ingrese una cantidad de boletos válida. Solo se permiten números positivos.');
        return false;
    }

    if (!cantidadEventos || cantidadEventos <= 0) {
        messageError('Error', 'Por favor, ingrese una cantidad de eventos válida. Solo se permiten números positivos.');
        return false;
    }

    if (!costoMembresia || costoMembresia <= 0) {
        messageError('Error', 'Por favor, ingrese un costo de membresía válido. Solo se permiten números positivos.');
        return false;
    }

    // Si todas las validaciones son exitosas, mostramos el mensaje de éxito 
    
    return true;
}



function crearMembresia(nombreMembresia, costoTotal, cantidadEventos, cantidadBoletos) {
    $.ajax({
        type: 'POST',
        url: `${pathRedireccionBackEnd}/api/Membresia/CrearMembresia?nombreMembresia=${nombreMembresia}&costo=${costoTotal}&cantidadEventos=${cantidadEventos}&cantidadBoletos=${cantidadBoletos}`,
        contentType: 'application/json',
        success: function (data) {
            // Utilizar la respuesta de los boletos guardados
            mensajeConfirmacionAnimado('¡Creación exitosa! La membresía ha sido creada exitosamente.');
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}
