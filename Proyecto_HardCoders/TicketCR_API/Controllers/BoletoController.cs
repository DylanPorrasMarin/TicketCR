using AppLogic;
using DataAccess.Crud;
using DataAccess.Mapper;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TicketCR_API.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BoletoController : ControllerBase
    {
        [HttpPost]
        public IActionResult InsertarBoletos(int idEvento, List<Boleto> boletos)
        {
            try
            {
                BoletoManager boletoManager = new BoletoManager();

                // Asignar el idEvento correspondiente a cada boleto en la lista
                foreach (var boleto in boletos)
                {
                    boleto.IdEvento = idEvento;
                }

                // Insertar los boletos en la base de datos
                boletoManager.InsertarBoletosParaEvento(idEvento, boletos);

                return Ok("Boletos creados con éxito");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public List<Boleto> ObtenerBoletosEvento(int idEvento)
        {

            //AQUI SE MANEJAN LOS TRY CATCH 
            BoletoManager bm = new BoletoManager();

            return bm.ObtenerBoletosEvento(idEvento);
        }

        [HttpGet]
        public List<Boleto> ObtenerBoletosPorQr(string codigoQr)
        {

            //AQUI SE MANEJAN LOS TRY CATCH 
            BoletoManager bm = new BoletoManager();

            return bm.ObtenerBoletosPorQr(codigoQr);
        }

        [HttpPut]
        public dynamic ValidarBoletoPorQr(string codigoQr)
        {
            //AQUI SE MANEJAN LOS TRY CATCH 
            BoletoManager bm = new BoletoManager();

            return bm.ValidarBoletoPorQr(codigoQr);
        }
    }
}
