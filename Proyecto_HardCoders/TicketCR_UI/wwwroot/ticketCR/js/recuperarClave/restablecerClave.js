$(document).ready(function () {
    $('#formulario-restablecer-clave').on('submit', function (e) {
        e.preventDefault();
        // SECCION DE VALIDACIONES
        const clave01 = $('#clave-01').val();
        const clave02 = $('#clave-02').val();
        let isValid = true;

        if (clave01 != clave02) {
            isValid = false;
            messageError('Las contraseñas no coinciden', 'Asegúrate de proporcionar contraseñas idénticas para restablecer tu cuenta.');
        } else if (clave01.length < 8) {
            isValid = false;
            messageError('Contraseña sin longitud requerida', 'Ingresa una contraseña con al menos 8 caracteres.');
        }

        if (!isValid) { return; }
        // TERMINA SECCION DE VALIDACIONES

        // Obtener el correo electrónico de la URL
        function getUrlParameter(name) {
            name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
            var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
            var results = regex.exec(window.location.search);
            return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
        }
        // Obtener el correo electrónico y el token de la URL
        var email = getUrlParameter('email');

        // Enviar la solicitud al endpoint de actualización de contraseña
        $.ajax({
            url: pathRedireccionBackEnd + '/api/RestablecerClave/UpdatePassword?email='+email+'&newPassword='+clave01,
            type: 'POST',
            dataType: 'text',
            contentType: "application/json;charset=utf-8",
            hasContent: true,
            success: function (data) {
          
                mensajeConfirmacionAnimado('Contraseña restablecida correctamente')
                //MUESTRA EL MENSAJE POR 5 SEGUNDOS LUEGO REDIRECCIONA AL LOGIN
                setTimeout(function () {
                    window.location.href = pathRedireccionFrontEnd + 'Login';
                }, 2000);
                 // 2000 milisegundos = 2 segundos
            },
            error: function (error) {
                // Mostrar mensaje de error
                messageError('Error', ' El correo electrónico proporcionado no está registrado en nuestro sistema. Por favor, inténtalo nuevamente.');
            }
        });
    });
});

