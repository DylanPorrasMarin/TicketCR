
//MENSAJES CON LIBRERIA SWEETALERT
function messageInformativo(title, msj) {
    Swal.fire({
        icon: 'info',
        title: title,
        text: msj
    })
}

function messageError(title, msj) {
    Swal.fire({
        icon: 'error',
        title: title,
        text: msj
    })
}


function mensajeProcesoRealizado(title, msj) {
    Swal.fire({
        title: title,
        text: msj,
        icon: 'success',
        confirmButtonText: 'Aceptar',
        cancelButtonText: 'Cancelar'
    });
}

function mensajeConfirmacionAnimado(msj) {
    Swal.fire({
        icon: 'success',
        title: msj,
        showConfirmButton: false,
        timer: 2000
    })
}

function confirmacionProceso(title, msjAlerta,titleConfirmacion,msjConfimacion) {
    Swal.fire({
        title: title,
        text: msjAlerta,
        icon: 'warning',
        showCancelButton: true,
        cancelButtonColor: '#d33',
        confirmButtonColor: '#FFA500',
        confirmButtonText: 'Aceptar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire(
                titleConfirmacion,
                msjConfimacion,
                'success'
            )
        }
    })

}