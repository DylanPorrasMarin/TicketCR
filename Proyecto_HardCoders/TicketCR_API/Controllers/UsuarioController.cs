using AppLogic;
using DTO;
using DataAccess.Crud;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TicketCR_API.Controllers
{

    [EnableCors("MyCorsPolicy")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsuarioController : Controller
    {
        private UsuarioManager usuarioManager;

        public UsuarioController()
        {
            usuarioManager = new UsuarioManager();
        }

        [HttpPost]
        public IActionResult InsertarUsuario(Usuario usuario)
        {
            try
            {
                int idUsuarioCreado = usuarioManager.InsertarUsuario(usuario);

                // Devolver el usuario actualizado en la respuesta
                return Ok(new { usuario = idUsuarioCreado, message = "Usuario creado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult InsertarUsuarioDesdeAdmin(Usuario usuario)
        {
            try
            {
                int idUsuarioCreado = usuarioManager.InsertarUsuarioDesdeAdmin(usuario);

                // Devolver el usuario actualizado en la respuesta
                return Ok(new { usuario = idUsuarioCreado, message = "Usuario desde admin creado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
       public IActionResult ActualizarUsuario([FromBody] Usuario usuario)
       {
            try
            {
                UsuarioCrudFactory usuarioCrudFactory = new UsuarioCrudFactory();
                int usuarioActualizado = usuarioCrudFactory.ActualizarUsuario(usuario);

                // Devolver el usuario actualizado en la respuesta
                return Ok(new { usuario = usuarioActualizado, message = "Usuario actualizado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public dynamic ActualizarEstadoUsuario(int idUsuario, bool estado)
        {
            //AQUI SE MANEJAN LOS TRY CATCH 
            UsuarioManager eu = new UsuarioManager();

            return eu.ActualizarEstadoUsuario(idUsuario,estado);
        }

        [HttpGet]
        public IActionResult ObtenerUsuarioPorId(int id)
        {
            try
            {
                var usuario = usuarioManager.ObtenerUsuarioPorId(id);

                if (usuario != null)
                {
  
                    return Ok(usuario);
                }
                else
                {
                    return NotFound("Usuario no encontrado");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult VerificarCorreoExistente(string correoUsuario)
        {
            try
            {
                bool correoExistente = usuarioManager.VerificarCorreoExistente(correoUsuario);

                // Devolver la respuesta en formato JSON
                return Ok(new { existe = correoExistente });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public List<Usuario> ObtenerUsuariosRegistrados()
        {
          return  usuarioManager.ObtenerUsuariosRegistrados(); 
        }

    }
}
