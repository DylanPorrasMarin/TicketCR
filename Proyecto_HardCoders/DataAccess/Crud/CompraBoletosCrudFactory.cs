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
    public class CompraBoletosCrudFactory : CrudFactory
    {
        private CompraBoletosEvento mapper;
      

        public CompraBoletosCrudFactory() : base()
        {
            mapper = new CompraBoletosEvento();
            dao = SqlDao.GetInstance();
        }



        public dynamic InsertarCompraBoletosVirtuales(CompraBoletosVirtuales compraBoletosVirtuales)
        {


            var operation = mapper.InsertarCompraBoletosVirtuales(compraBoletosVirtuales);

            dao.ExecuteStoredProcedure(operation);

            return new { success = true };
        }


        public dynamic InsertarCompraBoletosPresenciales(CompraBoletosPresenciales compraBoletosPresenciales)
        {


            var operation = mapper.InsertarCompraBoletosPresenciales(compraBoletosPresenciales);

            dao.ExecuteStoredProcedure(operation);

            return new { success = true };
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

        public override T RetrieveByEmail<T>(string correo)
        {
            throw new NotImplementedException();
        }

        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
