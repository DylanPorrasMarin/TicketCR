
$(document).ready(function () {
    new DataTable('#datatablesSimple', {
        "lengthMenu": [5, 10, 25, 50] // Opciones de filtrado
    });

});

window.addEventListener('DOMContentLoaded', event => {
    // Simple-DataTables
    // https://github.com/fiduswriter/Simple-DataTables/wiki

    const datatablesSimple = document.getElementById('datatablesSimple');
    if (datatablesSimple) {
        new simpleDatatables.DataTable(datatablesSimple);
    }
});
