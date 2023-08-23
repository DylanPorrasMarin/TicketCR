using AppLogic;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TicketCR_API.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        [EnableCors("MyCorsPolicy")]
        [HttpGet]
        public dynamic ValidateUser([FromQuery] string correo, [FromQuery] string clave)
        {
            LoginManager loginManager = new LoginManager();

            Usuario user = loginManager.ValidarUsuario(correo, clave);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                // Usuario inválido, devolver un mensaje de error
                return BadRequest("Usuario inválido. Verifique las credenciales.");
            }
        }

    }
}
