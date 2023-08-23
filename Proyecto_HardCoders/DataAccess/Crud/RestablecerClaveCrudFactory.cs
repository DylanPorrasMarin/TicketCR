using DataAccess.Dao;
using DataAccess.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class RestablecerClaveCrudFactory : CrudFactory
    {
        private RestablecerClaveMapper mapper;
        public RestablecerClaveCrudFactory() : base()
        {
            mapper = new RestablecerClaveMapper();
            dao = SqlDao.GetInstance();
        }
     
        public void UpdatePasswordByEmail(string emailAddress, string newPassword)
        {
            var operation = mapper.UpdatePasswordByEmail(emailAddress, newPassword);
            dao.ExecuteStoredProcedure(operation);
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
