let map, marker;
let idUsuarioRecienCreado, correo, Otp;

function configuracionCalendario() {


    $('#nacimiento').daterangepicker({
        locale: {
            format: "YYYY-MM-DD", // formato de la fecha sin las horas
            applyLabel: "Aplicar",
            cancelLabel: "Cancelar",
            fromLabel: "Desde",
            toLabel: "Hasta",
            customRangeLabel: "Custom",
            weekLabel: "W",
            daysOfWeek: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"],
            monthNames: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
            firstDay: 1
        },
        singleDatePicker: true,
        showDropdowns: true,
        minYear: 1903, // Establecer 1903 como el año mínimo
        maxYear: 2005, // Establecer 2005 como el año máximo
        minDate: '1903-01-01', // Establecer '1903-01-01' como la fecha mínima
        maxDate: '2005-12-31', // Establecer '2005-12-31' como la fecha máxima
        startDate: '2000-01-01', // Establecer '2000-01-01' como la fecha de inicio
        endDate: '2005-12-31', // Establecer '2005-12-31' como la fecha final
    }, (inicio, fin) => {
        console.log(inicio.format('YYYY-MM-DD') + '-' + fin.format('YYYY-MM-DD')); // actualizar aquí también el formato de la fecha
    });

}

function initMap() {
    const initialPosition = { lat: 9.9333300, lng: -84.0833300 }; 
    map = new google.maps.Map(document.getElementById('map'), {
        center: initialPosition,
        zoom: 12
    });

    marker = new google.maps.Marker({
        position: initialPosition,
        map: map,
        draggable: true // Hace que el marcador sea movible
    });

    // Escucha el evento 'dragend' para actualizar las coordenadas cuando el marcador se mueve
    google.maps.event.addListener(marker, 'dragend', function (event) {
        document.getElementById('latitude').value = event.latLng.lat();
        document.getElementById('longitude').value = event.latLng.lng();
    });


}

//DEJARLO YA QUE EVITA EL COMPORTAMIENTO POR DEFECTO DEL ENVIO DEL FORMULARIO Y ASI NO PROVOCA ERROR
$("#formulario-registro").on('submit', function (e) {
    e.preventDefault();
});

// Función para validar el formato del correo electrónico
function validarCorreo(correo) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(correo);
}

// Función para validar la identificación
function validarIdentificacion(identificacion) {
    // Verificar si la identificación contiene solo números y tiene exactamente 9 dígitos
    return /^\d{9}$/.test(identificacion);
}

// Función para validar el teléfono
function validarTelefono(telefono) {
    // Verificar si el teléfono contiene solo números y tiene exactamente 8 dígitos
    return /^\d{8}$/.test(telefono);
}


function limpiarFormulario() {
    document.getElementById('nombre').value = '';
    document.getElementById('apellido1').value = '';
    document.getElementById('apellido2').value = '';
    document.getElementById('identificacion').value = '';
    document.getElementById('nacimiento').value = '';
    document.getElementById('correo').value = '';
    document.getElementById('password1').value = '';
    document.getElementById('password2').value = '';
    document.getElementById('telefono').value = '';
    document.getElementById('tipoCuenta').value = '';
    document.getElementById('autenticacion').value = '';
    document.getElementById('latitude').value = '';
    document.getElementById('longitude').value = '';
}

