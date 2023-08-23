// Función para buscar en la tabla
//function searchTable() {
//    var input, filter, table, tr, td, i, txtValue;
//    input = document.getElementById("searchInput");
//    filter = input.value.toUpperCase();
//    table = document.getElementById("datatablesSimple");
//    tr = table.getElementsByTagName("tr");

//    for (i = 0; i < tr.length; i++) {
//        td = tr[i].getElementsByTagName("td")[1]; // Columna donde realizaremos la búsqueda (en este caso, la primera columna)

//        if (td) {
//            txtValue = td.textContent || td.innerText;

//            if (txtValue.toUpperCase().indexOf(filter) > -1) {
//                tr[i].style.display = "";
//            } else {
//                tr[i].style.display = "none";
//            }
//        }
//    }
//}

//function editarfila(id) {
//    // aquí puedes implementar la lógica para editar la fila con el id proporcionado.
//    // por ejemplo, puedes abrir un formulario de edición con los datos actuales de la fila.
//    alert("editar fila con id: " + id);
//}

// obtener referencia al botón mediante su clase
//const botoneseliminar = document.getelementsbyclassname('eliminar-btn');
//for (const boton of botoneseliminar) {
//    boton.addeventlistener('click', function () {
//        // mostrar el mensaje cuando se hace clic en el botón
//        confirmacionproceso("eliminar evento", "segruro que desea eliminar el evento", "eliminado", "evento eliminado de manera correcta");
//    });
//}


//// Agregar evento de escucha para el campo de entrada de búsqueda
//document.getElementById("searchInput").addEventListener("keyup", searchTable);
$(document).ready(function () {
    new DataTable('#datatablesSimple', {
        "lengthMenu": [5, 10, 25, 50], // Opciones de filtrado
        "language": {
            processing: "Procesando...",
            search: "Buscar&nbsp;:",
            lengthMenu: "Mostrar _MENU_ elementos",
            info: "Mostrando de _START_ a _END_ de _TOTAL_ elementos totales",
            infoEmpty: "Mostrando el elemento 0 al 0 de 0 elementos",
            infoFiltered: "(filtrado de _MAX_ elementos en total)",
            infoPostFix: "",
            loadingRecords: "Cargando...",
            zeroRecords: "Ningún elemento para mostrar",
            emptyTable: "No hay datos disponibles en la tabla",
            paginate: {
                first: "Primero",
                previous: "Anterior",
                next: "Siguiente",
                last: "Último"
            },
            aria: {
                sortAscending: ": activar para ordenar la columna de forma ascendente",
                sortDescending: ": activar para ordenar la columna de forma descendente"
            }
        }
    });

});





