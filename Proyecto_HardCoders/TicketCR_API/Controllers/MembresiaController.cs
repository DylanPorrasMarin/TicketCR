using AppLogic;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TicketCR_API.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MembresiaController : ControllerBase
    {
        private MembresiaManager membresiaManager;

        public MembresiaController()
        {
            membresiaManager = new MembresiaManager();
        }
        [HttpGet]
        public IActionResult ObtenerMembresiaPorId(int idMembresia)
        {
            Membresia membresia = membresiaManager.ObtenerMembresiaPorId(idMembresia);
            if (membresia == null)
            {
                return NotFound(); // Si no se encuentra la membresía, retornar un 404 Not Found
            }

            return Ok(membresia);
        }
        [HttpGet]
        public List<Membresia> ObtenerMembresias()
        {

            //AQUI SE MANEJAN LOS TRY CATCH 
            MembresiaManager em = new MembresiaManager();

            return em.ObtenerMembresias();
        }

        [HttpPost]
        public dynamic CrearMembresia(string nombreMembresia, float costo, int cantidadEventos, int cantidadBoletos)
        {
            //AQUI SE MANEJAN LOS TRY CATCH 
            MembresiaManager em = new MembresiaManager();

            return em.CrearMembresia(nombreMembresia, costo, cantidadEventos, cantidadBoletos);
        }

        [HttpPut]
        public dynamic ActualizarEstadoMembresia(int idMembresia, bool estado)
        {
            //AQUI SE MANEJAN LOS TRY CATCH 
            MembresiaManager em = new MembresiaManager();

            return em.ActualizarEstadoMembresia(idMembresia, estado);
        }
    }
}
