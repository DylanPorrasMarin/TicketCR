

var IdUsuario = $("#IdUsuario").val();


//chatBot
var coll = document.getElementsByClassName("collapsible");
let chatbotEnabled = true;
for (let i = 0; i < coll.length; i++) {
    coll[i].addEventListener("click", function () {
        this.classList.toggle("active");

        var content = this.nextElementSibling;

        if (content.style.maxHeight) {
            content.style.maxHeight = null;
        } else {
            content.style.maxHeight = content.scrollHeight + "px";
        }

    });
}

//MOSTRAR HORA EN EL CHAT
function getTime() {
    let today = new Date();
    hours = today.getHours();
    minutes = today.getMinutes();

    if (hours < 10) {
        hours = "0" + hours;
    }

    if (minutes < 10) {
        minutes = "0" + minutes;
    }

    let time = hours + ":" + minutes;
    return time;
}

// Obtiene el primer mensaje del bot con opciones clickeables
function primerMensajeBot() {
    let welcome = "¡Bienvenido!<br>¿En qué podemos ayudarte?<br>";
    let primerMensaje = '<p class="botText text-align-center"><span>' + welcome + '</span></p>';

    let botHtml = '<p class="botText text-align-center"><span>';
    botHtml += '<button class="option-button" onclick="enviarTextoBoton(\'1\')">1- Información de TicketCR</button><br>';
    botHtml += '<button class="option-button" onclick="enviarTextoBoton(\'2\')">2- Información de los servicios</button><br>';
    botHtml += '<button class="option-button" onclick="enviarTextoBoton(\'3\')">3- Mis boletos a eventos</button><br>';
    botHtml += '<button class="option-button" onclick="enviarTextoBoton(\'4\')">4- Contactarnos</button><br>';
    botHtml += '<button class="option-button" onclick="enviarTextoBoton(\'menu\')">Menú </button><br>';
    botHtml += '<button class="option-button" onclick="enviarTextoBoton(\'salir\')">Salir </button>';

    document.getElementById("botStarterMessage").innerHTML = primerMensaje+ botHtml;

    updateChatTimestamp();
}

// Obtiene la respuesta del bot y muestra la hora actualizada
function obtenerRespuestaDura(userText) {
    let respuestaBot = obtenerRespuestaBot(userText);
    let botHtml = '<p class="botText"><span>' + respuestaBot + '</span></p>';
    $("#chatbox").append(botHtml);

    updateChatTimestamp();
    document.getElementById("chat-bar-bottom").scrollIntoView(true);
}

// Actualiza el timestamp del chat con la hora actualizada
function updateChatTimestamp() {
    let time = getTime();
    $("#chat-timestamp").text(time);
}


// Obtiene la respuesta
function obtenerRespuestaDura(userText) {
    let respuestaBot = obtenerRespuestaBot(userText);
    let botHtml = '<p class="botText"><span>' + respuestaBot + '</span></p>';
    $("#chatbox").append(botHtml);

    document.getElementById("chat-bar-bottom").scrollIntoView(true);
}

// Obtiene el texto del cuadro de entrada y lo procesa
function obtenerRespuesta() {
    let userText = $("#textInput").val().toLowerCase();

    if (userText == "") {
        userText = "";
    }

    let userHtml = '<p class="userText"><span>' + userText + '</span></p>';

    $("#textInput").val("");
    $("#chatbox").append(userHtml);
    document.getElementById("chat-bar-bottom").scrollIntoView(true);

    setTimeout(() => {
        obtenerRespuestaDura(userText);
    }, 1000)
}// Maneja el envío de texto mediante clic en botones
function enviarTextoBoton(textoEjemplo) {
    let userHtml = '<p class="userText"><span>' + textoEjemplo + '</span></p>';

    $("#textInput").val("");
    $("#chatbox").append(userHtml);
    document.getElementById("chat-bar-bottom").scrollIntoView(true);

    obtenerRespuestaDura(textoEjemplo);
}

