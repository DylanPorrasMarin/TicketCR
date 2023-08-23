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
    public class LoginCrudFactory : CrudFactory
    {
        private LoginMapper mapper;
        public LoginCrudFactory() : base()
        {
            mapper = new LoginMapper();
            dao = SqlDao.GetInstance();
        }
        public Usuario ValidarUsuario(string Correo, string Clave)
        {
            var operation = mapper.GetValidateUserStatement(Correo, Clave);

            var result = dao.ExecuteStoredProcedureWithQuery(operation);

            // Si se encontró un resultado en la base de datos
            if (result.Count > 0)
            {
                // Mapear los datos del resultado al objeto User
                Usuario user = mapper.MapObjectFromResult(result[0]); // Suponiendo que el resultado es una lista y tomas el primer elemento

                // Devolver el objeto User completo
                return user;
            }

            // Si no se encontró ningún resultado, devolver null o lanzar una excepción
            return null;
        }


        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override T RetrieveByEmail<T>(string correo)
        {
            throw new NotImplementedException();
        }

        public override void Create()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }
    }
}
