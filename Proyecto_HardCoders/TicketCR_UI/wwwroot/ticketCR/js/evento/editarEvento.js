// Variables globales para almacenar las coordenadas de la ubicación inicial
let initialLat = 9.9333300;
let initialLng = -84.0833300;

let nuevaLatitud = 0;
let nuevaLongitud = 0;

const idUsuarioSesion = parseInt($('#id-usuario').val());

let latitudCargada = parseFloat($('#latitud-input').val());
let longitudCargada = parseFloat($('#longitud-input').val());
const idEvento = parseInt($('#id-evento').val());
let idRolUsuario = $('#id-rol').val();

console.log('rol', idRolUsuario)

console.log('ID EVENTO:', idEvento)
console.log('Latiud cargada antes:', latitudCargada)

//INICIA UBICACION-MAPA 
let map, marker;
function initMap() {
    const initialPosition = { lat: latitudCargada, lng: longitudCargada }; // Utilizar las coordenadas iniciales
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

        nuevaLatitud = newPosition.lat();
        nuevaLongitud = newPosition.lng();

        if (nuevaLatitud !== 0) {
            latitudCargada = nuevaLatitud;
            longitudCargada = nuevaLongitud;
        }
        console.log('Latiud cargada despues:', latitudCargada)

        // Validar si las nuevas coordenadas son iguales a las de la ubicación inicial
        if (latitudCargada !== newPosition.lat()) {
            messageInformativo('Ubicación nueva', 'Por favor, asegurese de ingresar la ubicación real del evento.');
        }
    });
}

