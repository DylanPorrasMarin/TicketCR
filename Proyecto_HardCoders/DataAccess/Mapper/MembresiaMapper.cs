using DataAccess.Dao;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class MembresiaMapper : ICrudStatements, IObjectMapper
    {

        public SqlOperation CrearMembresia(string nombreMembresia,float costo, int cantidadEventos, int cantidadBoletos)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "CrearMembresia"
            };

            operation.AddVarcharParam("nombreMembresia", nombreMembresia);
            operation.AddIntegerParam("cantidadEventos", cantidadEventos);
            operation.AddIntegerParam("cantidadBoletos", cantidadBoletos);
            operation.AddFloatParam("costo", costo);

            return operation;
        }
        public SqlOperation MembresiaPorId(int idMembresia)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ObtenerMembresiaPorId"
            };

            operation.AddIntegerParam("idMembresia", idMembresia);

            return operation;
        }

        public Membresia MapObjectFromResult(Dictionary<string, object> result)
        {
            Membresia membresia = new Membresia();

            if (result.ContainsKey("idMembresia"))
                membresia.IdMembresia = int.Parse(result["idMembresia"].ToString());

            if (result.ContainsKey("nombreMembresia"))
                membresia.NombreMembresia = result["nombreMembresia"].ToString().Trim();

            if (result.ContainsKey("cantidadEventos"))
                membresia.CantidadEventos = int.Parse(result["cantidadEventos"].ToString());

            if (result.ContainsKey("cantidadBoletos"))
                membresia.CantidadBoletos = int.Parse(result["cantidadBoletos"].ToString());

            if (result.ContainsKey("costo"))
                membresia.Costo = float.Parse(result["costo"].ToString());

            if (result.ContainsKey("estado"))
                membresia.Estado = bool.Parse(result["estado"].ToString());


            return membresia;
        }



        public SqlOperation ObtenerMembresias()
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ObtenerMembresias"
            };


            return operation;
        }

        public Membresia BuildObject(Dictionary<string, object> objectRow)
        {
            Membresia membresia = new Membresia();
            if (objectRow.ContainsKey("idMembresia"))
                membresia.IdMembresia = int.Parse(objectRow["idMembresia"].ToString());
            if (objectRow.ContainsKey("nombreMembresia"))
                membresia.NombreMembresia = objectRow["nombreMembresia"].ToString();
            if (objectRow.ContainsKey("cantidadEventos"))
                membresia.CantidadEventos = int.Parse(objectRow["cantidadEventos"].ToString());
            if (objectRow.ContainsKey("cantidadBoletos"))
                membresia.CantidadBoletos = int.Parse(objectRow["cantidadBoletos"].ToString());
            if (objectRow.ContainsKey("impuestos"))
                membresia.Impuestos = float.Parse(objectRow["impuestos"].ToString());
            if (objectRow.ContainsKey("costo"))
                membresia.Costo = float.Parse(objectRow["costo"].ToString());

            if (objectRow.ContainsKey("estado"))
                membresia.Estado = bool.Parse(objectRow["estado"].ToString());
            

            return membresia;
        }
        public List<Membresia> BuildObjects(List<Dictionary<string, object>> objectRows)
        {
            var lstResult = new List<Membresia>();

            foreach (var objRow in objectRows)
            {
                var membresia = BuildObject(objRow);
                lstResult.Add(membresia);
            }
            return lstResult;
        }


        public FacturaMembresia MapearFacturaMembreia(Dictionary<string, object> result)
        {
            FacturaMembresia membresia = new FacturaMembresia();

            if (result.ContainsKey("idMembresia"))
                membresia.IdNuevaMembresia = int.Parse(result["idMembresia"].ToString());

            if (result.ContainsKey("nombreMembresia"))
                membresia.NombreMembresia = result["nombreMembresia"].ToString().Trim();

            if (result.ContainsKey("costo"))
                membresia.CostoTotal = float.Parse(result["costo"].ToString());

            return membresia;
        }

        public SqlOperation ActualizarMembresiaUsuarioYRegistrarCompra(int IdNuevaMembresia, float CostoTotal,float SubTotal, float Impuestos,int idUsuario, string IdPagoPaypal)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ActualizarMembresiaUsuarioYRegistrarCompra"
            };

            operation.AddIntegerParam("idUsuario", idUsuario);
            operation.AddIntegerParam("idNuevaMembresia ",IdNuevaMembresia);
            operation.AddFloatParam("total ", CostoTotal);
            operation.AddFloatParam("subTotal ",SubTotal);
            operation.AddFloatParam("impuesto ",Impuestos);
            operation.AddVarcharParam("idPagoPaypal ",IdPagoPaypal);

            return operation;
        }

        public SqlOperation ActualizarEstadoMembresia(int idMembresia, bool estado)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ActualizarEstadoMembresia"
            };
            operation.AddIntegerParam("idMembresia", idMembresia);
            operation.AddBooleanParam("nuevoEstado", estado);
            return operation;
        }




        public SqlOperation GetRetrieveByEmailStatement(string correo)
        {
            throw new NotImplementedException();
        }



        Usuario IObjectMapper.MapObjectFromResult(Dictionary<string, object> result)
        {
            throw new NotImplementedException();
        }
    }
}
