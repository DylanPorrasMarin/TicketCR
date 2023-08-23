// Variables globales para almacenar las coordenadas de la ubicación inicial
let initialLat = 9.9333300;
let initialLng = -84.0833300;

let nuevaLatitud = 9.9333300;
let nuevaLongitud = -84.0833300;
const idUsuarioSesion = parseInt($('#id-usuario').val());
let idRolUsuario = $('#id-rol').val();
let marcadorMovido = false; // Variable para indicar si el marcador ha sido movido
const event = new Event("localStorageChange");
let aforoMaximo = 0;
var cantidadBoletosCreados = 0;

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

        // Validar si las nuevas coordenadas son iguales a las de la ubicación inicial
        if (!marcadorMovido) {
            messageInformativo('Ubicación registrada', 'Por favor, asegúrese de ingresar la ubicación real del evento.');
            marcadorMovido = true; // Marcar que el marcador ha sido movido
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


let urlImagen1 = '';
let urlImagen2 = '';
let urlImagen3 = '';

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
        console.log('UrlIamgen1',urlImagen1)
        console.log('UrlIamgen2', urlImagen2)
        console.log('UrlIamgen3', urlImagen3)
    }
});

// Variable para almacenar el identificador de la imagen seleccionada
let currentImageId;

// Función para mostrar la vista previa de la imagen seleccionada en el campo correspondiente
function mostrarVistaPrevia(url) {
    $(currentImageId).attr('src', url); // Mostrar la vista previa en la imagen correspondiente
}