// Función para validar el formulario
function validarFormularioRegistro() {
    const nombre = document.getElementById('nombre').value.trim();
    const apellido1 = document.getElementById('apellido1').value.trim();
    const apellido2 = document.getElementById('apellido2').value.trim();
    const identificacion = document.getElementById('identificacion').value.trim();
    const nacimiento = document.getElementById('nacimiento').value;
    const correo = document.getElementById('correo').value.trim();
    const password1 = document.getElementById('password1').value;
    const password2 = document.getElementById('password2').value;
    const telefono = document.getElementById('telefono').value.trim();
    const tipoCuenta = $('#tipoCuenta').val();
    const autenticacion = $('#autenticacion').val();
    const latitude = document.getElementById('latitude').value;
    const longitude = document.getElementById('longitude').value;

    if (
        nombre === '' ||
        apellido1 === '' ||
        apellido2 === '' ||
        identificacion === '' ||
        nacimiento === '' ||
        correo === '' ||
        password1 === '' ||
        password2 === '' ||
        telefono === '' ||
        tipoCuenta === 'null' ||
        autenticacion === 'null' ||
        latitude === '' ||
        longitude === ''
    ) {
        document.getElementById("nombre").classList.add("invalid");
        document.getElementById("apellido1").classList.add("invalid");
        document.getElementById("apellido2").classList.add("invalid");
        document.getElementById("identificacion").classList.add("invalid");
        document.getElementById("correo").classList.add("invalid");
        document.getElementById("password1").classList.add("invalid");
        document.getElementById("password2").classList.add("invalid");
        document.getElementById("telefono").classList.add("invalid");
        document.getElementById("tipoCuenta").classList.add("invalid");
        document.getElementById("autenticacion").classList.add("invalid");
        document.getElementById("latitude").classList.add("invalid");
        document.getElementById("longitude").classList.add("invalid");

        messageError('Error', 'Por favor, complete todos los campos.');
        return false; // Detener el envío del formulario
    } else {
        // Validar identificación
        document.getElementById("nombre").classList.remove("invalid");
        document.getElementById("apellido1").classList.remove("invalid");
        document.getElementById("apellido2").classList.remove("invalid");
        document.getElementById("identificacion").classList.remove("invalid");
        document.getElementById("correo").classList.remove("invalid");
        document.getElementById("password1").classList.remove("invalid");
        document.getElementById("password2").classList.remove("invalid");
        document.getElementById("telefono").classList.remove("invalid");
        document.getElementById("tipoCuenta").classList.remove("invalid");
        document.getElementById("autenticacion").classList.remove("invalid");
        document.getElementById("latitude").classList.remove("invalid");
        document.getElementById("longitude").classList.remove("invalid");

        if (!validarIdentificacion(identificacion)) {
            document.getElementById("identificacion").classList.add("invalid");
            return false;
        } else {
            document.getElementById("identificacion").classList.remove("invalid"); // Remover la clase "invalid" si es válida
        }

        // Validar correo electrónico
        if (!validarCorreo(correo)) {
            document.getElementById("correo").classList.add("invalid");

            return false;
        } else {
            document.getElementById("correo").classList.remove("invalid");
        }

        // Validar contraseñas
        if (password1 !== password2) {
            document.getElementById("password1").classList.add("invalid");
            document.getElementById("password2").classList.add("invalid");

            return false;
        } else {
            if (password1.length < 8 || password2.length < 8) {
                document.getElementById("password1").classList.add("invalid");
                document.getElementById("password2").classList.add("invalid");

                return false;
            } else {
                document.getElementById("password1").classList.remove("invalid");
                document.getElementById("password2").classList.remove("invalid");
            }
        }

        // Validar teléfono
        if (!validarTelefono(telefono)) {
            document.getElementById("telefono").classList.add("invalid");

            return false;
        } else {
            document.getElementById("telefono").classList.remove("invalid");
        }

       
        console.log(autenticacion)

       // limpiarFormulario(); // Limpia el formulario después de enviarlo

       

        var usuario = {
            Nombre: nombre,
            Apellidos: apellido1 +" "+ apellido2,
            Cedula: identificacion,
            FechaNacimiento: nacimiento,
            Correo: correo,
            Clave: password1,
            Telefono: telefono,
            rol: tipoCuenta,
            Latitud: latitude,
            Longitud: longitude,
            Rol: tipoCuenta,
          
        };



        //Valida el correo del usaurio que no exista en la bd y dentro de la funcion de registrar ejecuta las demas
        ValidarCorreoRegistrados(correo, usuario)
      
    }
        return true; // Permite enviar el formulario a la base de datos
}
function ValidarCorreoRegistrados(correo, usuario) {
    $.ajax({
        type: 'GET',
        url: `${pathRedireccionBackEnd}/api/Usuario/VerificarCorreoExistente?correoUsuario=${correo}`,
        contentType: 'application/json',
        success: function (data) {
            if (data.existe === false) {
                console.log()
                registrarUsuario(correo, usuario);
            } else {
                messageError('El correo ya está registrado en la base de datos, intente de nuevo con otro correo electrónico.');
            }
        },
        error: function (error) {
            console.error('Error:', error);
           
        }
    });
}

