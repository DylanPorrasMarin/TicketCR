var selectedPrice = 0;

let idUsuario = parseInt($('#id-usuario').val());
console.log(idUsuario);

function actualizarPayPal(idMembre, costoMembre, impuestosMembre, nombreMembre) {


    if (costoMembre === 0) {
        costoMembre=0.01
    }
    selectedPrice = costoMembre;

   console.log(selectedPrice)

        let nombreMembresia= nombreMembre;
        let impuestos = impuestosMembre;
        let idNuevaMembresia = idMembre;

    console.log('Nueva membresia select: ',idNuevaMembresia)
    console.log ('Nombre membre select: ',nombreMembresia)
        console.log('Impeustos', impuestos)


        paypal.Buttons({
            createOrder: function (data, actions) {
                return actions.order.create({
                    purchase_units: [{
                        amount: {
                            value: selectedPrice
                        }
                    }]
                });
            },
            onApprove: function (data, actions) {
                return actions.order.capture().then(function (details) {
                    // acciones después de que se haya aprobado el pago
                    mensajeConfirmacionAnimado('¡Pago exitoso, se le ha enviado un correo electrónico con los detalles!');

                    setTimeout(function () {
                        location.reload();
                    }, 3000);

                    const correoUsuario = details.payer.email_address;
                    const payerId = details.payer.payer_id;

    

                    actualizarMembresiaUsuario(correoUsuario, nombreMembresia, idNuevaMembresia, selectedPrice, impuestos, idUsuario, payerId)


                });
            },
            onCancel: function (data) {
                // Acciones cuando el pago es cancelado
                messageInformativo('¡Pago cancelado!');
                setTimeout(function () {
                    location.reload();
                }, 2000);
            }
        }).render('#paypal-button-container');
    }




function actualizarMembresiaUsuario(correoUsuario,nombreMembresia,idNuevaMembresia,costoTotal,impuestos,idUsaurio,idPagoPaypal)
{
    $.ajax({
        type: 'POST',
        url: `${pathRedireccionBackEnd}/api/Communications/CorreoMembresiaAdquirida?emailAddress=${correoUsuario}&NombreMembresia=${nombreMembresia}&IdNuevaMembresia=${idNuevaMembresia}&CostoTotal=${costoTotal}&Impuestos=${impuestos}&idUsuario=${idUsaurio}&IdPagoPaypal=${idPagoPaypal}`,
        contentType: 'application/json',
        success: function (data) {
            // Utilizar la respuesta de los boletos guardados
            console.log('Membresia actualizada pa!  ', data);
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}