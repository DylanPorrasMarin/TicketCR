using DataAccess.Dao;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class CompraBoletosEvento 
    {

        public CompraBoletosVirtuales BuildObject(Dictionary<string, object> objectRow)
        {
            CompraBoletosVirtuales evento = new CompraBoletosVirtuales();
            //if (objectRow.ContainsKey("idEvento"))
            //    evento.IdEvento = int.Parse(objectRow["idEvento"].ToString());
       

            return evento;
        }



        public List<CompraBoletosVirtuales> BuildObjects(List<Dictionary<string, object>> objectRows)
        {
            var lstResult = new List<CompraBoletosVirtuales>();

            foreach (var objRow in objectRows)
            {
                var boleto = BuildObject(objRow);
                lstResult.Add(boleto);
            }
            return lstResult;
        }



        public SqlOperation InsertarCompraBoletosVirtuales(CompraBoletosVirtuales compraBoletosEvento) {


            var operation = new SqlOperation()
            {
                ProcedureName = "ComprarBoletosVirtuales"
            };

            // Agregar aquí los parámetros necesarios para el Stored Procedure
            operation.AddIntegerParam("idEvento", compraBoletosEvento.IdEvento);
            operation.AddIntegerParam("idUsuario", compraBoletosEvento.IdUsuario);
            operation.AddIntegerParam("idBoleto", compraBoletosEvento.IdBoleto);
            operation.AddFloatParam("total", compraBoletosEvento.Total);
            operation.AddFloatParam("subtotal", compraBoletosEvento.Subtotal);
            operation.AddFloatParam("comision", compraBoletosEvento.Comision);
            operation.AddFloatParam("impuesto", compraBoletosEvento.Impuesto);

            return operation;


        }

        public SqlOperation InsertarCompraBoletosPresenciales(CompraBoletosPresenciales compraBoletosPresenciales)
        {


            var operation = new SqlOperation()
            {
                ProcedureName = "ComprarBoletosPresenciales"
            };

            // Agregar aquí los parámetros necesarios para el Stored Procedure
            operation.AddIntegerParam("idEvento", compraBoletosPresenciales.IdEvento);
            operation.AddIntegerParam("idUsuario", compraBoletosPresenciales.IdUsuario);
            operation.AddVarcharParam("asiento", compraBoletosPresenciales.Asiento);
            operation.AddIntegerParam("idBoleto", compraBoletosPresenciales.IdBoleto);
            operation.AddFloatParam("total", compraBoletosPresenciales.Total);
            operation.AddFloatParam("subtotal", compraBoletosPresenciales.Subtotal);
            operation.AddFloatParam("comision", compraBoletosPresenciales.Comision);
            operation.AddFloatParam("impuesto", compraBoletosPresenciales.Impuesto);
            operation.AddVarcharParam("infoPlantilla", compraBoletosPresenciales.InfoPlantilla);

            return operation;


        }




    }

    
}