function registrarUsuario(correo, objetoUsuario) {
    const autenticacion = $('#autenticacion').val();
    var apiUrl = pathRedireccionBackEnd + "/api/Usuario/InsertarUsuario";

    $.ajax({
        headers: {
            'Accept': "application/json",
            'Content-Type': "application/json"
        },
        method: "POST",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: JSON.stringify(objetoUsuario),
        hasContent: true
    }).done(function (data) {
        const idUsuarioRecienCreado = data.usuario;
        crearOTP(idUsuarioRecienCreado);

        // Autenticación por correo
        if (autenticacion === 'correo') {
            enviarCorreo(correo, idUsuarioRecienCreado);
        }
        if (autenticacion === 'sms') {
            enviarSms(correo, idUsuarioRecienCreado);
        }

        // Limpia el formulario después de un registro exitoso
        limpiarFormulario();
        
    }).fail(function (error) {
        messageError('Error', 'No se envió el formulario.');
    });
}

//OTP
function crearOTP(idUsuarioRecienCreado ) {
    // Definimos los caracteres como números del 0 al 9
    const caracteres = '0123456789';

    let otpCreado = "";

    // Generamos un código numérico de 6 dígitos
    for (let i = 6; i > 0; i--) {
        otpCreado += caracteres[Math.floor(Math.random() * caracteres.length)];
    }

    // Convertimos el código en un valor entero (int)
    const Otp = parseInt(otpCreado);
    
    var otp = {
        IdUsuario: idUsuarioRecienCreado,
        Otp: Otp,
        estado: true,
    };

   

    var apiUrl = pathRedireccionBackEnd + "/api/Otp/CreateOtp";

    $.ajax({
        headers: {
            'Accept': "application/json",
            'Content-Type': "application/json"
        },
        method: "POST",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: JSON.stringify(otp),
        hasContent: true
    }).done(function (data) {

        console.log("OTP guardado");


    }).fail(function (error) {
        console.log("Fallo guardar OTP");
    });
    return true; // Permite enviar el formulario a la base de datos
}


//Envio OTP SMS

function enviarSms(correo, idUsuarioRecienCreado) {

    const telefono = document.getElementById('telefono').value.trim();
        
    
    var apiUrl = `${pathRedireccionBackEnd}/api/Communications/SendSMS?emailAddress=${correo}&telefono=${telefono}&idUsuario=${idUsuarioRecienCreado}`;

    $.ajax({
        
        method: "POST",
        url: apiUrl,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        hasContent: true
    }).done(function (data) {

        console.log("Sms enviado");
        mensajeProcesoRealizado('¡Registro exitoso!', 'Un mensaje de validación ha sido enviado, por favor verifique el correo o SMS.');


    }).fail(function (error) {
        messageError('Problemas con Twilio', 'Estamos teniendo problemas con nuestro servidor de SMS, por favor contacta al administrador para activar tu cuenta.')
        console.log("Fallo Twilio Sms ");
    });
   
}


//Envio OTP correo
function enviarCorreo(correoUsuario, idUsuario) {

    $.ajax({
        url: ` ${pathRedireccionBackEnd}/api/Communications/SendEmailActivation?emailAddress=${correoUsuario}&idUsuario=${idUsuario}`,
        type: 'POST',
        contentType: "application/json;charset=utf-8",
        success: function (data) {

            console.log('Correo de validación enviado con exito');
            limpiarFormulario();
            mensajeProcesoRealizado('¡Registro exitoso!', 'Un mensaje de validación ha sido enviado, por favor verifique el correo o SMS.');
        },
        error: function (error) {
            // Mostrar mensaje de error
            messageError('Error', ' El correo electrónico proporcionado no está registrado en ningun sistema');
        }
    });

}


$(document).ready(function () {
    configuracionCalendario();
});