function enviarBoton() {
    obtenerRespuesta();
}

// Presionar Enter para enviar un mensaje
$("#textInput").keypress(function (e) {
    if (e.which == 13) {
        obtenerRespuesta();
    }
});

// Obtiene la respuesta del bot basada en el input del usuario
function obtenerRespuestaBot(input) {
    switch (input) {
        case '1':
            return 'TicketCR es una compañía nacional que busca fomentar el entretenimiento. Nos distinguimos por nuestra dedicación, innovación y atención al detalle en cada evento que organizamos. Nuestra meta es superar tus expectativas y crear momentos inolvidables.';
        case '2':
            return 'Creamos una plataforma donde puedas tener al alcance los boletos a tus eventos favoritos y también donde puedas vender boletos de los eventos que quieras realizar. Te invitamos a suscribirte y disfrutar de los servicios.';
        case '3':
            ObtenerEventosAsistenciaUsuario(); // Trigger AJAX request
            return 'Eventos para los que compraste boletos: ' //+ ObtenerEventosAsistenciaUsuario();
        case '4':
            return "Puedes escribirnos a nuestro correo electrónico info@ticketCR.com o llamarnos al +506 2222 2222";
        case 'menu':
            let botHtml = '<button class="option-button" onclick="enviarTextoBoton(\'1\')">1- Información de TicketCR</button><br>';
            botHtml += '<button class="option-button" onclick="enviarTextoBoton(\'2\')">2- Información de los servicios</button><br>';
            botHtml += '<button class="option-button" onclick="enviarTextoBoton(\'3\')">3- Mis boletos a eventos</button><br>';
            botHtml += '<button class="option-button" onclick="enviarTextoBoton(\'4\')">4- Contactarnos</button><br>';
            botHtml += '<button class="option-button" onclick="enviarTextoBoton(\'menu\')">Menú </button><br>';
            botHtml += '<button class="option-button" onclick="enviarTextoBoton(\'salir\')">Salir </button>';

            return document.getElementById("botStarterMessage").innerHTML = botHtml;
        case 'salir':
            return "Gracias por usar nuestro servicio. ¡Hasta luego!";
        default:
            return "¡Intenta escribir números válidos!";
    }
}


function ObtenerEventosAsistenciaUsuario(IdUsuario) {
    var IdUsuario = $("#IdUsuario").val();
    
        
    $.ajax({
        url: `https://localhost:7255/api/Evento/ObtenerHistorialEventos?idUsuario=${IdUsuario}`,
        method: 'GET',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json'

    }).done(function (result) {
        let eventsHtml = ''; // Preparar una cadena para contener la información de los eventos

        console.log('Nombre evento', result.nombre)
        if (result.length > 0) {
            for (let i = 0; i < result.length; i++) {
                // Personaliza la forma en que muestras la información de los eventos aquí
                let fechaInicio = result[i].fechaInicio;
                let fechaFormateado = new Date(fechaInicio).toLocaleString('es-ES', {
                    weekday: 'long',
                    year: 'numeric',
                    month: 'long',
                    day: 'numeric',
                    hour: 'numeric',
                    minute: 'numeric',
                    second: 'numeric'
                });

                eventsHtml += '<p class="botText text-align-center"><span>' + result[i].nombre + ' - ' + fechaFormateado + '</span></p>';
            }
        } else {
            
            eventsHtml = '<p class="botText text-align-center"><span>No tienes eventos próximos.</span></p>';
        }

        // Agregar la información de los eventos al cuadro de chat
      
        $("#chatbox").append(eventsHtml);

        // Desplazarse hasta la parte inferior del cuadro de chat
        document.getElementById("chat-bar-bottom").scrollIntoView(true);
    });
}


// Cuando el documento está listo, muestra el primer mensaje del bot
$(document).ready(function () {
    primerMensajeBot();
});

