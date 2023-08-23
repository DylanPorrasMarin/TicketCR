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
    public class BoletoCrudFactory : CrudFactory
    {
        private BoletoMapper mapper;
 

        public BoletoCrudFactory() : base()
        {
            mapper = new BoletoMapper();
            dao = SqlDao.GetInstance();
        }

        public void InsertarBoleto(Boleto boleto)
        {
            SqlOperation operation = mapper.InsertarBoleto(boleto);
            dao.ExecuteStoredProcedure(operation);
        }
        public List<T> RetrieveAllBoletos<T>(int idEvento)
        {
            List<T> lstResults = new List<T>();
            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(mapper.ObtenerBoletosEvento(idEvento));

            if (dataResults.Count > 0)
            {
                var dtoObjects = mapper.BuildObjects(dataResults);

                foreach (var ob in dtoObjects)
                {
                    lstResults.Add((T)Convert.ChangeType(ob, typeof(T)));
                }
            }
            return lstResults;
        }

        public List<T> ObtenerCodigosPorQr<T>(string codigoQr)
        {
            List<T> lstResults = new List<T>();
            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(mapper.ObtenerBoletosPorQr(codigoQr));

            if (dataResults.Count > 0)
            {
                var dtoObjects = mapper.BuildObjects(dataResults);

                foreach (var ob in dtoObjects)
                {
                    lstResults.Add((T)Convert.ChangeType(ob, typeof(T)));
                }
            }
            return lstResults;
        }

        public dynamic ValidarBoletoPorQr(string codigoQr)
        {
      
            var operation =mapper.ValidarBoletoPorQr(codigoQr);
            dao.ExecuteStoredProcedure(operation);

            return new { exito = "Boleto validado y cambiado de estado a false" };

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
