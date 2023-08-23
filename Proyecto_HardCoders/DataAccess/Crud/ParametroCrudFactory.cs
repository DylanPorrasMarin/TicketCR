using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using System;
using System.Collections.Generic;

namespace DataAccess.Crud
{
    public class ParametroCrudFactory : CrudFactory
    {
        private ParametroMapper mapper;

        public ParametroCrudFactory() : base()
        {
            mapper = new ParametroMapper();
            dao = SqlDao.GetInstance();
        }

  /*      public int InsertarParametro(Parametro parametro)
        {
            SqlOperation operation = mapper.InsertarParametro(parametro);
            dao.ExecuteStoredProcedure(operation);
            int idParametroCreado = Convert.ToInt32(operation.GetParameterValue("idParametro"));
            return idParametroCreado;
        }*/

        public int ActualizarParametro(float impuestos, float comisiones)
        {
            SqlOperation operation = mapper.ActualizarParametro(impuestos, comisiones);
            dao.ExecuteStoredProcedure(operation);
            int idParametro = Convert.ToInt32(operation.GetParameterValue("idParametro"));
            return idParametro;
        }

   /*     public Parametro ObtenerParametroPorId(int idParametro)
        {
            SqlOperation operation = mapper.ObtenerParametroPorId(idParametro);
            var result = dao.ExecuteStoredProcedureWithQuery(operation);

            if (result.Count > 0)
            {
                return mapper.BuildObject(result[0]);
            }

            return null; // Si no se encuentra el parametro, se devuelve null
        }*/

   /*     public List<Parametro> ObtenerTodosParametros()
        {
            List<Parametro> lstResults = new List<Parametro>();
            SqlOperation operation = mapper.ObtenerTodosParametros();
            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(operation);
            if (dataResults.Count > 0)
            {
                var dtoObjects = mapper.BuildObjects(dataResults);

                foreach (var ob in dtoObjects)
                {
                    lstResults.Add((Parametro)Convert.ChangeType(ob, typeof(Parametro)));
                }
            }
            return lstResults;
        }*/

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

        public override void Create()
        {
            throw new NotImplementedException();
        }
    }
}
