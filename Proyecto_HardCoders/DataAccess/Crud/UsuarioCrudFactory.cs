using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class UsuarioCrudFactory : CrudFactory
    {
        private UsuarioMapper mapper;

        public UsuarioCrudFactory() : base()
        {
            mapper = new UsuarioMapper();
            dao = SqlDao.GetInstance();
        }

        public int InsertarUsuario(Usuario usuario)
        {
            SqlOperation operation = mapper.InsertarUsuario(usuario);
            dao.ExecuteStoredProcedure(operation);
            int idUsuarioCreado = Convert.ToInt32(operation.GetParameterValue("idUsuario"));
            return idUsuarioCreado;
        }

        public int InsertarUsuarioDesdeAdmin(Usuario usuario)
        {
            SqlOperation operation = mapper.InsertarUsuarioDesdeAdmin(usuario);
            dao.ExecuteStoredProcedure(operation);
            int idUsuarioCreado = Convert.ToInt32(operation.GetParameterValue("idUsuario"));
            return idUsuarioCreado;
        }

        public dynamic ActualizarEstadoUsuario(int idUsuario, bool estado)
        {
            SqlOperation operation = mapper.ActualizarEstadoUsuario(idUsuario, estado);
            dao.ExecuteStoredProcedure(operation);

            return new { exito = true };


        }


        public int ActualizarUsuario(Usuario usuario)
        {
            SqlOperation operation = mapper.ActualizarUsuario(usuario);
            dao.ExecuteStoredProcedure(operation);
            int idUsuario = Convert.ToInt32(operation.GetParameterValue("idUsuario"));
            return idUsuario;
        }

        public Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            SqlOperation operation = mapper.ObtenerUsuarioPorId(idUsuario);
            var result = dao.ExecuteStoredProcedureWithQuery(operation);

            if (result.Count > 0)
            {
                return mapper.BuildObject(result[0]);
            }

            return null; // Si no se encuentra el usuario, se devuelve null
        }

        //memtodo
        public bool VerificarCorreoRegistrado(string correo)
        {
            bool existe;
            var operation = mapper.VerificarCorreoIdentico(correo);
            var result = dao.ExecuteStoredProcedureWithQuery(operation);

            // Verificar si el resultado contiene filas y obtener el valor del campo "Existe"
            existe = result.Count > 0 && Convert.ToInt32(result[0]["Existe"]) == 1;
            return existe;
        }

        public List<Usuario> ObtenerTodosUsuarios()
        {
            List <Usuario> lstResults= new List<Usuario>();
            SqlOperation operation = mapper.ObtenerUsuarios();
            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(operation);
            if (dataResults.Count > 0)
            {
                var dtoObjects = mapper.BuildObjects(dataResults);

                foreach (var ob in dtoObjects)
                {
                    lstResults.Add((Usuario)Convert.ChangeType(ob, typeof(Usuario)));
                }
            }
            return lstResults;
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public List<T> RetrieveAllUsuarios<T>()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override T RetrieveByEmail<T>(string correo)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public override void Create()
        {
            throw new NotImplementedException();
        }
    }
}
