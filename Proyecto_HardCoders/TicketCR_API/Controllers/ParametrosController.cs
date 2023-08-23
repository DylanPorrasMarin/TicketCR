using AppLogic;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System; // Para el manejo de excepciones

namespace TicketCR_API.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ParametrosController : Controller
    {
        private ParametroManager parametroManager;

        public ParametrosController()
        {
            parametroManager = new ParametroManager();
        }

    /*    [HttpPost]
        public IActionResult InsertarParametro(Parametro parametro)
        {
            try
            {
                int idParametroCreado = parametroManager.InsertarParametro(parametro);
                return Ok(new { parametro = idParametroCreado, message = "Parametro creado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/


        [HttpPost]
        public IActionResult ActualizarParametro(float impuestos, float comisiones)
        {
            try
            {
                parametroManager.ActualizarParametro(impuestos, comisiones);
                return Ok(new { message = "Parametros actualizados con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

 /*       [HttpGet]
        public IActionResult ObtenerParametroPorId(int id)
        {
            try
            {
                var parametro = parametroManager.ObtenerParametroPorId(id);

                if (parametro != null)
                {
                    return Ok(parametro);
                }
                else
                {
                    return NotFound("Parametro no encontrado");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
*/
        // Puedes agregar más métodos según sea necesario para el ParametrosController...
    }
}
