// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';
let ganancias = []
let eventos = []
let boletos = []


function ListarEventosLineal() {
    this.Init = function () {

        this.ListarEventosLineal();

    }

    let rolUsuario = $('#rol-usuario').val();
    let idUsuario = parseInt($('#id-usuario').val());

    if (rolUsuario === "1") {

        this.ListarEventosLineal = function () {

            const apiUrl = pathRedireccionBackEnd + '/api/InformeEventos/ObtenerInformeEventos'
            console.log(apiUrl)

            $.ajax({
                url: apiUrl,
                method: 'GET',
                contentType: 'application/json;charset=utf-8',
                dataType: 'json'

            }).done(function (result) {

                console.log(JSON.stringify(result))
                eventos = result.map(item => item.nombreEvento);
                ganancias = result.map(item => item.ganacias);
                const maximo = Math.max(...ganancias);
                tickMax = parseInt(maximo, 10) + 100

                // Area Chart Example
                var ctx = document.getElementById("myAreaChart");
                var myLineChart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: eventos,
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

                boletos = result.map(item => item.boletosVendidos);
                const maximo2 = Math.max(...boletos);
                tickMax2 = maximo2 + 10

                var ctx = document.getElementById("myBarChart");
                var myLineChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: eventos,
                        datasets: [{
                            label: "Boletos Vendidos",
                            backgroundColor: "#FF340B",
                            borderColor: "rgba(2,117,216,1)",
                            data: boletos,
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


    } else {
        this.ListarEventosLineal = function () {

            const apiUrl = `${pathRedireccionBackEnd}/api/InformeEventos/ObtenerInformeEventosGestor?idUsuario=${idUsuario}`
            console.log(apiUrl)

            $.ajax({
                url: apiUrl,
                method: 'GET',
                contentType: 'application/json;charset=utf-8',
                dataType: 'json'

            }).done(function (result) {

                console.log(JSON.stringify(result))
                eventos = result.map(item => item.nombreEvento);
                ganancias = result.map(item => item.ganacias);
                const maximo = Math.max(...ganancias);
                tickMax = parseInt(maximo, 10) + 100

                // Area Chart Example
                var ctx = document.getElementById("myAreaChart");
                var myLineChart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: eventos,
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

                boletos = result.map(item => item.boletosVendidos);
                const maximo2 = Math.max(...boletos);
                tickMax2 = maximo2 + 10

                var ctx = document.getElementById("myBarChart");
                var myLineChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: eventos,
                        datasets: [{
                            label: "Boletos Vendidos",
                            backgroundColor: "#FF340B",
                            borderColor: "rgba(2,117,216,1)",
                            data: boletos,
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
}
$(document).ready(function () {
    new DataTable('#datatablesSimple', {
        "lengthMenu": [5, 10, 25, 50] // Opciones de filtrado
    });

    const view = new ListarEventosLineal();
    view.Init();

});


