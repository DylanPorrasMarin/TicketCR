using DataAccess.Dao;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Mapper
{
    public class ParametroMapper : ICrudStatements, IObjectMapper
    {
        public Parametro BuildObject(Dictionary<string, object> objectRow)
        {
            var parametro = new Parametro();

            if (objectRow.ContainsKey("Id"))
                parametro.Id = int.Parse(objectRow["Id"].ToString());

            if (objectRow.ContainsKey("Impuestos"))
                parametro.Impuestos = objectRow["Impuestos"].ToString();

            if (objectRow.ContainsKey("Comisiones"))
                parametro.Comisiones = objectRow["Comisiones"].ToString();

            return parametro;
        }

        public List<Parametro> BuildObjects(List<Dictionary<string, object>> rows)
        {
            var parametros = new List<Parametro>();

            foreach (var row in rows)
            {
                parametros.Add(BuildObject(row));
            }

            return parametros;
        }

    /*    public SqlOperation InsertarParametro(Parametro parametro)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "InsertarParametro"
            };

            operation.AddVarcharParam("Impuestos", parametro.Impuestos);
            operation.AddVarcharParam("Comisiones", parametro.Comisiones);

            // Output parameter for the generated id
            operation.AddOutputParam("Id", SqlDbType.Int);

            return operation;
        }*/

        public SqlOperation ActualizarParametro(float impuestos, float comisiones)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ActualizarParametros"
            };

            //operation.AddIntegerParam("Id", parametro.Id);
            operation.AddFloatParam("Impuestos", impuestos);
            operation.AddFloatParam("Comisiones", comisiones);

            return operation;
        }

      /*  public SqlOperation ObtenerParametroPorId(int id)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ObtenerParametroPorId"
            };
            operation.AddIntegerParam("Id", id);
            return operation;
        }

        public SqlOperation ObtenerTodosParametros()
        {
            return new SqlOperation()
            {
                ProcedureName = "ObtenerTodosParametros"
            };
        }*/

        public SqlOperation GetRetrieveByEmailStatement(string correo)
        {
            throw new NotImplementedException();
        }

        public Usuario MapObjectFromResult(Dictionary<string, object> result)
        {
            throw new NotImplementedException();
        }

        // Puedes agregar o modificar otras operaciones según las necesidades de Parametro ...
    }
}
