const botones = document.getElementsByClassName('btn');
for (const boton of botones) {
    boton.addEventListener('click', function () {
        // mostrar el mensaje cuando se hace clic en el botón
        messageInformativo("Usuario no encontrado", "¡Debe estar registrado e iniciar sesión para comprar boletos!");
    });
}