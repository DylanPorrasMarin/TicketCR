//INICIA UBICACION-MAPA 
let map, marker;
let latitudEvento = parseFloat($('#latitud-input').val());
let longitudEvento = parseFloat($('#longitud-input').val());
var comision = parseFloat($('#comision').val());
var impuesto = parseFloat($('#impuesto').val());
var total = 0;

console.log(latitudEvento)
console.log(longitudEvento)

function initMap() {
    const initialPosition = { lat: latitudEvento, lng: longitudEvento}; // Nueva York como ubicación inicial
    map = new google.maps.Map(document.getElementById('map'), {
        center: initialPosition,
        zoom: 12
    });

    marker = new google.maps.Marker({
        position: initialPosition,
        map: map,
        draggable: false // Hace que el marcador sea movible
    });

    // Escucha el evento 'dragend' para actualizar las coordenadas cuando el marcador se mueve
    google.maps.event.addListener(marker, 'dragend', function () {
        const newPosition = marker.getPosition();
        console.log('Nueva posición:', newPosition.lat(), newPosition.lng());
    });
}
//TERMINA UBICACACION-MAPA

function actualizarTotal(soldSeatsInfo = []) {


    /*  total = 0;
      const selectsBoletos = $('.tipo-boleto');
      selectsBoletos.each(function () {
          const precioSeleccionado = parseInt($(this).find(':selected').data('precio'));
          console.log(selectsBoletos)
          if (!isNaN(precioSeleccionado)) {
              total += precioSeleccionado;
          }
      });*/

    total = 0; // Inicializamos el total

    // Sumamos los precios de los asientos de soldSeatsInfo
    soldSeatsInfo.forEach(seat => {
        total += seat.seatPrice;
    });
    costoFinalP = total + (total * (comision / 100)) + (total * (impuesto / 100));
    document.querySelector('#total-amount').innerHTML = costoFinalP;

}

//COMIENZA CARRUSEL
const buttons = document.querySelectorAll('[data-carousel-btn]');
buttons.forEach(btn => {
    btn.addEventListener('click', () => {
        const offset = btn.dataset.carouselButton === 'next' ? 1 : -1
        const slides = btn.closest('[data-carousel]').querySelector('[data-slides]')

        const activeSlide = slides.querySelector('[data-active]')
        let newIndex = [...slides.children].indexOf(activeSlide) + offset
        if (newIndex < 0) newIndex = slides.children.length - 1
        if (newIndex >= slides.children.length) newIndex = 0

        slides.children[newIndex].dataset.active = true
        delete activeSlide.dataset.active
    });
});


function limpiarFormularios() {
    $('input[type="email"]').val('');
    $('select').prop('selectedIndex', 0);
    actualizarTotal(); // Actualizamos el total para que refleje los cambios
}



function generateFormFromSeatInfo(soldSeatsInfo) {
    // Esta función crea la estructura de los formularios basándose en la información de los asientos vendidos.
    return soldSeatsInfo.map((seat, index) => `
        <div  class="form-line pad-bg mb-2">
            <div class="card text-center custom-form">
                <div class="card-header">
                    Titular ${index + 1}
                </div>
                <div class="card-body">
                    <div class="form-group">
                        <label for="email-compra-boleto-${index}" class="sr-only">Email</label>
                        <input type="email" id="email-compra-boleto-${index}" class="form-control input-lg correo-boleto-generado" placeholder="usuario@dominio.com"/>
                        <label for="asiento-compra-boleto-${index}" style="color:black">Asiento seleccionado</label>
                        <input type="text" id="asiento-seleccionado-${index}" class="form-control input-lg" value="${seat.seatNumber}" readonly />
                        <label for="tipo-boleto-${index}" style="color:black">Tipo boleto seleccionado</label>
                        <input type="text" id="tipo-boleto-${index}" class="form-control input-lg" value="${seat.seatType}" readonly />
                    </div>
                </div>
            </div>
        </div>
    `).join('');
}

var soldSeatsInfo;

function handleSeatsChanged() {
 
    soldSeatsInfo = JSON.parse(localStorage.getItem("soldSeatsInfo"));
    // Utilizamos la función generadora para obtener el HTML basándose en la info de los asientos vendidos
    const generatedHTML = generateFormFromSeatInfo(soldSeatsInfo);

    const seccionGeneradaHTML = `
        <div class="col-md-12" style="width:100%;">
            ${generatedHTML}
        </div>
    `;

    $('#seccion-generada').html(seccionGeneradaHTML);

    // Actualizamos el total utilizando la información de los asientos vendidos
    actualizarTotal(soldSeatsInfo);

    console.log('asientos seleccionados', soldSeatsInfo);

}

// Función para obtener los valores de los campos de correo electrónico
var correos = [];
$('#seccion-generada').on('input', '.correo-boleto-generado', function () {
    var index = parseInt($(this).attr('id').split('-')[3]);
    var correo = $(this).val();
    correos[index] = correo; // Actualizar el arreglo de correos con el valor ingresado
});


// Escuchamos cuando cambien los asientos seleccionados en la plantilla.
window.addEventListener("SeatsChanged", handleSeatsChanged);



