using DataAccess.Crud;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class LoginManager
    {
        public Usuario ValidarUsuario(string correo, string clave)
        {
            LoginCrudFactory lf = new LoginCrudFactory();
            var Usuario = lf.ValidarUsuario(correo, clave);
            // Devuelve el objeto User completo si las credenciales son válidas
            return Usuario;
        }
    }
}
