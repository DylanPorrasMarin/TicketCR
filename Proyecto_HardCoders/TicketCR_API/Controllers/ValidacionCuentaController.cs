using AppLogic;
using DataAccess.Crud;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TicketCR_API.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValidacionCuentaController : ControllerBase
    {

        [EnableCors("MyCorsPolicy")]
        [HttpPut]
        public dynamic ActualizarEstadoOTPUsuario(int idUsuario)
        {
            var validacionCuentaManager = new ValidacionCuentaManager();
            return validacionCuentaManager.ActualizarEstadoOTPUsuario(idUsuario);

        }

    }
}
