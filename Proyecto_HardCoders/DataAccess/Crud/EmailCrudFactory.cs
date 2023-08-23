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
    public class EmailCrudFactory : CrudFactory
    {
        private EmailMapper mapper;
        public EmailCrudFactory() : base()
        {
            mapper = new EmailMapper();
            dao = SqlDao.GetInstance();
        }
        public override T RetrieveByEmail<T>(string correo)
        {
            var operation = mapper.GetRetrieveByEmailStatement(correo);

            var result = dao.ExecuteStoredProcedureWithQuery(operation);

            // Si se encontró un resultado en la base de datos
            if (result.Count > 0)
            {
                // Mapear los datos del resultado al objeto T
                T objeto = mapper.MapObjectFromResult<T>(result[0]); // Suponiendo que el resultado es una lista y tomas el primer elemento

                // Devolver el objeto completo
                return objeto;
            }

            // Si no se encontró ningún resultado, devolver el valor predeterminado de T
            return default(T);
        }

        public override void Create()
        {
            throw new NotImplementedException();
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
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
    }
}
