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
    public class RestablecerClaveController : ControllerBase
    {
   
        [EnableCors("MyCorsPolicy")]
        [HttpPost]
        public IActionResult UpdatePassword(string email, string newPassword)
        {
            // Aquí puedes agregar validaciones adicionales, como verificar que el correo y la contraseña no sean nulos o vacíos, etc.

            // Actualizar la contraseña en la base de datos
            var restablecerClaveManager = new RestablecerClaveManager();
            restablecerClaveManager.UpdatePasswordByEmail(email, newPassword);

            // Devolver una respuesta exitosa
            return Ok("Contraseña actualizada con éxito");
        }
    }
}
