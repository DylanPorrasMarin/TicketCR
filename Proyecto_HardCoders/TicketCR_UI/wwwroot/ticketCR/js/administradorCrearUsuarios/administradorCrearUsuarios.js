let initialLat = 9.9333300;
let initialLng = -84.0833300;
let nuevaLatitud = 0;
let nuevaLongitud = 0;
let marcadorMovido = false; // Variable para indicar si el marcador ha sido movido


//INICIA UBICACION-MAPA 
let map, marker;
function initMap() {
    const initialPosition = { lat: initialLat, lng: initialLng }; // Utilizar las coordenadas iniciales
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
    google.maps.event.addListener(marker, 'dragend', function () {
        const newPosition = marker.getPosition();
        console.log('Nueva posición:', newPosition.lat(), newPosition.lng());

        // Actualizar los valores de los inputs ocultos con las nuevas coordenadas
        nuevaLatitud = newPosition.lat();
        nuevaLongitud = newPosition.lng();
        console.log('Nueva posición:', newPosition.lat(), newPosition.lng());

    });

}

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
    $('#nombre, #apellido1, #apellido2, #identificacion, #correo, #password1, #password2, #telefono, #tipoCuenta, #latitude, #longitude').val('');
}


function validarFormulario() {
    const nombre = $('#nombre').val().trim();
    const apellido1 = $('#apellido1').val().trim();
    const apellido2 = $('#apellido2').val().trim();
    const identificacion = $('#identificacion').val().trim();
    const nacimiento = $('#nacimiento').val();
    const correo = $('#correo').val().trim();
    const password1 = $('#password1').val();
    const password2 = $('#password2').val();
    const telefono = $('#telefono').val().trim();
    const tipoCuenta = $('#tipoCuenta').val();


 
    $('.invalid').removeClass('invalid');

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
        tipoCuenta === '' ||
        nuevaLatitud === 0 ||
        nuevaLatitud === 0
    ) {

        $('#nombre').addClass('invalid');
        $('#apellido1').addClass('invalid');
        $('#apellido2').addClass('invalid');
        $('#identificacion').addClass('invalid');
        $('#nacimiento').addClass('invalid');
        $('#correo').addClass('invalid');
        $('#password1').addClass('invalid');
        $('#password2').addClass('invalid');
        $('#telefono').addClass('invalid');
        $('#tipoCuenta').addClass('invalid');
        $('#latitude').addClass('invalid');
        $('#longitude').addClass('invalid');

        messageError('Error', 'Por favor, complete todos los campos.');
        return false; // Stop the form submission
    } else {

        if (!validarIdentificacion(identificacion)) {
            $('#identificacion').addClass('invalid');
            return false;
        } else {
            $('#identificacion').removeClass('invalid'); // Remove the "invalid" class if it's valid
        }

        
        if (!validarCorreo(correo)) {
            $('#correo').addClass('invalid');
            return false;
        } else {
            $('#correo').removeClass('invalid');
        }


        if (password1 !== password2 || password1.length < 8 || password2.length < 8) {
            $('#password1').addClass('invalid');
            $('#password2').addClass('invalid');
            return false;
        } else {
            $('#password1').removeClass('invalid');
            $('#password2').removeClass('invalid');
        }


        if (!validarTelefono(telefono)) {
            $('#telefono').addClass('invalid');
            return false;
        } else {
            $('#telefono').removeClass('invalid');
        }
  


        return true; // Allow the form to be submitted to the database
    }
}


function AdministradorRegistraUsuario(usuario) {
   
    $.ajax({
        type: 'POST',
        url: `${pathRedireccionBackEnd}/api/Usuario/InsertarUsuarioDesdeAdmin`,
        contentType: 'application/json',
        data: JSON.stringify(usuario),
        success: function (data) {
            mensajeConfirmacionAnimado('¡Registro exitoso!', 'Usuario registrado en el sistema correctamente.');
            console.log('Registro exitoso', data);
            limpiarFormulario()
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });

}

function ValidarCorreoRegistrado(correo,usuario) {
    $.ajax({
        type: 'GET',
        url: `${pathRedireccionBackEnd}/api/Usuario/VerificarCorreoExistente?correoUsuario=${correo}`,
        contentType: 'application/json',
        success: function (data) {
            // Verificar si el resultado es una respuesta válida y contiene el campo "existe", si es igual a false quiere decir que no eite en la base de datos
            if (data.existe === false) {
                AdministradorRegistraUsuario(usuario)

            } else {
                messageError('El correo ya está registrado en la base de datos, intente de nuevo con otro correo electrónico.');
            }
        },
        error: function (error) {
            console.error('Error:', error);
            // Llamar al callback con valor "false" en caso de error
            callback(false);
        }
    });
}

$(document).ready(function () {
    //VARIABLES GLOBALES


   
    configuracionCalendario();
    // Attach submit event to the form
    $("#formulario-registro").on('submit', function (e) {
        e.preventDefault();
        // Call the form validation function here
        //const isFormValid = validarFormulario();

        if (validarFormulario()) {
            const nombre = $('#nombre').val().trim();
            const apellido1 = $('#apellido1').val().trim();
            const apellido2 = $('#apellido2').val().trim();
            const identificacion = $('#identificacion').val().trim();
            const nacimiento = $('#nacimiento').val();
            const correo = $('#correo').val().trim();
            const password1 = $('#password1').val();
            const telefono = $('#telefono').val().trim();
            const tipoCuenta = $('#tipoCuenta').val();
            const latitude = nuevaLatitud;
            const longitude = nuevaLongitud;

            const usuario = {
                Nombre: nombre,
                Apellidos: apellido1 + " " + apellido2,
                Cedula: identificacion,
                FechaNacimiento: nacimiento,
                Correo: correo,
                Clave: password1,
                Telefono: telefono,
                Rol: tipoCuenta,
                Latitud: latitude,
                Longitud: longitude
            };

            ValidarCorreoRegistrado(correo, usuario) 
            
        }
    

    });
});
