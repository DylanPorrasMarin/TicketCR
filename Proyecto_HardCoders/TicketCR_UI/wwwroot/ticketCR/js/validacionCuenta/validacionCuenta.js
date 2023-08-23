$(document).ready(function () {


    function getUrlParameter(name) {
        name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
        var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
        var results = regex.exec(window.location.search);
        return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
    }

    // Obtener el idUsuario de la URL
    var idUsuario = getUrlParameter('idUsuario');

    // Convertir el valor a un número si es necesario
    var idUsuarioNumero = parseInt(idUsuario);




    $('#validar').on('submit', function (e) {
        e.preventDefault();
        console.log(idUsuarioNumero)
        cambiarEstadoCuentaUsuario(idUsuarioNumero)
   

        
    });
});


function cambiarEstadoCuentaUsuario(idUsaurio) {

    // Enviar la solicitud al endpoint de actualización de contraseña
    $.ajax({
        url: `${pathRedireccionBackEnd}/api/ValidacionCuenta/ActualizarEstadoOTPUsuario?idUsuario=${idUsaurio}`,
        type: 'PUT',
        contentType: 'application/json',
        success: function (data) {

            mensajeConfirmacionAnimado('Su cuenta ha sido validada exitosamente')
            //MUESTRA EL MENSAJE POR 5 SEGUNDOS LUEGO REDIRECCIONA AL LOGIN
            setTimeout(function () {
                window.location.href = pathRedireccionFrontEnd + 'Login';
            }, 2000);
            // 2000 milisegundos = 2 segundos
        },
        error: function (error) {
            // Mostrar mensaje de error
            messageError('Error', error);
        }
    });
}