function generarCodigoUnico() {
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
            for (let j = 0; j < cantidad; j++) {
                const codigo = generarCodigoUnico(); // Generar un código único para cada boleto
                const boletoData = {
                    idBoleto: 0,
                    idEvento: 0,
                    idUsuarioComprador: 0,
                    cantidad: 0,
                    tipoBoleto: tipoBoleto,
                    asiento: "string",
                    costo: costo,
                    estado: true,
                    codigoQr: codigo, // Asignar el código único a cada boleto
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



function CantidadTotalBoletosCreados() {
    const cantidadInputs = document.querySelectorAll(".cantidad-boleto");
    let cantidadTotal = 0;

    cantidadInputs.forEach(input => {
        const cantidad = parseInt(input.value) || 0;
        cantidadTotal += cantidad;
    });

    cantidadBoletosCreados = cantidadTotal;
    console.log("Cantidad total de boletos:", cantidadBoletosCreados);

    return cantidadBoletosCreados; // Devolver el valor calculado
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









const guardarInfoGeneralEvento = (objectoEvento,boletosData) => {
    // Construir la URL con los parámetros
    const url = `${pathRedireccionBackEnd}/api/Evento/CrearEvento`;
 
    $.ajax({
        type: 'POST',
        url: url,
        contentType: 'application/json;charset=UTF-8',
        data: JSON.stringify(objectoEvento),
        success: function (data) {
            console.log('Información general guardada:', data);

            const idEventoRecienCreado = data.idEvento;
            console.log(idEventoRecienCreado);
            guardarBoletosEvento(idEventoRecienCreado, boletosData);

            mensajeConfirmacionAnimado('Evento creado', `Se ha registrado satisfactoriamente su evento en TicketCR.`);
            setTimeout(function () {
                window.location.href = pathRedireccionFrontEnd + 'CrearEvento';
            }, 2000);
        },
        error: function (error) {
            console.error('Error:', error);
            messageError('Error al crear el evento', `Consulte con los administradores para resolver el error`)
        }
    });
};

function obtenerCantidadMaximaEventosMembresia(idMembresia) {
    return new Promise((resolve, reject) => {
        // URL de tu API
        const apiUrl = `${pathRedireccionBackEnd}/api/Membresia/ObtenerMembresiaPorId?idMembresia=${idMembresia}`;

        // Realizar la solicitud a la API utilizando AJAX de jQuery
        $.ajax({
            type: 'GET',
            url: apiUrl,
            contentType: 'application/json',
            success: function (data) {
                console.log('cantidadeveb', data)
                const cantidadMaximaEventos = data.cantidadEventos;
                resolve(cantidadMaximaEventos); // Resolvemos la promesa con la cantidadMaximaEventos
            },
            error: function (error) {
                console.error('Error al obtener la membresía:', error);
                reject(error); // Rechazamos la promesa en caso de error
            },
        });
    });
}
let idMembresia = parseInt($('#id-membresia').val());

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


let cantidadEventosCreados = 0;


console.log(idUsuarioSesion)


$(document).ready(function () {


    //localStorage.removeItem("boletos");

    $(`#tipoBoleto-${0}, #cantidadBoletos${0}, #costoBoleto${0}`).on('input', function () {

        if ($(`#cantidadBoletos${0}`).val() > 70) {
            messageInformativo('Por tipo de boleto solo se puede agregar 70', 'Añade un boleto con el mismo tipo de boleto sin pasar los 70 en cantidad total a crear')
            $(this).val(0)

        }
   
        guardarBoletosEnLocalStorage(); // Actualizar el localStorage cuando cambia el valor en los nuevos campos
    });


    const cantidadEventosCreados = parseInt($('#cantidad-eventos').val());

    console.log('cantidad-Eventos ', cantidadEventosCreados)


    var formularioCrearEvento = $('#formulario-crear-evento');
    const btnCrearEvento = $('#btn-crear-evento')


    console.log('Cantidad de eventos creados:', cantidadEventosCreados)
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

    // Agregar el evento input al campo de cantidad de boletos  
    if (idRolUsuario!=='1') {

        // Realizar la validación de la cantidad de eventos como lo hacías anteriormente
        obtenerCantidadMaximaEventosMembresia(idMembresia).then((cantidadMaximaEventos) => {
            console.log('cantievent', cantidadMaximaEventos)
            if (cantidadEventosCreados >= cantidadMaximaEventos) {
                messageError('Actualizar membresía', 'Haz alcanzado el límite máximo de eventos por crear');
                btnCrearEvento.prop('disabled', true);
            } else {
                // Si no se supera el límite, habilitar el botón de crear evento
                btnCrearEvento.prop('disabled', false);
            }
        }).catch((error) => {
            console.error(error);
        });

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


    document.addEventListener("input", function (event) {
        if (event.target.classList.contains("cantidad-boleto")) {
            cantidadBoletosCreados = CantidadTotalBoletosCreados();
            console.log('Despues del if', cantidadBoletosCreados);
        }
    });

    console.log('Despues del if',cantidadBoletosCreados)

 
    console.log(cantidadEventosCreados)
    //OCULATAR PLANTILLAS Y MAPA SI SE SELECCIONA VIRTUAL
    ocultarMapaYPlantillas();

    //Seleccion de plantillas
    seleccionarPlantillas()
    console.log('Latitud prueba:', nuevaLongitud)



    console.log('Datos de boletos recuperados de localStorage:', JSON.parse(localStorage.getItem('boletos')));
    //ENVIO DE FORMULARIO

    let link = 'PRESENCIAL';

    formularioCrearEvento.on('submit', function (e) {
        e.preventDefault(); // Evitar el envío por defecto del formulario

         
        const modalidadEvento = $('#modalidad').val();
        const nombreEvento = $('#nombre').val();
        const esloganEvento = $('#slogan').val();
        const fechaInicioEvento = moment($('#fecha-inicio').val(), 'YYYY-MM-DD HH:mm:ss').format('YYYY-MM-DDTHH:mm:ss');
        const fechaFinalEvento = moment($('#fecha-final').val(), 'YYYY-MM-DD HH:mm:ss').format('YYYY-MM-DDTHH:mm:ss');
        const descripcionEvento = $('#descripcion').val();
        const restricciones = $('#restricciones').val();
        const idCategoriaSeleccionada = parseInt($('#categoria').val());
        const grafica = parseInt($('#plantilla-selector').val());
        link = $('#link').val();

        console.log('FECHA INICIO' + fechaInicioEvento)
        console.log('FECHA FINAL' + fechaFinalEvento)




        // Obtener los datos de los boletos ingresados por el usuario
  
        const boletosData = obtenerDatosBoletos();
        console.log(boletosData)

        if (validarCampos()) {
         
           // cantidadEventosCreados++; // Incrementar el contador de eventos creados
           // localStorage.setItem('cantidadEventosCreados', cantidadEventosCreados); // Actualizar el almacenamiento local
            
            limpiarCampos();
            console.log('grafica', grafica)


            //OBTENER INFORMACION DE LA PLANTILLA
            // Obtener información de la plantilla solo si el evento NO es virtual
            var infoPlantilla = 'VIRTUAL';

            if (modalidadEvento === '2') {
                infoPlantilla = localStorage.getItem('InfoPlantilla');
            }



            console.log(infoPlantilla)

            const objetoEvento = {

                idUsuario: idUsuarioSesion,
                nombre:nombreEvento,
                eslogan:esloganEvento,
                descripcion:descripcionEvento,
                grafica:grafica,
                modalidad:modalidadEvento,
                fechaInicio:fechaInicioEvento,
                fechaFinal:fechaFinalEvento,
                restricciones:restricciones,
                aforoMaximo:aforoMaximo,
                idCategoria:idCategoriaSeleccionada,
                longitud:nuevaLongitud,
                latitud:nuevaLatitud,
                imagen1:urlImagen1,
                imagen2:urlImagen2,
                imagen3:urlImagen3,
                infoPlantilla: infoPlantilla,
                link: link,
                CantidadBoletosCreados: cantidadBoletosCreados


            } 

            console.log(objetoEvento)
            guardarInfoGeneralEvento(objetoEvento, boletosData);

        }

    });

    btnCrearEvento.on('click', function () {
        formularioCrearEvento.submit();
    });

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
                        <option selected disabled value="">Seleccione el tipo de boleto</option>
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
                    <label for="costoBoleto${newIndex}">Costo($)</label>
                    <input type="number" name="costoBoleto${newIndex}" id="costoBoleto${newIndex}" class="form-control" required>
                </div>
            </div>
        </div>
    `;

    $('#nuevos-boletos').append(htmlNuevoBoleto);

    //  GUARDAR BOLETOS EN VIVO EN EL LOCAL STORAGE
    $(`#tipoBoleto-${newIndex}, #cantidadBoletos${newIndex}, #costoBoleto${newIndex}`).on('input', function () {
        guardarBoletosEnLocalStorage(); // Actualizar el localStorage cuando cambia el valor en los nuevos campos
    });

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
        "4": "Regalía",
        "5": "Virtual"
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
    window.dispatchEvent(event);
    console.log('info boletos:', boletosSinDuplicados);
}
//GUARDAR BOLETOS EN LOCALSTORAGE APENAS SE INGRESE UN DATOS EN EL INPUT


//OCULTAR PLANTILA Y MAPA
function ocultarMapaYPlantillas() {

   
    $('.container-link-evento').hide();

    $('#contenedor-mapa-ubicacion').hide();
    $('#modalidad').on('change', function () {
        const modalidadSeleccionada = $(this).val();
      
        if (modalidadSeleccionada === '1') {
           
            $('#contenedor-mapa-ubicacion').hide();
            $('#contenedor-plantilla-grafica').hide();
            $('.container-link-evento').show();
        } else {
            if (nuevaLatitud === 0 && nuevaLongitud === 0) {
                messageInformativo('Ingresa la ubicación', 'Mueva el pin en el mapa para especificar la ubicación del evento.');
            }
            $('.container-link-evento').hide();
            $('#contenedor-mapa-ubicacion').show();
            $('#contenedor-plantilla-grafica').show();
        }
    });

}


//Seleccionar plantillas
function seleccionarPlantillas() {

    $('.plantilla-container').hide();

    const selectElement = $('#plantilla-selector');
    selectElement.on('change', function () {
        const selectedPlantilla = selectElement.val();

        // Ocultar todas las plantillas antes de mostrar la seleccionada
        $('.plantilla-container').hide();

        if (selectedPlantilla === '1') {
            $('#plantilla1-container').show();
        } else if (selectedPlantilla === '2') {
            $('#plantilla2-container').show();
        } else {
            // Si se selecciona una opción no válida (por si acaso)
            $('.plantilla-container').show();
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
    const categoria = $('#categoria').val();
    //const imagenesEvento = $('#upload_imgs').get(0).files

    if (
        !modalidadSelect.val() ||
        !tipoBoletoSelect.val() ||
        !nombreInput.val() ||
        !sloganInput.val() || 
        !descripcionInput.val() ||
        !cantidadBoletosInput.val() ||
        !costoBoletoInput.val() ||
        !restriccionesInput.val() ||
        !categoria
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
         urlImagen3 === '')
    {
        messageError('Ingrese imágenes para su evento', 'Debe de seleccionar 3 imágenes para su evento.');
        return false; // Indicamos que no se han seleccionado imágenes
    }


    return true; // Todos los campos están completos
}


function limpiarCampos() {
    // Limpiar todos los campos del formulario
    $('#formulario-crear-evento')[0].reset();

    // Eliminar la URL de las imágenes previas
    $('#preview-imagen-evento-1').attr('src', '');
    $('#preview-imagen-evento-2').attr('src', '');
    $('#preview-imagen-evento-3').attr('src', '');

    // Volver a habilitar el input file para cargar imágenes
    // $('#upload_imgs').prop('disabled', false);

    $('#plantilla').empty();
    // Limpiar campos adicionales de boletos dinámicos
    $('#nuevos-boletos').empty();

    // Ocultar las imágenes de las plantillas
    $('.plantilla-container').hide();

    // Reiniciar el mapa a la posición inicial
    const initialPosition = { lat: 9.9333300, lng: -84.0833300 };
    marker.setPosition(initialPosition);
    map.setCenter(initialPosition);
    map.setZoom(12);
}