//FUNCION PARA CONFIGURAR CALENDARIO
function configuracionCalendario() {

    $('#fecha-inicio').daterangepicker({
        locale: {
            format: "YYYY-MM-DD HH:mm:ss",
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
        timePicker: true,
        minYear: moment().year(), // Establecer el año actual como el mínimo
        maxYear: moment().format('YYYY'),
        minDate: moment().startOf('day'), // Establecer la fecha actual como la mínima
        startDate: moment().startOf('day'), // Establecer la fecha actual como fecha de inicio
        endDate: moment().startOf('day').add(1, 'day'), // Establecer la fecha de inicio + 1 día como fecha final
    }, (inicio, fin) => {
        console.log(inicio.format('YYYY-MM-DD HH:mm:ss') + '-' + fin.format('YYYY-MM-DD HH:mm:ss'));
    });

    $('#fecha-final').daterangepicker({
        locale: {
            format: "YYYY-MM-DD HH:mm:ss",
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
        timePicker: true,
        minYear: moment().year(), // Establecer el año actual como el mínimo
        maxYear: moment().format('YYYY'),
        minDate: moment().startOf('day'), // Establecer la fecha actual como la mínima
        startDate: moment().startOf('day'), // Establecer la fecha actual como fecha de inicio
        endDate: moment().startOf('day').add(1, 'day'), // Establecer la fecha de inicio + 1 día como fecha final
    }, (inicio, fin) => {
        console.log(inicio.format('YYYY-MM-DD HH:mm:ss') + '-' + fin.format('YYYY-MM-DD HH:mm:ss'));
    });
}



// Eventos para cada botón de cargar imagen
$('#btn-foto1').on('click', function (e) {
    e.preventDefault();
    // Asignar un identificador único para la imagen seleccionada en la variable currentImageId
    currentImageId = '#preview-imagen-evento-1';
    widgetCloudinary.open();
});

$('#btn-foto2').on('click', function (e) {
    e.preventDefault();
    // Asignar un identificador único para la imagen seleccionada en la variable currentImageId
    currentImageId = '#preview-imagen-evento-2';
    widgetCloudinary.open();
});

$('#btn-foto3').on('click', function (e) {
    e.preventDefault();
    // Asignar un identificador único para la imagen seleccionada en la variable currentImageId
    currentImageId = '#preview-imagen-evento-3';
    widgetCloudinary.open();
});

let urlImagen1 = $('#preview-imagen-evento-1').attr('src');
let urlImagen2 = $('#preview-imagen-evento-2').attr('src');
let urlImagen3 = $('#preview-imagen-evento-3').attr('src');

let imagen1 = $('#preview-imagen-evento-1');
let imagen2 = $('#preview-imagen-evento-2');
let imagen3 = $('#preview-imagen-evento-3');

const btnAgregarImagen1 = $('.btn-foto1');
const btnAgregarImagen2 = $('.btn-foto2');
const btnAgregarImagen3 = $('.btn-foto3');

//const imagenesSeleccionadas = []; // Variable para almacenar las URLs de las imágenes seleccionadas

let widgetCloudinary = cloudinary.createUploadWidget({
    cloudName: 'dxhibktjk',
    uploadPreset: 'present_ticketCR',
    folder: 'TicketCR'

}, (error, result) => {
    if (!error && result && result.event === 'success') {
        console.log('Imagen subida con éxito:', result.info);
        // Guardar la cadena de la URL de la imagen ya subida en Cloudinary en la variable
        //imagenesSeleccionadas.push(result.info.secure_url);
        if (currentImageId === '#preview-imagen-evento-1') {
            urlImagen1 = result.info.secure_url;
        } else if (currentImageId === '#preview-imagen-evento-2') {
            urlImagen2 = result.info.secure_url;
        } else if (currentImageId === '#preview-imagen-evento-3') {
            urlImagen3 = result.info.secure_url;
        }

        mostrarVistaPrevia(result.info.secure_url);
        console.log('UrlIamgen1', urlImagen1)
        console.log('UrlIamgen2', urlImagen2)
        console.log('UrlIamgen3', urlImagen3)
    }
});
console.log('UrlIamgen1', urlImagen1)
console.log('UrlIamgen2', urlImagen2)
console.log('UrlIamgen3', urlImagen3)
// Variable para almacenar el identificador de la imagen seleccionada
let currentImageId;

// Función para mostrar la vista previa de la imagen seleccionada en el campo correspondiente
function mostrarVistaPrevia(url) {
    $(currentImageId).attr('src', url); // Mostrar la vista previa en la imagen correspondiente
}

function generarCodigoUnicoBoletoNuevo() {
    const caracteres = '0123456789';
    let codigoUnico = '';

    for (let i = 0; i < 5; i++) {
        const indice = Math.floor(Math.random() * caracteres.length);
        codigoUnico += caracteres.charAt(indice);
    }

    return codigoUnico;
}



// Recopilar datos de boletos ingresados por el usuario
function obtenerDatosBoletos() {

    const boletosData = [];

    const totalBoletos = nuevoIndiceBoleto; // Obtener el número total de boletos creados

    for (let i = 0; i < totalBoletos; i++) {
        const tipoBoleto = $(`#tipoBoleto-${i}`).val();
        const cantidad = parseInt($(`#cantidadBoletos${i}`).val());
        const costo = parseFloat($(`#costoBoleto${i}`).val());

        if (tipoBoleto && cantidad && costo) {
            // Crear múltiples boletos según la cantidad ingresada
            for (let j = 0; j < cantidad; j++) {
                const codigo = generarCodigoUnicoBoletoNuevo();
                const boletoData = {
                    idBoleto: 0,
                    idEvento: 0,
                    idUsuarioComprador: 0,
                    cantidad: 0,
                    tipoBoleto: tipoBoleto,
                    asiento: "string",
                    costo: costo,
                    estado: true,
                    codigoQr: codigo,
                    impuesto: 0,
                    comision: 0
                };
                boletosData.push(boletoData);
            }
        }
    }

    console.log('Boletos desde la function:', boletosData);

    return boletosData;
}




//INSERTAR BOLETOS A EVENTO
const guardarBoletosEvento = (idEvento, boletos) => {
    $.ajax({
        type: 'POST',
        url: `${pathRedireccionBackEnd}/api/Boleto/InsertarBoletos?idEvento=${idEvento}`,
        contentType: 'application/json',
        data: JSON.stringify(boletos), // Convertir el array de boletos a formato JSON
        success: function (data) {
            // Utilizar la respuesta de los boletos guardados
            console.log('Boletos guardados:', data);
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
};
const ActualizarEvento = (idEvento,nombre,eslogan, descripcion, grafica, modalidad, FechaInicio, FechaFinal, restricciones, aforoMaximo, idCategoria, longitud, latitud, imagen1, imagen2, imagen3,link) => {
    // Construir la URL con los parámetros
    const url = `${pathRedireccionBackEnd}/api/Evento/ActualizarEvento?` +
        `IdEvento=${encodeURIComponent(idEvento)}&` +
        `Nombre=${encodeURIComponent(nombre)}&` +
        `Eslogan=${encodeURIComponent(eslogan)}&` +
        `Descripcion=${encodeURIComponent(descripcion)}&` +
        `Grafica=${encodeURIComponent(grafica)}&` +
        `Modalidad=${encodeURIComponent(modalidad)}&` +
        `FechaInicio=${encodeURIComponent(FechaInicio)}&` +
        `FechaFinal=${encodeURIComponent(FechaFinal)}&` +
        `Restricciones=${encodeURIComponent(restricciones)}&` +
        `AforoMaximo=${encodeURIComponent(aforoMaximo)}&` +
        `IdCategoria=${encodeURIComponent(idCategoria)}&` +
        `Longitud=${encodeURIComponent(longitud)}&` +
        `Latitud=${encodeURIComponent(latitud)}&` +
        `Imagen1=${encodeURIComponent(imagen1)}&` +
        `Imagen2=${encodeURIComponent(imagen2)}&` +
        `Imagen3=${encodeURIComponent(imagen3)}&` +
        `Link=${encodeURIComponent(link)}` 

    $.ajax({
        type: 'POST',
        url: url,
        success: function (data) {
            console.log(' Ecento actualizado general guardada:', data);

            Swal.fire({
                title: '¡Modificación exitosa!',
                text: 'Su solicitud ha sido enviada.',
                icon: 'success',
                showCancelButton: false,
                confirmButtonText: 'Aceptar',

            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = pathRedireccionFrontEnd + 'ConfiguracionEventos';
                }
            })

        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
};


//DESACTIVAR EVENTO Y SUS BOLETOS
function desactivarEvento(idEvento) {
    cambiarEstadoEvento(idEvento, true);
}

function activarEvento(idEvento) {
    cambiarEstadoEvento(idEvento, false);
}


function cambiarEstadoEvento(idEvento, nuevoEstado) {

    if (cantidadBoletosCreadosCargados !== 0) {

        Swal.fire({
            title: `¿Seguro que desea ${nuevoEstado ? 'desactivar' : 'activar'} este evento?`,
            text: `Este evento ${nuevoEstado ? 'no aparecera' : 'aparecera'} en la promoción de eventos, confirme si así lo desea`,
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
                    url: `${pathRedireccionBackEnd}/api/Evento/ActualizarEstadoEvento?idEvento=${idEvento}&estado=${nuevoEstado}`,
                    contentType: 'application/json',
                    success: function (data) {
                        mensajeConfirmacionAnimado(`¡Cambio de estado exitoso! El evento ha sido ${nuevoEstado ? 'desactivado' : 'activado'} correctamente.`);
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

    } else {
        messageInformativo('Boletos vendidos', 'El evento ha vendido todos lo boletos por lo que no se puede reactivar.')

        $('#btn-editar-evento').attr('disabled', true);

    }
 
}


function obtenerCantidadMaximaBoletosMembresia(idMembresia) {
    return new Promise((resolve, reject) => {
        // URL de tu API
        const apiUrl = `${pathRedireccionBackEnd}/api/Membresia/ObtenerMembresiaPorId?idMembresia=${idMembresia}`;

        // Realizar la solicitud a la API utilizando AJAX de jQuery
        $.ajax({
            type: 'GET',
            url: apiUrl,
            contentType: 'application/json',
            success: function (data) {
                console.log(data)
                const cantidadMaximaBoletos = data.cantidadBoletos;
                resolve(cantidadMaximaBoletos); // Resolvemos la promesa con la cantidadMaximaEventos
            },
            error: function (error) {
                console.error('Error al obtener la membresía:', error);
                reject(error); // Rechazamos la promesa en caso de error
            },
        });
    });
}
function getTotalCantidadBoletos() {
    let totalCantidad = 0;
    $('.cantidad-boleto').each(function () {
        const cantidad = parseInt($(this).val()) || 0;
        totalCantidad += cantidad;
    });
    return totalCantidad;
}


//FUNCIONES PARA CARGAR DATOS QUE VENGAN DESDE EL MODELEVENTO
function cargarDatosModelo() {
    //DESDE C#
    $('.plantilla-container').hide();

    var modalidad = $('#modalidad-cargada').val();
    console.log('modalidad cargada:', modalidad)
    var selectModalidad = $("#modalidad");
    selectModalidad.val(modalidad);
    if (modalidad === "2") {
        $('#contenedor-mapa-ubicacion').show();
    }

    var categoria = parseInt($('#categoria-cargada').val());
    console.log('categoria cargada:', categoria)
    var selectCategoria= $("#categoria");
    selectCategoria.val(categoria);

    var grafica = $('#grafica-cargada').val();
    console.log('grafica cargada:', grafica);

    var selectGrafica = $("#plantilla-selector");
    selectGrafica.val(grafica);
    if (grafica === '1') {
        $('#plantilla1-container').show();
      
        $('#plantilla2-container').hide();
    } else {
        $('#plantilla2-container').show();
     
        $('#plantilla1-container').hide();
    }
    
}

let aforoMaximo = 0;
let cantidadEventosCreados = 0;

console.log(idUsuarioSesion)
$(document).ready(function () {

    cargarDatosModelo()
    $(`#tipoBoleto-${0}, #cantidadBoletos${0}, #costoBoleto${0}`).on('input', function () {

        if ($(`#cantidadBoletos${0}`).val() > 70) {
            messageInformativo('Por tipo de boleto solo se puede agregar 70', 'Añade un boleto con el mismo tipo de boleto sin pasar los 70 en cantidad total a crear')
            $(this).val(0)

        }

        guardarBoletosEnLocalStorage(); // Actualizar el localStorage cuando cambia el valor en los nuevos campos
    });

    const idMembresia = parseInt($('#id-membresia').val());
    var formularioCrearEvento = $('#formulario-editar-evento');
    const btnCrearEvento = $('#btn-editar-evento')
    // Obtener el contador de eventos creados desde el almacenamiento local al cargar la página

    console.log('membre', idMembresia)
    //configurar calendario
    configuracionCalendario()

    btnAgregarImagen1.on('click', function (e) {
        e.preventDefault();
        widgetCloudinary.open();
    });
    btnAgregarImagen2.on('click', function (e) {
        e.preventDefault();
        widgetCloudinary.open();
    });
    btnAgregarImagen3.on('click', function (e) {
        e.preventDefault();
        widgetCloudinary.open();
    });





    if (idRolUsuario !== '1') {


        // Realizar la validación de la cantidad de boletos por membresia
        obtenerCantidadMaximaBoletosMembresia(idMembresia).then((cantidadMaximaBoletos) => {
            const cantidadActualBoletos = getTotalCantidadBoletos();
            const cantidadMaximaRestante = cantidadMaximaBoletos - cantidadActualBoletos;
            aforoMaximo = cantidadMaximaBoletos;

            // Establecer el valor máximo permitido para los campos de cantidad de boletos
            $('.cantidad-boleto').attr('max', cantidadMaximaRestante);

            // Agregar el evento input al campo de cantidad de boletos para validar en tiempo real
            $(document).on('input', '.cantidad-boleto', function () {
                const cantidadActualBoletos = getTotalCantidadBoletos();
                const cantidadMaximaRestante = cantidadMaximaBoletos - cantidadActualBoletos;

                // Establecer el valor máximo permitido para los campos de cantidad de boletos
                $('.cantidad-boleto').attr('max', cantidadMaximaRestante);

                if (cantidadActualBoletos > cantidadMaximaBoletos) {
                    messageError('Cantidad de boletos excedida', `Cantidad maxima de boletos a crear según membresía por evento es de ${cantidadMaximaBoletos}`)
                    // Deshabilitar el botón de crear evento si se alcanza el límite máximo de boletos
                    $(this).val(0);
                }
            });
        }).catch((error) => {
            console.error(error);
        });
    }

    //OCULATAR PLANTILLAS Y MAPA SI SE SELECCIONA VIRTUAL
    ocultarMapaYPlantillas();

    //Seleccion de plantillas
    seleccionarPlantillas()
    console.log('Latitud prueba:', nuevaLongitud)

    console.log('Datos de boletos recuperados de localStorage:', JSON.parse(localStorage.getItem('boletos')));
    //ENVIO DE FORMULARIO
    formularioCrearEvento.on('submit', function (e) {
        e.preventDefault(); // Evitar el envío por defecto del formulario

        let link = $('#link').val();

        const modalidadEvento = $('#modalidad').val();
        const nombreEvento = $('#nombre').val();
        const esloganEvento = $('#slogan').val();
        const fechaInicioEvento = moment($('#fecha-inicio').val(), 'YYYY-MM-DD HH:mm:ss').format('YYYY-MM-DDTHH:mm:ss');
        const fechaFinalEvento = moment($('#fecha-final').val(), 'YYYY-MM-DD HH:mm:ss').format('YYYY-MM-DDTHH:mm:ss');
        const descripcionEvento = $('#descripcion').val();
        const restricciones = $('#restricciones').val();
        const idCategoriaSeleccionada = parseInt($('#categoria').val());
        const grafica = parseInt($('#grafica-cargada').val());


        if (link === '') {

            link = 'PRESENCIAL'
        } else {
            link = $('#link').val();
        }
 

        console.log('FECHA INICIO' + fechaInicioEvento)
        console.log('FECHA FINAL' + fechaFinalEvento)
       



        // Obtener los datos de los boletos ingresados por el usuario
        //const boletosData = obtenerDatosBoletos();


        if (validarCampos()) {
            guardarBoletosEnLocalStorage(); // Guardar los datos del boleto en localStorage
            cantidadEventosCreados++; // Incrementar el contador de eventos creados
            localStorage.setItem('cantidadEventosCreados', cantidadEventosCreados); // Actualizar el almacenamiento local
            console.log('grafica', grafica)

            ActualizarEvento(
                idEvento,
                nombreEvento,
                esloganEvento,
                descripcionEvento,
                grafica,
                modalidadEvento,
                fechaInicioEvento,
                fechaFinalEvento,
                restricciones,
                aforoMaximo,
                idCategoriaSeleccionada,
                longitudCargada,
                latitudCargada,
                urlImagen1,
                urlImagen2,
                urlImagen3,
                link                
            );

           
        }

    });



    if (cantidadBoletosCreadosCargados !== 0) {
        btnCrearEvento.on('click', function () {
            formularioCrearEvento.submit();
        });


    } else {
        messageInformativo('Boletos vendidos', 'El evento ha vendido todos lo boletos por lo que no se puede actualizar.')
        $('#btn-editar-evento').attr('disabled', true);
    }
    
});



let nuevoIndiceBoleto = 1;
function agregarNuevoTipoBoleto() {
    const newIndex = nuevoIndiceBoleto; // Obtener el índice único para el nuevo boleto
    nuevoIndiceBoleto++; // Aumentar el contador para el próximo nuevo boleto

    const htmlNuevoBoleto = `
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="tipoBoleto-${newIndex}">Tipo de boleto</label>
                    <select name="tipoBoleto-${newIndex}" id="tipoBoleto-${newIndex}" class="tipo-Boleto form-control" required>
                        <option selected disabled value="">Tipo boleto</option>
                        <option value="1">Particular</option>
                        <option value="2">Estudiante</option>
                        <option value="3">Adulto mayor</option>
                        <option value="4">Regalía</option>
                        <option value="5">Virtual</option>
                    </select>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <label for="cantidadBoletos${newIndex}">Cantidad</label>
                    <input type="number" name="cantidadBoletos${newIndex}" id="cantidadBoletos${newIndex}" class="cantidad-boleto form-control" required>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <label for="costoBoleto${newIndex}">Costo</label>
                    <input type="number" name="costoBoleto${newIndex}" id="costoBoleto${newIndex}" class="form-control" required>
                </div>
            </div>
        </div>
    `;
    $(`#tipoBoleto-${newIndex}, #cantidadBoletos${newIndex}, #costoBoleto${newIndex}`).on('input', function () {

        if ($(`#cantidadBoletos${newIndex}`).val() > 70) {
            messageInformativo('Por tipo de boleto solo se puede agregar 70', 'Añade un boleto con el mismo tipo de boleto sin pasar los 70 en cantidad total a crear')
            $(this).val(0)

        }
        guardarBoletosEnLocalStorage(); // Actualizar el localStorage cuando cambia el valor en los nuevos campos
    });

    $('#nuevos-boletos').append(htmlNuevoBoleto);
}
$(document).on('change', '.tipo-Boleto', function () {
    const costoInput = $(this).closest('.row').find('[id^="costoBoleto"]');
    const tipoBoletoValue = $(this).val();

    if (tipoBoletoValue === '4') { // '4' representa el valor de "Regalía" en el select
        const costoSinDecimales = (0.01).toFixed(2); // Convertir a cadena con 0 decimales
        costoInput.val(costoSinDecimales);
        costoInput.prop('disabled', true);

    } else {
        costoInput.val(''); // Vaciar el campo si no es tipo "Regalía"
        costoInput.prop('disabled', false);
    }
});


$(document).on('click', '#agregar-tipo-boleto-nuevo', function () {
    agregarNuevoTipoBoleto();
});


// Función para eliminar el último tipo de boleto creado
function eliminarUltimoTipoBoleto() {
    const nuevosBoletos = $('#nuevos-boletos');
    const filasBoletos = nuevosBoletos.children('.row');

    // Verificamos si hay al menos una fila para eliminar
    if (filasBoletos.length > 0) {
        filasBoletos.last().remove();
    }
    // Después de eliminar, actualiza el localStorage
    guardarBoletosEnLocalStorage();
}

$(document).on('click', '#eliminar-tipo-boleto-nuevo', function () {
    eliminarUltimoTipoBoleto();
});


// Función para mapear los valores numéricos del tipo de boleto a sus nombres
function mapearTipoBoleto(nombre) {
    const tiposBoleto = {
        "1": "Particular",
        "2": "Estudiante",
        "3": "Adulto mayor",
        "4": "Regalía"
    };
    return tiposBoleto[nombre];
}


// Función para guardar los boletos seleccionados en localStorage
function guardarBoletosEnLocalStorage() {
    const boletos = []; // Array para almacenar los datos del boleto seleccionados

    // Obtener el tipo de boleto, cantidad y costo seleccionados para cada fila
    $('.row').each(function () {
        const tipoBoleto = $(this).find('select[name^="tipoBoleto"]').val();
        const cantidadBoletos = $(this).find('input[name^="cantidadBoletos"]').val();
        const costoBoleto = $(this).find('input[name^="costoBoleto"]').val();

        if (tipoBoleto && cantidadBoletos && costoBoleto) {
            boletos.push({
                tipo: tipoBoleto,
                cantidad: cantidadBoletos,
                costo: costoBoleto
            });
        }
    });

    // Eliminar duplicados del array de boletos
    const boletosSinDuplicados = [];
    boletos.forEach(function (boleto) {
        // Comprobar si el boleto ya existe en el nuevo array de boletos sin duplicados
        if (!boletosSinDuplicados.some(function (b) {
            return b.tipo === boleto.tipo && b.cantidad === boleto.cantidad && b.costo === boleto.costo;
        })) {
            boletosSinDuplicados.push(boleto);
        }
    });

    // Mapear los valores numéricos del tipo de boleto a sus nombres antes de guardar en localStorage
    for (let i = 0; i < boletosSinDuplicados.length; i++) {
        boletosSinDuplicados[i].tipo = mapearTipoBoleto(boletosSinDuplicados[i].tipo);
    }

    // Guardar los datos del boleto seleccionados en localStorage como una cadena JSON
    localStorage.setItem('boletos', JSON.stringify(boletosSinDuplicados));
    console.log('info boletos:', boletosSinDuplicados);
}


//OCULTAR PLANTILA Y MAPA
function ocultarMapaYPlantillas() {
    $('.container-link-evento').hide();

    if ($('#modalidad').val() === '1') {
        $('#contenedor-mapa-ubicacion').hide();
        $('#contenedor-plantilla-grafica').hide();
        $('.container-link-evento').show();
    }

    $('#modalidad').on('change', function () {
        const modalidadSeleccionada = $(this).val();

        if (modalidadSeleccionada === '1') {
            $('#contenedor-mapa-ubicacion').hide();
            $('#contenedor-plantilla-grafica').hide();
        } else {
            if (nuevaLatitud === 0 && nuevaLongitud === 0) {
                messageInformativo('Ingresa la ubicación', 'Mueva el pin en el mapa para especificar la ubicación del evento.');
            }
            $('#contenedor-mapa-ubicacion').show();
            $('#contenedor-plantilla-grafica').show();
            $('.container-link-evento').hide();
        }
    });

}


//Seleccionar plantillas
function seleccionarPlantillas() {

   
    const selectElement = $('#plantilla-selector');
    selectElement.on('change', function () {
        const selectedPlantilla = selectElement.val();

        // Ocultar todas las plantillas antes de mostrar la seleccionada
   
        if (selectedPlantilla === '1') {
            $('#plantilla1-container').show();
            $('#plantilla2-container').hide();
        } else if (selectedPlantilla === '2') {
            $('#plantilla2-container').show();
            $('#plantilla1-container').hide();
        } 
    });
}



//VALIDAR CAMPOS
function validarCampos() {
    const modalidadSelect = $('#modalidad');
    const tipoBoletoSelect = $('#tipoBoleto-0');
    const nombreInput = $('#nombre');
    const sloganInput = $('#slogan');
    const descripcionInput = $('#descripcion');
    const cantidadBoletosInput = $('#cantidadBoletos0');
    const costoBoletoInput = $('#costoBoleto0');
    const restriccionesInput = $('#restricciones');
    //const imagenesEvento = $('#upload_imgs').get(0).files

    if (
        modalidadSelect.val()==='' ||
        tipoBoletoSelect.val() === '' ||
        nombreInput.val() === '' ||
        sloganInput.val() === '' ||
        descripcionInput.val() === '' ||
        cantidadBoletosInput.val() === '' ||
        costoBoletoInput.val() === '' ||
        restriccionesInput.val() === ''
    ) {
        messageError('Campos sin completar', 'Por favor, complete todos los campos requeridos para crear el evento.');
        return false; // Indicamos que hay campos sin completar
    }


    if (cantidadBoletosInput.val() < 0) {
        messageError('Cantidad inválida', 'Ingrese un número positivo para la cantidad de boletos.');
        return false; // Indicamos que la cantidad de boletos es inválida
    }

    if (costoBoletoInput.val() < 0) {
        messageError('Costo inválido', 'Ingrese un número positivo para el costo de los boletos.');
        return false; // Indicamos que el costo de los boletos es inválido
    }
    if (
        urlImagen1 === '',
        urlImagen2 === '',
        urlImagen3 === '') {
        messageError('Ingrese imágenes para su evento', 'Debe de seleccionar 3 imágenes para su evento.');
        return false; // Indicamos que no se han seleccionado imágenes
    }


    return true; // Todos los campos están completos
}

