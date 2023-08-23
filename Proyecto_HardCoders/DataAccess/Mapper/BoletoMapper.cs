using DataAccess.Dao;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class BoletoMapper : ICrudStatements, IObjectMapper
    {
        public SqlOperation InsertarBoleto(Boleto boleto)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "InsertarBoleto"
            };
            // Agregar aquí los parámetros necesarios para el Stored Procedure
            operation.AddIntegerParam("IdEvento", boleto.IdEvento);
            operation.AddVarcharParam("TipoBoleto", boleto.TipoBoleto);
            operation.AddVarcharParam("codigoQr", boleto.CodigoQr);
            operation.AddFloatParam("Costo", boleto.Costo);

            return operation;
        }
        public SqlOperation ObtenerBoletosEvento(int idEvento)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ObtenerBoletosPorEvento"
            };

            // Agregar aquí los parámetros necesarios para el Stored Procedure
            operation.AddIntegerParam("IdEvento", idEvento);

            return operation;
        }



        public SqlOperation ObtenerBoletosPorQr(string codigoQr)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ObtenerBoletoPorCodigoQr"
            };

            // Agregar aquí los parámetros necesarios para el Stored Procedure
            operation.AddVarcharParam("CodigoQr", codigoQr);

            return operation;
        }






        public SqlOperation ValidarBoletoPorQr(string codigoQr)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ActualizarEstadoBoletoPorCodigoQr"
            };

            // Agregar aquí los parámetros necesarios para el Stored Procedure
            operation.AddVarcharParam("CodigoQr", codigoQr);

            return operation;
        }




        public Boleto BuildObject(Dictionary<string, object> objectRow)
        {
            var boleto = new Boleto();

            if (objectRow.ContainsKey("idBoleto"))
                boleto.IdBoleto = int.Parse(objectRow["idBoleto"].ToString());
            if (objectRow.ContainsKey("idEvento"))
                boleto.IdEvento = int.Parse(objectRow["idEvento"].ToString());
            if (objectRow.ContainsKey("asiento"))
                boleto.Asiento = objectRow["asiento"].ToString();
            if (objectRow.ContainsKey("codigoQR"))
                boleto.CodigoQr = objectRow["codigoQR"].ToString();
            if (objectRow.ContainsKey("tipoBoleto"))
                boleto.TipoBoleto = objectRow["tipoBoleto"].ToString();
            if (objectRow.ContainsKey("costo"))
                boleto.Costo = float.Parse(objectRow["costo"].ToString());
            if (objectRow.ContainsKey("impuesto"))
                boleto.Impuesto = float.Parse(objectRow["impuesto"].ToString());
            if (objectRow.ContainsKey("comision"))
                boleto.Comision = float.Parse(objectRow["comision"].ToString());
            if (objectRow.ContainsKey("estado"))
                boleto.Estado = bool.Parse(objectRow["estado"].ToString());
            if (objectRow.ContainsKey("estadoComprado"))
                boleto.EstadoComprado = bool.Parse(objectRow["estadoComprado"].ToString());
            if (objectRow.ContainsKey("codigoQr"))
                boleto.CodigoQr =objectRow["codigoQr"].ToString();
            return boleto;
        }

        public List<Boleto> BuildObjects(List<Dictionary<string, object>> objectRows)
        {
            var lstResult = new List<Boleto>();

            foreach (var objRow in objectRows)
            {
                var boleto = BuildObject(objRow);
                lstResult.Add(boleto);
            }
            return lstResult;
        }


        public SqlOperation GetRetrieveByEmailStatement(string correo)
        {
            throw new NotImplementedException();
        }

        public Usuario MapObjectFromResult(Dictionary<string, object> result)
        {


            throw new NotImplementedException();
        }
    }
}
