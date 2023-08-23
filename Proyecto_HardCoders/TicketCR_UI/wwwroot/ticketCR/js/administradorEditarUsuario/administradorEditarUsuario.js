let map, marker;



// Convertir las cadenas a números
var latitudParsed = parseFloat(latitud);
var longitudParsed = parseFloat(longitud);



/*const actualizarSesion = (usuario) => {
    $.ajax({
        type: 'POST',
        url: pathRedireccionBackEnd + '/api/Usuario/ActualizarSesion', // Asegúrate de que esta URL sea correcta
        contentType: 'application/json',
        data: JSON.stringify(usuario),
        success: function (data) {
            console.log('Sesión actualizada:', data);
        },
        error: function (error) {
            console.error('Error al actualizar la sesión:', error);
        }
    });
};*/

const actualizarPerfilUsuario = (data) => {

    console.log(pathRedireccionBackEnd);
    $.ajax({
        type: 'POST',
        url: pathRedireccionBackEnd + '/api/Usuario/ActualizarUsuario',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function (payload) {

          
            console.log('Datos del usuario Actualizados:', payload);


            const idUsuarioCreado = payload.idUsuario;
            console.log(idUsuarioCreado);


            //actualizarSesion(data);

        },
        error: function (error) {

            console.error('Error:', error);
        }
    });
};

const actualizarEstadoUsuario = (idUsuario,estado) => {

    console.log(pathRedireccionBackEnd);
    $.ajax({
        type: 'POST',
        url: `${pathRedireccionBackEnd}/api/InformeEventos/ActualizarEstadoUsuario?idUsuario=${idUsuario}?estado=${estado}`,
        
        success: function (payload) {

        
            console.log('Datos del usuario Actualizados:', payload);


            const idUsuarioCreado = payload.idUsuario;
            console.log(idUsuarioCreado);


            //actualizarSesion(data);

        },
        error: function (error) {

            console.error('Error:', error);
        }
    });
};



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
    const initialPosition = { lat: latitudParsed, lng: longitudParsed }; // Nueva York como ubicación inicial
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

// Función para validar el formulario
function validarFormulario() {
    const nombre = document.getElementById('nombre').value.trim();
    const apellidos = document.getElementById('apellidos').value.trim();
    const identificacion = document.getElementById('identificacion').value.trim();
    const nacimiento = document.getElementById('nacimiento').value;
    const correo = document.getElementById('correo').value.trim();
    const password1 = document.getElementById('password1').value;
    const password2 = document.getElementById('password2').value;
    const telefono = document.getElementById('telefono').value.trim();
    const latitude = document.getElementById('latitude').value;
    const longitude = document.getElementById('longitude').value;
    const tipoCuenta = document.getElementById('tipoCuenta').value;

    if (
        nombre === '' ||
        apellidos === '' ||
        identificacion === '' ||
        nacimiento === '' ||
        correo === '' ||
        password1 === '' ||
        password2 === '' ||
        telefono === '' ||
        latitude === '' ||
        longitude === ''
    ) {

        messageError('Error', 'Por favor, complete todos los campos.');
        return false;
    }
    else {
        // Validar identificación
        if (!validarIdentificacion(identificacion)) {
            messageError('Error', 'El número de identificación debe tener exactamente 9 dígitos.');
            return false;
        }
        else {
            // Validar correo electrónico
            if (!validarCorreo(correo)) {
                messageError('Error', 'Por favor, ingrese un correo electrónico válido.');
                return false;
            }
            else {
                // Validar contraseñas
                if (password1 !== password2) {
                    messageError('Error', 'Las contraseñas no coinciden.');
                    return false;
                } else {
                    if (password1.length < 8 || password2.length < 8) {
                        messageError('Contraseña sin longitud requerida', 'Ingresa una contraseña con al menos 8 caracteres.');
                        return false;
                    }
                    else {
                        // Validar teléfono
                        if (!validarTelefono(telefono)) {
                            messageError('Error', 'El número de teléfono debe tener exactamente 8 dígitos y está sin caracteres especiales.');
                            return false;
                        }
                        else {
                            // Si todo está validado y es correcto, muestra mensaje de registro exitoso

                            let fullPath = window.location.pathname; // Obtiene la ruta completa
                            let segments = fullPath.split('/'); // Divide la ruta en segmentos usando el carácter '/'
                            let IdUsuario = segments[segments.length - 1]; // Toma el último segmento, que es el ID del usuario

                           


                            const usuario = {
                                IdUsuario: IdUsuario,
                                Nombre: nombre,
                                Apellidos: apellidos,
                                Cedula: identificacion,
                                FechaNacimiento: nacimiento,
                                Correo: correo,
                                Clave: password1,
                                Telefono: telefono,
                                Latitud: latitude,
                                Longitud: longitude,
                                Rol: tipoCuenta,
                            }
                     

                            actualizarPerfilUsuario(usuario);

                            Swal.fire({
                                title: '¡Modificación exitosa!',
                                text: 'Su solicitud ha sido enviada.',
                                icon: 'success',
                                showCancelButton: false,
                                confirmButtonText: 'Aceptar',
                               
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    window.location.href = pathRedireccionFrontEnd + 'AdministrarUsuarios';
                                }
                            })

                           

                            return true; // Permite enviar el formulario a la base de datos
                        }
                    }
                }
            }
        }
    }
}






$(document).ready(function () {

   
    configuracionCalendario()

    var formularioRegistro = $('#formulario-registro');

    const btnEnviar = $('#btn-enviar-form');

    $("#formulario-registro").on('submit', function (e) {
        e.preventDefault();
        validarFormulario();
    });

    btnEnviar.on('click', function () {
        formularioRegistro.submit();
    });

});

function desactivarUsuario(idUsuario) {
    cambiarEstadoUsuario(idUsuario,false);
}

function activarUsuario(idUsuario) {
    cambiarEstadoUsuario(idUsuario, true);
}


function cambiarEstadoUsuario(idUsuario, nuevoEstado) {

        Swal.fire({
            title: `¿Seguro que desea ${nuevoEstado ? 'activar' : 'desactivar'} este Usuario?`,
            text: `Este Usuario ${nuevoEstado ? 'tendra acceso' : 'no tendra acceso'} al sistema`,
            icon: 'warning',
            showCancelButton: true,
            cancelButtonColor: '#d33',
            confirmButtonColor: '#FFA500',
            confirmButtonText: 'Aceptar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'PUT',
                    url: `${pathRedireccionBackEnd}/api/Usuario/ActualizarEstadoUsuario?idUsuario=${idUsuario}&estado=${nuevoEstado}`,
                    contentType: 'application/json',
                    success: function (data) {
                        mensajeConfirmacionAnimado(`¡Cambio de estado exitoso! El usuario ha sido ${nuevoEstado ? 'activado' : 'desactivado'} correctamente.`);
                        setTimeout(function () {
                            location.reload();
                        }, 2000);
                    },
                    error: function (error) {
                        console.error('Error:', error);
                    }
                });
            }
        });

    

}