function compraBoletos() {

    let listaObjetosBoletosPresenciales = []; // Inicializar la lista 

      // Lista para almacenar objetos de boletos
    var fechaInicioEventoStr = $('#fechaInicio-evento').val();
    var fechaFinalStr = $('#fechaFinal-evento').val();

    var tiposBoletosPorNumero = {
        1: "particular",
        2: "estudiante",
        3: "adulto mayor",
        4: "regalía"
    };

    var idsTiposBoletos = [];

    for (var i = 0; i < boletosData.length; i++) {
        var tipoNumero = boletosData[i].tipoBoleto;
        var tipoNombre = tiposBoletosPorNumero[tipoNumero];

        if (boletosData[i].estadoComprado === false) {
            var boletoId = boletosData[i].idBoleto;

            // Buscar en soldSeatsInfo un objeto que coincida con el tipo de boleto
            var matchingSeat = soldSeatsInfo.find(seat => seat.seatType === tipoNombre);
       
            if (matchingSeat) {
           
                idsTiposBoletos.push({
                    id: boletoId,
                    tipo: tipoNombre,
                    asiento: matchingSeat.seatNumber,
                    qr: boletosData[i].codigoQr
                });
            }
        }
    }

    console.log('ARRAY ID, TIPOS, ASIENTOS Y COSTOS BOLETOS', idsTiposBoletos);

    var nombreEvento = $('#nombre-evento').val();
    var idUsuarioComprador = $('#id-usuario-comprador').val();
    var idEvento = $('#id-evento').val();
    var fechaInicioEvento = parseFechaBoletos(fechaInicioEventoStr);
    var fechaFinal = parseFechaBoletos(fechaFinalStr);
    var indexAsiento = 0; // Índice para recorrer el arreglo de asientos
  
    let infoPlantillaActualizada = localStorage.getItem('InfoPlantilla');

    for (var i = 0; i < idsTiposBoletos.length; i++) {
        var tipoBoletoId = idsTiposBoletos[i].id; // OBTENER EL ID DE BOLETO
      
 
        // Buscar en idsTiposBoletos el objeto que coincide con el tipo de boleto actual
        var matchingBoleto = idsTiposBoletos.find(boleto => boleto.id === tipoBoletoId);

        console.log('Code',matchingBoleto.qr)

       
        if (matchingBoleto) {
            // Obtener el asiento correspondiente del arreglo soldSeatsInfo
            var matchingSeat = soldSeatsInfo[indexAsiento];
        
            if (!matchingSeat) continue;

            indexAsiento++; // Incrementar el índice para el siguiente asiento
          
            let objetoBoletoPresencial = {
                correo: correos[i],
                nombreEventoAsistir: nombreEvento,
                asiento: matchingSeat.seatNumber, // Agregar el asiento al objeto
                idUsuario: idUsuarioComprador,
                idBoleto: tipoBoletoId,
                idEvento: idEvento,
                cantidadBoletos: 1,
                total: total + (total * (comision / 100)) + (total * (impuesto / 100)),
                subtotal: total,
                comision: total * (comision / 100),
                impuesto: total * (impuesto / 100),
                fechaIncio: fechaInicioEvento.toISOString(),
                fechaFinal: fechaFinal.toISOString(),
                codigoQr: matchingBoleto.qr,
                infoPlantilla: infoPlantillaActualizada
            };
         
            // Agregar el objeto a la lista
            listaObjetosBoletosPresenciales.push(objetoBoletoPresencial);
        }
    }

 
    console.log('LISTA OFICIAL', listaObjetosBoletosPresenciales);

   

    insertarBoletosPresenciales(listaObjetosBoletosPresenciales);

}


function insertarBoletosPresenciales(listaObjetosBoletosPresenciales) {
 
    $.ajax({
        type: 'POST',
        url: `${pathRedireccionBackEnd}/api/Communications/CorreosCompraBoletosPresenciales`,
        contentType: 'application/json;charset=UTF-8',
        data: JSON.stringify(listaObjetosBoletosPresenciales),
        success: function (data) {
            // Utilizar la respuesta de los boletos guardados
            console.log('Insercion de boletos correcta!  ', data);

        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}


function CorreoFacturaBoletos(cuentaPaypal) {

    $.ajax({
        type: 'POST',
        url: `${pathRedireccionBackEnd}/api/Communications/CorreoFacturaBoletos?cuentaPaypal=${cuentaPaypal}`,
        contentType: 'application/json;charset=UTF-8',
        data: JSON.stringify(listaObjetosBoletosPresenciales),
        success: function (data) {
            // Utilizar la respuesta de los boletos guardados
            console.log('Insercion de boletos correcta!  ', data);

        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}


function PagoPaypalBoletosPresenciales() {

    paypal.Buttons({
        createOrder: function (data, actions) {
            return actions.order.create({
                purchase_units: [{
                    amount: {
                        value: costoFinalP
                    }
                }]
            });
        },
        onApprove: function (data, actions) {
            //VALIDAR INPUTS

            return actions.order.capture().then(function (details) {

               
                compraBoletos(); 
                // acciones después de que se haya aprobado el pag
                mensajeConfirmacionAnimado('¡Pago exitoso, se le ha enviado un correo electrónico con los detalles!');
                 setTimeout(function () {
                location.reload();
                    }, 2000);

            });
        },
        onCancel: function (data) {
            // Acciones cuando el pago es cancelado
            messageInformativo('¡Pago cancelado!');
            //setTimeout(function () {
            //    location.reload();
            //}, 2000);
        }
    }).render('#paypal-button-presencial-container');

}
$(document).ready(function () {

    PagoPaypalBoletosPresenciales()

});

function parseFechaBoletos(fechaStr) {
    var partes = fechaStr.split(' ');
    var fechaPartes = partes[0].split('/');
    var horaPartes = partes[1].split(':');

    var ano = parseInt(fechaPartes[2], 10);
    var mes = parseInt(fechaPartes[0], 10) - 1; // Meses en JavaScript son 0-indexados
    var dia = parseInt(fechaPartes[1], 10);

    var hora = parseInt(horaPartes[0], 10);
    var minutos = parseInt(horaPartes[1], 10);
    var segundos = parseInt(horaPartes[2], 10);

    return new Date(ano, mes, dia, hora, minutos, segundos);
}

