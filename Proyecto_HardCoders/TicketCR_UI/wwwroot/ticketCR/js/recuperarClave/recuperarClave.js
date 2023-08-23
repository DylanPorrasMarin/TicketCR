$(document).ready(function () {
    $('#formulario-recuperacion-clave').on('submit', function (e) {
        e.preventDefault();


        //SECCION DE VALIDACIONES
        const btnEnviarCorreo = $('#btn-correo-recuperacion')
        // Se toma el valor del input correo electrónico
        const emailRecuperacion = $('#email-recuperacion').val();
        // Variable bandera para evitar el envío del formulario
        let isValid = true;

        // Validar que el formulario tenga información
        if (!emailRecuperacion) {
            
            isValid = false;
            messageError('Campo sin rellenar', 'Por favor, ingresa el correo electrónico de recuperación');
        } else {
            // Validación del formato del correo electrónico
            const emailRegex = /^\w+([.-_+]?\w+)*@\w+([.-]?\w+)*(\.\w{2,10})+$/;
            if (!emailRegex.test(emailRecuperacion)) {
                
                isValid = false;
                messageError('Formato incorrecto', 'Por favor, ingrese un correo electrónico válido.');
            }
        }
        // Evitar el envío del formulario
        if (!isValid) { return; }
        //TERMINA SECCION DE VALIDACIONES

         // Deshabilitar el botón de envío mientras se espera la respuesta del AJAX
        btnEnviarCorreo.prop('disabled', true);

        // Inicializar SweetAlert con timer inicial en 2000 milisegundos (2 segundos)



        //CODIGO PARA ENVIAR CON AJAX EL CORREO ELECTRONICO PARA LA RECUPERACION DE CLAVE DEL USUARIO
        $.ajax({
            url: pathRedireccionBackEnd + '/api/Communications/SendEmailForgotPassword?emailAddress=' + emailRecuperacion,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            hasContent: true,
            success: function (response) {
                // Mostrar mensaje de éxito
                mensajeProcesoRealizado(`Correo enviado con exito`, `Se le ha enviado las instrucciones a su correo para el restablecimiento de contraseña`)
                // Mostrar mensaje de éxito
                btnEnviarCorreo.prop('disabled', false);
                console.log(response);
            },
            error: function (error) {
                btnEnviarCorreo.prop('disabled', false);
                // Mostrar mensaje de error
                messageError('Error', 'No se pudo enviar la solicitud de recuperación de contraseña(correo electrónico no encontrado). Inténtalo nuevamente.');
            }
        });
    });
});

