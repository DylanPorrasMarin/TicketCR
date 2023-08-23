using DataAccess.Dao;
using DataAccess.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class ValidacionCuentaCrudFactory : CrudFactory
    {
        private ValidarCuentaMapper mapper;

        public ValidacionCuentaCrudFactory() : base()
        {
            mapper = new ValidarCuentaMapper();
            dao = SqlDao.GetInstance();
        }



        public dynamic ActualizarEstadoOTPUsuario(int idUsuario)
        {
            var operation = mapper.ActualizarEstadoOTPUsuario(idUsuario);

            dao.ExecuteStoredProcedure(operation);
            return new

                { estadoOTP = false,
                  estadoUsuario = true
                };
           
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

        public override T RetrieveByEmail<T>(string correo)
        {
            throw new NotImplementedException();
        }

        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }
    }
}
