var listaObjetosBoletosVirtuales = []; // Lista para almacenar objetos de boletos
const cantidadBoletosSelect = $('#cantidad-boletos');
const nuevaCantidadBoletos = parseInt(cantidadBoletosSelect.val()) - 1;
var fechaInicioEventoStr = $('#fechaInicio-evento').val();
var fechaFinalStr = $('#fechaFinal-evento').val();
var costoBoletos = 0;
var totalBolets = 0;
var costoFinal = 0;

function capturarInfoBoletosVirtuales(selectElement) {
    totalBolets = 0;
    var selects = $('.select-valor'); // Usando jQuery para seleccionar los elementos


    // Sacar total de costo y actualizar el span
    selects.each(function () {
        var selectedOption = $(this).find('option:selected');
        var costo = parseFloat(selectedOption.attr('data-costo')) || 0;
        totalBolets += costo;
    });

    // Validar selects que se deben deshabilitar
    selects.each(function () {
        var selectedOption = $(this).find('option:selected');
        var selectedId = selectedOption.attr('data-id');
        var currentSelect = this;
        selects.each(function () {
            if (this !== currentSelect) {
                var otherOptions = $(this).find('option');
                otherOptions.each(function () {
                    if ($(this).attr('data-id') === selectedId) {
                        $(this).prop('disabled', true);
                    }
                });
            }
        });
    });

    console.log('ID del boleto seleccionado:', $(selectElement).find('option:selected').attr('data-id'));
    console.log('comision del boleto seleccionado:', $(selectElement).find('option:selected').attr('data-comision'));
    console.log('impuesto del boleto seleccionado:', $(selectElement).find('option:selected').attr('data-impuesto'));

    var formulario = $(selectElement).closest('.custom-form'); // Obtener el formulario padre

    var correo = formulario.find('.input-lg').val();
    var tipoBoletoId = $(selectElement).val();
    var costo = parseFloat($(selectElement).find('option:selected').attr('data-costo')) || 0;
    var comision = parseFloat($(selectElement).find('option:selected').attr('data-comision')) || 0;
    var impuesto = parseFloat($(selectElement).find('option:selected').attr('data-impuesto')) || 0;
    var nombreEvento = $('#nombre-evento').val();
    var idUsuarioComprador = $('#id-usuario-comprador').val();
    var idEvento = $('#id-evento').val();
    var link = $('#link').val();


    costoFinal = totalBolets + (totalBolets * (comision / 100)) + (totalBolets * (impuesto / 100));

    $('#total-amount').text(costoFinal.toFixed(2));

    var fechaInicioEvento = parseFecha(fechaInicioEventoStr);
    var fechaFinal = parseFecha(fechaFinalStr);

    var objetoBoletoVirtual = {
        correo: correo,
        nombreEventoAsistir: nombreEvento,
        idUsuario: idUsuarioComprador,
        idBoleto: tipoBoletoId,
        idEvento: idEvento,
        cantidadBoletos: 1,
        link: link,
        total: costo + (costo * (comision / 100)) + (costo * (impuesto / 100)),
        subtotal: costo,
        comision: costo*(comision/100),
        impuesto: costo*(impuesto/100),
        fechaIncio: fechaInicioEvento.toISOString(),
        fechaFinal: fechaFinal.toISOString()
    };


    // Agregar el objeto a la lista
    listaObjetosBoletosVirtuales.push(objetoBoletoVirtual);
    console.log(listaObjetosBoletosVirtuales);

}


function insertarBoletosVirtuales() {
    $.ajax({
        type: 'POST',
        url: `${pathRedireccionBackEnd}/api/Communications/CorreosCompraBoletosVirtuales`,
        contentType: 'application/json;charset=UTF-8',
        data: JSON.stringify(listaObjetosBoletosVirtuales),
        success: function (data) {
            // Utilizar la respuesta de los boletos guardados
            console.log('Insercion de boletos correcta!  ', data);

        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}


function PagoPaypalBoletosVirtuales() {


 
    paypal.Buttons({

        createOrder: function (data, actions) {
   
            return actions.order.create({
                purchase_units: [{
                    amount: {
                        value: costoFinal
                    }
                }]
            });
        },
        onApprove: function (data, actions) {

       
            return actions.order.capture().then(function (details) {

              
                // acciones después de que se haya aprobado el pago
                insertarBoletosVirtuales();

                mensajeConfirmacionAnimado('¡Pago exitoso, se le ha enviado un correo electrónico con los detalles!');
               
                setTimeout(function () {
                    window.location.href = pathRedireccionFrontEnd + 'MenuEventos';
                }, 3000);

            });
        },
        onCancel: function (data) {
            // Acciones cuando el pago es cancelado
            messageInformativo('¡Pago cancelado!');
            //setTimeout(function () {
            //    location.reload();
            //}, 2000);
        }
    }).render('#paypal-button-virtual-container');

}

function validarFormulario(card) {
    var correo = card.find('.email-field').val();
    var selectedBoleto = card.find('.select-boleto').val();

    if (correo.trim() === '') {
        messageError('Por favor, ingresa un correo electrónico.');
        return false;
    }

    if (selectedBoleto === '0') {
        messageError('Por favor, selecciona un tipo de boleto.');
        return false;
    }

    return true;
}



$(document).ready(function () {
    var selectBoleto = $('.select-boleto'); // Deshabilitar todos los selects inicialmente
    selectBoleto.prop('disabled', true);

    $('.email-field').on('input', function () {
        var correo = $(this).val();
        var card = $(this).closest('.custom-form'); // Obtener la "card" actual
        var selectBoleto = card.find('.select-boleto'); // Encontrar el select en la misma "card"

        if (correo.trim() !== '') {
            selectBoleto.prop('disabled', false);
        } else {
            selectBoleto.prop('disabled', true);
           
        }
    });
    
    $('#btn-agregar-boletos-virtuales').on('click', function (e) {
        e.preventDefault();
        $('#boletosModal').modal('show');
      
    });

    PagoPaypalBoletosVirtuales();
    $('#cerrar-modal').on('click', function (e) {
        e.preventDefault();
        $('#boletosModal').modal('hide');

    });

});




function parseFecha(fechaStr) {
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