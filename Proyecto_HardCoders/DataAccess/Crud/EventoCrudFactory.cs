using Azure;
using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class EventoCrudFactory : CrudFactory
    {
        private EventoMapper mapper;
        private int idEvento; // Variable para almacenar el idEvento generado

        public EventoCrudFactory() : base()
        {
            mapper = new EventoMapper();
            dao = SqlDao.GetInstance();
        }

        //memtodo para actualziar evento

        public int InsertarEvento(Evento evento)
        {
            SqlOperation operation = mapper.InsertarEvento(evento);

            // Ejecutar el procedimiento almacenado
            dao.ExecuteStoredProcedure(operation);
            // Obtener el valor del parámetro de salida "idEvento" desde la operación
            int idEvento = Convert.ToInt32(operation.GetParameterValue("idEvento"));

            return idEvento;
        }
        // Método para actualizar evento
        public int ActualizarEvento(int IdEvento, string Nombre, int IdUsuario, string Eslogan, string Descripcion, int Grafica, string Modalidad, DateTime FechaInicio, DateTime FechaFinal,
            string Restricciones, int AforoMaximo, int IdCategoria, float Longitud, float Latitud, string Imagen1, string Imagen2, string Imagen3, string Link)
        {
            SqlOperation operation = mapper.ActualizarEvento(IdEvento,Nombre, IdUsuario, Eslogan, Descripcion, Grafica, Modalidad, FechaInicio, FechaFinal,
             Restricciones, AforoMaximo, IdCategoria, Longitud, Latitud, Imagen1, Imagen2, Imagen3, Link);
            dao.ExecuteStoredProcedure(operation);
            int idEvento = Convert.ToInt32(operation.GetParameterValue("idEvento"));
            return idEvento;
        }


        public override void Create()
        {
            throw new NotImplementedException();
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public List<T> RetrieveAllEventos<T>()
        {
            List<T> lstResults = new List<T>();
            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(mapper.ObtenerEventosVigentes());

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

        public override List<T> RetrieveAll<T>()
        {
            List<T> lstResults = new List<T>();
            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(mapper.ObtenerEventosVigentes());

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

        public List<EventoListar> ObtenerInfoTotalEventos(int idEvento)
        {
            SqlOperation operation = mapper.ObtenerInfoTotalEvento(idEvento);
            List<Dictionary<string, object>> result = dao.ExecuteStoredProcedureWithQuery(operation);
            return mapper.ConstruirObjetos(result);
        }

        public List<T> ObtenerEventosUsuario<T>(int idUsuario)
        {
            List<T> lstResults = new List<T>();
            SqlOperation operation = mapper.ObtenerEventosUsuario(idUsuario);
            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(operation);
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

        public List<T> ObtenerHistorialEventos<T>(int idUsuario)
        {
            List<T> lstResults = new List<T>();
            SqlOperation operation = mapper.ObtenerHistorialEventos(idUsuario);
            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(operation);
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

        public List<T> ObtenerEventosAsistenciaUsuario<T>(int idUsuario)
        {
            List<T> lstResults = new List<T>();
            SqlOperation operation = mapper.ObtenerEventosAsistenciaUsuario(idUsuario);
            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(operation);
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


        public dynamic ActualizarEstadoEvento(int idMembresia, bool estado)
        {
            EventoCrudFactory eventoCrud = new EventoCrudFactory();

            var operation = eventoCrud.mapper.ActualizarEstadoEvento(idMembresia, estado);
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

        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }
    }
}
