using DataAccess.Crud;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class BoletoManager
    {
        public void InsertarBoletosParaEvento(int idEvento, List<Boleto> boletos)
        {
            BoletoCrudFactory boletoCrudFactory = new BoletoCrudFactory();

            foreach (var boleto in boletos)
            {
                // Asignar el idEvento correspondiente al boleto
                boleto.IdEvento = idEvento;

                // Insertar el boleto en la base de datos
                boletoCrudFactory.InsertarBoleto(boleto);
            }
        }

        public List<Boleto> ObtenerBoletosEvento(int idEvento)
        {
            BoletoCrudFactory boleto = new BoletoCrudFactory();

            return boleto.RetrieveAllBoletos<Boleto>(idEvento);
        }

        public List<Boleto> ObtenerBoletosPorQr(string codigoQr)
        {
            BoletoCrudFactory boleto = new BoletoCrudFactory();

            return boleto.ObtenerCodigosPorQr<Boleto>(codigoQr);
        }

        public dynamic ValidarBoletoPorQr(string codigoQr)
        {

            BoletoCrudFactory boleto = new BoletoCrudFactory();
            return boleto.ValidarBoletoPorQr(codigoQr);

        }

    }

}
