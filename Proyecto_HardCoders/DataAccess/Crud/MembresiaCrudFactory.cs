using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Video.V1.Room;

namespace DataAccess.Crud
{
    public class MembresiaCrudFactory : CrudFactory
    {
        private MembresiaMapper mapper;

        public MembresiaCrudFactory() : base()
        {
            mapper = new MembresiaMapper();
            dao = SqlDao.GetInstance();
        }

        public override T RetrieveById<T>(int id)
        {
            if (typeof(T) == typeof(Membresia))
            {
                var operation = mapper.MembresiaPorId(id); // Llamar al método del Mapper para obtener la operación de lectura
                // Aquí, debes ejecutar la operación en la base de datos y obtener los resultados
                // Luego, mapear el resultado a un objeto de tipo T utilizando el método MapObjectFromResult del Mapper
                List<Dictionary<string, object>> results = dao.ExecuteStoredProcedureWithQuery(operation);

                if (results.Count > 0)
                {
                    return (T)(object)mapper.MapObjectFromResult(results[0]);
                }
            }

            return default(T);
        }


        public dynamic CrearMembresia(string nombreMembresia, float costo, int cantidadEventos, int cantidadBoletos) {


            var operation = mapper.CrearMembresia(nombreMembresia,  costo,  cantidadEventos,  cantidadBoletos);

            dao.ExecuteStoredProcedure(operation);

            return new { success=true };
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
            List<T> lstResults = new List<T>();
            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(mapper.ObtenerMembresias());

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

        public dynamic ActualizarMembresiaUsuarioYRegistrarCompra(int IdNuevaMembresia, float CostoTotal, float SubTotal, float Impuestos, int idUsuario, string IdPagoPaypal)
        { 
            MembresiaCrudFactory membresiaCrud = new MembresiaCrudFactory();

            var operation = membresiaCrud.mapper.ActualizarMembresiaUsuarioYRegistrarCompra( IdNuevaMembresia,  CostoTotal,  SubTotal,  Impuestos,  idUsuario, IdPagoPaypal);
            dao.ExecuteStoredProcedure(operation);

            return new { exito = true };
        
        }

        public dynamic ActualizarEstadoMembresia(int idMembresia, bool estado)
        {
            MembresiaCrudFactory membresiaCrud = new MembresiaCrudFactory();

            var operation = membresiaCrud.mapper.ActualizarEstadoMembresia(idMembresia,estado);
            dao.ExecuteStoredProcedure(operation);

            return new { exito = true };

        }


        public override T RetrieveByEmail<T>(string correo)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
