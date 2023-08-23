


function guardarEnLocalStorage(impuestos, comisiones) {
    localStorage.setItem('impuestos', impuestos);
    localStorage.setItem('comisiones', comisiones);
}

function cargarDesdeLocalStorage() {
    const impuestos = localStorage.getItem('impuestos');
    const comisiones = localStorage.getItem('comisiones');

    if (impuestos && comisiones) {
        $('#impuestos').val(impuestos);
        $('#comisiones').val(comisiones);
    }
}


$(document).ready(function () {

    cargarDesdeLocalStorage();

    $("#formulario-enviar-parametros").on('submit', function (e) {
        e.preventDefault();

        if (validarFormulario()) {
            const comisiones = parseFloat($('#comisiones').val());
            const impuestos = parseFloat($('#impuestos').val());

            guardarEnLocalStorage(impuestos, comisiones);

            actualizarParametro(impuestos, comisiones);
            // Limpiar los campos del formulario después de completar las validaciones
            $("#comisiones").val("");
            $("#impuestos").val("");

            cargarDesdeLocalStorage();
            // Mostrar el mensaje de éxito
        }
    });
});



function validarFormulario() {
    const impuestos = parseInt(document.getElementById('impuestos').value);
    const comisiones = parseInt(document.getElementById('comisiones').value);

    if (
        isNaN(impuestos) || impuestos < 0 || impuestos > 100 ||
        isNaN(comisiones) || comisiones < 0 || comisiones > 100
    ) {
        messageError('Error', 'Por favor, complete todos los campos correctamente. Los impuestos y las comisiones no pueden superar el 100%.');
        return false;
    }


    return true;
}



function actualizarParametro(impuesto, comisiones) {
    $.ajax({
        type: 'POST',
        url: `${pathRedireccionBackEnd}/api/Parametros/ActualizarParametro?comisiones=${comisiones}&impuestos=${impuesto}`,
        contentType: 'application/json',
        success: function (data) {
      
            // Utilizar la respuesta de los boletos guardados
            mensajeConfirmacionAnimado('¡Creación exitosa! Los parametros han sido actualizados exitosamente.');
        },
        error: function (error) {
            debugger
            console.error('Error:', error);
        }
    });
}
