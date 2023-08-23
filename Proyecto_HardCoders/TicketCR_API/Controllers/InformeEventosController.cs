using AppLogic;
using DataAccess.Crud;
using DataAccess.Dao;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace TicketCR_API.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class InformeEventosController : Controller
    {
      

        [HttpGet]
        public List<InformeEventos> ObtenerInformeEventos()
        {

            //AQUI SE MANEJAN LOS TRY CATCH 
            InformeEventosManager iem = new InformeEventosManager();

            return iem.ObtenerInformeEventos();
        }
        [HttpGet]
        public List<InformeMembresias> ObtenerInformeMembresias()
        {

            //AQUI SE MANEJAN LOS TRY CATCH 
            InformeEventosManager iem = new InformeEventosManager();

            return iem.ObtenerInformeMembresias();
        }
        [HttpGet]
        public List<InformeEventos> ObtenerInformeEventosGestor(int idUsuario)
        {

            //AQUI SE MANEJAN LOS TRY CATCH 
            InformeEventosManager iem = new InformeEventosManager();

            return iem.ObtenerInformeEventosGestor(idUsuario);
        }
    }
}
