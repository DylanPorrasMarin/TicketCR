using AppLogic;
using DataAccess.Crud;
using DataAccess.Dao;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace TicketCR_API.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EventoController : ControllerBase
    {
        private EventoManager eventoManager;

        public EventoController()
        {
            eventoManager = new EventoManager();
        }


        //metodo para relaizar la api que obtiene los eventos vigentes, llamando el metodo del EventoManager
        [HttpGet]
        public List<Evento> ObtenerEventoVigentes()
        {

            //AQUI SE MANEJAN LOS TRY CATCH 
            EventoManager em = new EventoManager();

            return em.ObtenerEventoVigentes();
        }


        [HttpPost]
        public IActionResult CrearEvento(Evento evento)
        {
            try
            {
                EventoCrudFactory eventoCrudFactory = new EventoCrudFactory();
                int idEvento = eventoCrudFactory.InsertarEvento(evento);

                // Devolver el idEvento generado en la respuesta
                return Ok(new { idEvento = idEvento, message = "Evento creado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ObtenerEventoYCreador(int idEvento)
        {
            try
            {
                EventoListar evento = eventoManager.ObtenerInformacionTotalEvento(idEvento);
                if (evento != null)
                {
                    return Ok(evento);
                }
                else
                {
                    return NotFound("No se encontró ningún evento con el id especificado.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        public IActionResult ActualizarEvento(int IdEvento, string Nombre, int IdUsuario, string Eslogan, string Descripcion, int Grafica
            , string Modalidad, DateTime FechaInicio, DateTime FechaFinal, string Restricciones, int AforoMaximo, int IdCategoria, float Longitud, float Latitud, string Imagen1, string Imagen2, string Imagen3, string Link)
        {
            try
            {
                eventoManager.ActualizarEvento(IdEvento, Nombre, IdUsuario, Eslogan, Descripcion, Grafica, Modalidad, FechaInicio, FechaFinal,
             Restricciones, AforoMaximo, IdCategoria, Longitud, Latitud, Imagen1, Imagen2, Imagen3, Link);
                return Ok("Evento actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public List<Evento> ObtenerEventosUsuario(int idUsuario)
        {

            //AQUI SE MANEJAN LOS TRY CATCH 
            EventoManager em = new EventoManager();

            return em.ObtenerEventosDelUsuarios(idUsuario);
        }

        [HttpGet]
        public List<Evento> ObtenerEventosAsistenciaUsuario(int idUsuario)
        {

            EventoManager em = new EventoManager();

            return em.ObtenerEventosAsistenciaUsuario(idUsuario);
        }

        [HttpGet]
        public List<Evento> ObtenerHistorialEventos(int idUsuario)
        {

            //AQUI SE MANEJAN LOS TRY CATCH 
            EventoManager em = new EventoManager();

            return em.ObtenerHistorialdeEventos(idUsuario);
        }

        [HttpPut]
        public dynamic ActualizarEstadoEvento(int idEvento, bool estado)
        {
            //AQUI SE MANEJAN LOS TRY CATCH 
            EventoManager em = new EventoManager();
            return em.ActualizarEstadoEvento(idEvento, estado);
        }


    }
}
