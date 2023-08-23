$(document).ready(function () {

  
    const contenedorQR = document.getElementById('contenedor-qr')
    
    function getUrlParameter(name) {
        name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
        var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
        var results = regex.exec(window.location.search);
        return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
    }

    // Obtener el idUsuario de la URL
    var codigoQR = getUrlParameter('codigoQr');

    //Generar codigoQR con el que venga desde la url
    new QRCode(contenedorQR,codigoQR);

    $("#formulario-validar-boleto").on('submit', function (e) {
        e.preventDefault();

       // let codigoQr = $("#codigo-boleto").val(); // Mover esta línea aquí
     
            obtenerBoletoPorQr(codigoQR);
     
    });
});




function obtenerBoletoPorQr(codigoQr) {
    $.ajax({
        url: `${pathRedireccionBackEnd}/api/Boleto/ObtenerBoletosPorQr?codigoQr=${codigoQr}`,
        type: 'GET',
        contentType: 'application/json;charset=UTF-8',
        success: function (dataArray) {
            console.log(dataArray);

            // Usar map() para acceder al primer objeto del array (si existe)
            const [data] = dataArray.map(obj => obj);

            if (data) { // Verificar si data es válido
                const estadoBoleto = data.estado;
                const codigoQr = data.codigoQr;

                if (estadoBoleto === true) {
                    cambiarEstadoQr(codigoQr);
                } else {
                    messageError('El boleto ya fue usado', 'Este boleto ya fue validado anteriormente para ingresar al evento.');
                }
            } else {
                messageError('Error', 'No se encontraron datos válidos para el código QR.');
            }
        },
        error: function (error) {
            messageError('Error', error);
        }
    });
}



function cambiarEstadoQr(codigoQr) {

    // Enviar la solicitud al endpoint de actualización de contraseña
    $.ajax({
        url: `${pathRedireccionBackEnd}/api/Boleto/ValidarBoletoPorQr?codigoQr=${codigoQr}`,
        type: 'PUT',
        contentType: 'application/json;charset=UTF-8',
        success: function (data) {

            mensajeConfirmacionAnimado('Boleto validado correctamente, el usuario comprador puede ingresar al evento')
            //MUESTRA EL MENSAJE POR 5 SEGUNDOS LUEGO REDIRECCIONA AL LOGIN
            //setTimeout(function () {
            //    window.location.href = pathRedireccionFrontEnd + 'Login';
            //}, 2000);
            // 2000 milisegundos = 2 segundos
        },
        error: function (error) {
            // Mostrar mensaje de error
            messageError('Error', error);
        }
    });
}

function validarFormulario() {
    // Obtenemos el valor del campo de entrada de código de boleto
    const codigoBoleto = document.getElementById('codigo-boleto').value.trim();

    // Validación específica para números
    if (codigoBoleto === '') {
        messageError('Error', 'Por favor, ingrese el código de su boleto.');
        return false;
    }

    if (!/^\d+$/.test(codigoBoleto)) {
        messageError('Error', 'El código del boleto debe ser un número.');
        return false;
    }

    // Si todas las validaciones son exitosas, mostramos el mensaje de éxito 
    return true;
}












