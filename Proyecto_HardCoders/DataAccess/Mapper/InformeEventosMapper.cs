using DataAccess.Dao;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccess.Mapper
{
    internal class InformeEventosMapper : ICrudStatements, IObjectMapper
    {
        public SqlOperation GetRetrieveByEmailStatement(string correo)
        {
            throw new NotImplementedException();
        }

        public Usuario MapObjectFromResult(Dictionary<string, object> result)
        {
            throw new NotImplementedException();
        }

        public InformeEventos BuildObject(Dictionary<string, object> objectRow)
        {
            InformeEventos informeEvento = new InformeEventos();
            if (objectRow.ContainsKey("idEvento"))
                informeEvento.IdEvento = int.Parse(objectRow["idEvento"].ToString());
            
            if (objectRow.ContainsKey("nombre"))
                informeEvento.NombreEvento = objectRow["nombre"].ToString();
           
            if (objectRow.ContainsKey("fechaInicio"))
                informeEvento.FechaInicio = DateTime.Parse(objectRow["fechaInicio"].ToString());
            if (objectRow.ContainsKey("boletosVendidos"))
                informeEvento.BoletosVendidos = int.Parse(objectRow["boletosVendidos"].ToString());
            if (objectRow.ContainsKey("ganancias"))
                informeEvento.Ganacias = float.Parse(objectRow["ganancias"].ToString());

          

            return informeEvento;
        }

        public InformeMembresias BuildMembresia(Dictionary<string, object> objectRow)
        {
            InformeMembresias informeMemb = new InformeMembresias();
            if (objectRow.ContainsKey("nombreMembresia"))
                informeMemb.NombreMembresia = objectRow["nombreMembresia"].ToString();
            if (objectRow.ContainsKey("costo"))
                informeMemb.Costo = float.Parse(objectRow["costo"].ToString());
            if (objectRow.ContainsKey("mes"))
                informeMemb.Mes = objectRow["mes"].ToString();
            if (objectRow.ContainsKey("membresias_adquiridas"))
                informeMemb.MembresiasAd = int.Parse(objectRow["membresias_adquiridas"].ToString());
            if (objectRow.ContainsKey("ganancias"))
                informeMemb.Ganancias = float.Parse(objectRow["ganancias"].ToString());

            return informeMemb;
        }

        public List<InformeMembresias> BuildMembresias(List<Dictionary<string, object>> objectRows)
        {
            var lstResult = new List<InformeMembresias>();

            foreach (var objRow in objectRows)
            {
                var informeMemb = BuildMembresia(objRow);
                lstResult.Add(informeMemb);
            }
            return lstResult;
        }

        public List<InformeEventos> BuildObjects(List<Dictionary<string, object>> objectRows)
        {
            var lstResult = new List<InformeEventos>();

            foreach (var objRow in objectRows)
            {
                var informeEvento = BuildObject(objRow);
                lstResult.Add(informeEvento);
            }
            return lstResult;
        }

        public SqlOperation ObtenerInformeEventos()
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "InformeEventosAdmin"
            };
            return operation;
        }

        public SqlOperation ObtenerInformeEventosGestor(int IdUsuario)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "InformeEventosGestor"
            };
            operation.AddIntegerParam("idUsuario", IdUsuario);
            return operation;
        }
        public SqlOperation ObtenerInformeMembresias()
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "InformeMembresias"
            };
            return operation;
        }
    }
}
