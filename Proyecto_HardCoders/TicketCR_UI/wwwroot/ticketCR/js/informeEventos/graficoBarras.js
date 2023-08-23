// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';
let boletos = []
let eventos = []

function ListarEventos() {
    this.Init = function () {

        this.ListarEventos();

    }


    this.ListarEventos = function () {

        const apiUrl = pathRedireccionBackEnd + '/api/InformeEventos/ObtenerInformeEventos'
        console.log(apiUrl)

        $.ajax({
            url: apiUrl,
            method: 'GET',
            contentType: 'application/json;charset=utf-8',
            dataType: 'json'

        }).done(function (result) {

            console.log(JSON.stringify(result))
            eventos2 = result.map(item => item.nombreEvento);
            boletos = result.map(item => item.boletosVendidos);
            const maximo = Math.max(...boletos);
            tickMax = maximo + 10

            var ctx = document.getElementById("myBarChart");
            var myLineChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: eventos2,
                    datasets: [{
                        label: "Revenue",
                        backgroundColor: "#FF340B",
                        borderColor: "rgba(2,117,216,1)",
                        data: boletos,
                    }],
                },
                options: {
                    scales: {
                        xAxes: [{
                            time: {
                                unit: 'month'
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
                                max: tickMax,
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

            messageInfo('ERROR', 'AJAX FAIL')

        })


    }

}
$(document).ready(function () {
    const view = new ListarEventos();
    view.Init();

});

// Bar Chart Example
