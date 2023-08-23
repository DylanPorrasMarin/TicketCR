using DataAccess.Crud;
using DataAccess.Dao;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class UsuarioManager
    {
        private UsuarioCrudFactory usuarioCrudFactory;

        public UsuarioManager()
        {
            usuarioCrudFactory = new UsuarioCrudFactory();
        }
     

        public void ActualizarUsuario(Usuario usuario)
        {
            usuarioCrudFactory.ActualizarUsuario(usuario);
        }

        public Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            return usuarioCrudFactory.ObtenerUsuarioPorId(idUsuario);
        }

        public int InsertarUsuario(Usuario usuario)
        {
            UsuarioCrudFactory usuarioCrud = new UsuarioCrudFactory();
            return usuarioCrud.InsertarUsuario(usuario);
        }

        public int InsertarUsuarioDesdeAdmin(Usuario usuario)
        {
            UsuarioCrudFactory usuarioCrud = new UsuarioCrudFactory();
            return usuarioCrud.InsertarUsuarioDesdeAdmin(usuario);
        }

        public bool VerificarCorreoExistente(string correoUsuario)
        {
            return usuarioCrudFactory.VerificarCorreoRegistrado(correoUsuario);
        }

        public List<Usuario> ObtenerUsuariosRegistrados()
        {
            return usuarioCrudFactory.ObtenerTodosUsuarios();
        }
        public dynamic ActualizarEstadoUsuario(int idUsuario, bool estado)
        {
            return usuarioCrudFactory.ActualizarEstadoUsuario(idUsuario, estado);

        }
    }
}
