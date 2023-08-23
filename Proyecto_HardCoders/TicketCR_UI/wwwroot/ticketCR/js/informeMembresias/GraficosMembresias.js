// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';
let membresias = []
let ganancias = []
let membresiasAdq = []


function ListarEventosLineal() {
    this.Init = function () {

        this.ListarEventosLineal();

    }


 

        this.ListarEventosLineal = function () {

            const apiUrl = pathRedireccionBackEnd + '/api/InformeEventos/ObtenerInformeMembresias'
            console.log(apiUrl)

            $.ajax({
                url: apiUrl,
                method: 'GET',
                contentType: 'application/json;charset=utf-8',
                dataType: 'json'

            }).done(function (result) {

                console.log(JSON.stringify(result))
                membresias = result.map(item => item.nombreMembresia);
                ganancias = result.map(item => item.ganancias);
                const maximo = Math.max(...ganancias);
                tickMax = parseInt(maximo, 10) + 100

                // Area Chart Example
                var ctx = document.getElementById("myAreaChart");
                var myLineChart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: membresias,
                        datasets: [{
                            label: "Ganancias",
                            lineTension: 0.3,
                            backgroundColor: "rgba(2,117,216,0.2)",
                            borderColor: "rgba(2,117,216,1)",
                            pointRadius: 5,
                            pointBackgroundColor: "rgba(2,117,216,1)",
                            pointBorderColor: "rgba(255,255,255,0.8)",
                            pointHoverRadius: 5,
                            pointHoverBackgroundColor: "rgba(2,117,216,1)",
                            pointHitRadius: 50,
                            pointBorderWidth: 2,
                            data: ganancias
                        }],
                    },
                    options: {
                        scales: {
                            xAxes: [{
                                time: {
                                    unit: 'Eventos'
                                },
                                gridLines: {
                                    display: false
                                },
                                ticks: {
                                    maxTicksLimit: 7
                                }
                            }],
                            yAxes: [{
                                ticks: {
                                    min: 0,
                                    max: tickMax,
                                    maxTicksLimit: 5
                                },
                                gridLines: {
                                    color: "rgba(0, 0, 0, .125)",
                                }
                            }],
                        },
                        legend: {
                            display: false
                        }
                    }
                });

                membresiasAdq = result.map(item => item.membresiasAd);
                const maximo2 = Math.max(...membresiasAdq);
                tickMax2 = maximo2 + 10

                var ctx = document.getElementById("myBarChart");
                var myLineChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: membresias,
                        datasets: [{
                            label: "Membresias Adquiridas",
                            backgroundColor: "#FF340B",
                            borderColor: "rgba(2,117,216,1)",
                            data: membresiasAdq,
                        }],
                    },
                    options: {
                        scales: {
                            xAxes: [{
                                time: {
                                    unit: 'Membresias'
                                },
                                gridLines: {
                                    display: false
                                },
                                ticks: {
                                    maxTicksLimit: 6
                                }

                            }],
                            yAxes: [{
                                ticks: {
                                    min: 0,
                                    max: tickMax2,
                                    maxTicksLimit: 5
                                },
                                gridLines: {
                                    display: true
                                }
                            }],
                        },
                        legend: {
                            display: false
                        }
                    }
                });




            }).fail(function () {

                messageError('ERROR', 'AJAX FAIL')

            })
        }


    

    
}
$(document).ready(function () {
    new DataTable('#datatablesSimple', {
        "lengthMenu": [5, 10, 25, 50] // Opciones de filtrado
    });

    const view = new ListarEventosLineal();
    view.Init();

});


