

$(document).ready(function () {




});

function desactivarMembresia(idMembresia) {
    cambiarEstadoEvento(idMembresia, false);
}

function activarMembresia(idMembresia) {
    cambiarEstadoEvento(idMembresia, true);
}

function cambiarEstadoEvento(idMembresia, nuevoEstado) {
    Swal.fire({
        title: `¿Seguro que desea ${nuevoEstado ? 'desactivar' : 'activar'} esta membresía?`,
        text: `confirme si así lo desea`,
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
                url: `${pathRedireccionBackEnd}/api/Membresia/ActualizarEstadoMembresia?idMembresia=${idMembresia}&estado=${nuevoEstado}`,
                contentType: 'application/json',
                success: function (data) {
                    mensajeConfirmacionAnimado(`¡Cambio de estado exitoso! La membresía ha sido ${nuevoEstado ? 'activada' : 'desactivada'} correctamente.`);
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

