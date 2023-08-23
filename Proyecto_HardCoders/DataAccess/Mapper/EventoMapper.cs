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
    public class EventoMapper : ICrudStatements, IObjectMapper
    {

        public Evento BuildObject(Dictionary<string, object> objectRow)
        {
            Evento evento = new Evento();
            if (objectRow.ContainsKey("idEvento"))
                evento.IdEvento = int.Parse(objectRow["idEvento"].ToString());
            if (objectRow.ContainsKey("idCategoria"))
                evento.IdCategoria = int.Parse(objectRow["idCategoria"].ToString());
            if (objectRow.ContainsKey("nombre"))
                evento.Nombre = objectRow["nombre"].ToString();
            if (objectRow.ContainsKey("idUsuario"))
                evento.IdUsuario = int.Parse(objectRow["idUsuario"].ToString());
            if (objectRow.ContainsKey("eslogan"))
                evento.Eslogan = objectRow["eslogan"].ToString();
            if (objectRow.ContainsKey("descripcion"))
                evento.Descripcion = objectRow["descripcion"].ToString();
            if (objectRow.ContainsKey("grafica"))
                evento.Grafica = int.Parse(objectRow["grafica"].ToString());
            if (objectRow.ContainsKey("modalidad"))
                evento.Modalidad = objectRow["modalidad"].ToString();
            if (objectRow.ContainsKey("fechaInicio"))
                evento.FechaInicio = DateTime.Parse(objectRow["fechaInicio"].ToString());
            if (objectRow.ContainsKey("fechaFinal"))
                evento.FechaFinal = DateTime.Parse(objectRow["fechaFinal"].ToString());
            if (objectRow.ContainsKey("restricciones"))
                evento.Restricciones = objectRow["restricciones"].ToString();
            if (objectRow.ContainsKey("aforoMaximo"))
                evento.AforoMaximo = int.Parse(objectRow["aforoMaximo"].ToString());

            if (objectRow.ContainsKey("latitud"))
                evento.Latitud = float.Parse(objectRow["latitud"].ToString());
            if (objectRow.ContainsKey("longitud"))
                evento.Longitud = float.Parse(objectRow["longitud"].ToString());

            if (objectRow.ContainsKey("imagen1"))
                evento.Imagen1= objectRow["imagen1"].ToString();
            if (objectRow.ContainsKey("imagen2"))
                evento.Imagen2 = objectRow["imagen2"].ToString();
            if (objectRow.ContainsKey("imagen3"))
                evento.Imagen3 = objectRow["imagen3"].ToString();

            if (objectRow.ContainsKey("inactivo"))
                evento.Inactivo = bool.Parse(objectRow["inactivo"].ToString());

            if (objectRow.ContainsKey("cantidadBoletosCreados"))
                evento.AforoMaximo = int.Parse(objectRow["cantidadBoletosCreados"].ToString());


            //infoPlantilla

            if (objectRow.ContainsKey("infoPlantilla"))
                evento.InfoPlantilla = objectRow["infoPlantilla"].ToString();

            return evento;
        }
        public List<Evento> BuildObjects(List<Dictionary<string, object>> objectRows)
        {
            var lstResult = new List<Evento>();

            foreach (var objRow in objectRows)
            {
                var evento = BuildObject(objRow);
                lstResult.Add(evento);
            }
            return lstResult;
        }


        public SqlOperation InsertarEvento(Evento evento)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "InsertarEvento"
            };

            // Agregar aquí los parámetros necesarios para el Stored Procedure
            operation.AddVarcharParam("Nombre", evento.Nombre);
            operation.AddIntegerParam("idUsuario", evento.IdUsuario);
            operation.AddVarcharParam("Eslogan", evento.Eslogan);
            operation.AddVarcharParam("Descripcion", evento.Descripcion);
            operation.AddIntegerParam("Grafica", evento.Grafica);
            operation.AddVarcharParam("Modalidad", evento.Modalidad);
            operation.AddDateTimeParam("FechaInicio", evento.FechaInicio);
            operation.AddDateTimeParam("FechaFinal", evento.FechaFinal);
            operation.AddVarcharParam("Restricciones", evento.Restricciones);
            operation.AddIntegerParam("AforoMaximo", evento.AforoMaximo);
            operation.AddIntegerParam("idCategoria", evento.IdCategoria);
            operation.AddFloatParam("Longitud", evento.Longitud);
            operation.AddFloatParam("Latitud", evento.Latitud);
            operation.AddVarcharParam("Imagen1", evento.Imagen1);
            operation.AddVarcharParam("Imagen2", evento.Imagen2);
            operation.AddVarcharParam("Imagen3", evento.Imagen3);
            operation.AddVarcharParam("infoPlantilla", evento.InfoPlantilla);
            operation.AddVarcharParam("Link",evento.Link);
            operation.AddIntegerParam("CantidadBoletosCreados", evento.CantidadBoletosCreados);




            // Parámetro de salida para el idEvento generado
            operation.AddOutputParam("idEvento", SqlDbType.Int);

            return operation;
        }


        #region OBTENER INFO TOTAL DEL EVENTO, BOLETOS Y USUARIOS CREADOR
        public EventoListar ObtenerInformacionTotalEvento(Dictionary<string, object> objectRow)
        {
            EventoListar evento = new EventoListar();

            if (objectRow.ContainsKey("idEvento"))
                evento.IdEvento = int.Parse(objectRow["idEvento"].ToString());
            if (objectRow.ContainsKey("idCategoria"))
                evento.idCategoria = int.Parse(objectRow["idCategoria"].ToString());
            if (objectRow.ContainsKey("evento_nombre"))
                evento.Nombre = objectRow["evento_nombre"].ToString();
            if (objectRow.ContainsKey("evento_idUsuario"))
                evento.IdUsuario = int.Parse(objectRow["evento_idUsuario"].ToString());
            if (objectRow.ContainsKey("eslogan"))
                evento.Eslogan = objectRow["eslogan"].ToString();
            if (objectRow.ContainsKey("descripcion"))
                evento.Descripcion = objectRow["descripcion"].ToString();
            if (objectRow.ContainsKey("grafica"))
                evento.Grafica = int.Parse(objectRow["grafica"].ToString());
            if (objectRow.ContainsKey("modalidad"))
                evento.Modalidad = objectRow["modalidad"].ToString();
            if (objectRow.ContainsKey("fechaInicio"))
                evento.FechaInicio = DateTime.Parse(objectRow["fechaInicio"].ToString());
            if (objectRow.ContainsKey("fechaFinal"))
                evento.FechaFinal = DateTime.Parse(objectRow["fechaFinal"].ToString());
            if (objectRow.ContainsKey("restricciones"))
                evento.Restricciones = objectRow["restricciones"].ToString();
            if (objectRow.ContainsKey("aforoMaximo"))
                evento.AforoMaximo = int.Parse(objectRow["aforoMaximo"].ToString());

            if (objectRow.ContainsKey("latitud"))
                evento.Latitud = float.Parse(objectRow["latitud"].ToString());
            if (objectRow.ContainsKey("longitud"))
                evento.Longitud = float.Parse(objectRow["longitud"].ToString());

            if (objectRow.ContainsKey("imagen1"))
                evento.Imagen1 = objectRow["imagen1"].ToString();
            if (objectRow.ContainsKey("imagen2"))
                evento.Imagen2 = objectRow["imagen2"].ToString();
            if (objectRow.ContainsKey("imagen3"))
                evento.Imagen3 = objectRow["imagen3"].ToString();


            if (objectRow.ContainsKey("infoPlantilla"))
                evento.InfoPlantilla = objectRow["infoPlantilla"].ToString();

            if (objectRow.ContainsKey("link"))
                evento.Link = objectRow["link"].ToString();

            if (objectRow.ContainsKey("cantidadBoletosCreados"))
                evento.CantidadBoletosCreados = int.Parse(objectRow["cantidadBoletosCreados"].ToString());

            if (objectRow.ContainsKey("inactivo"))
                evento.Inactivo = bool.Parse(objectRow["inactivo"].ToString());


            // Crear el objeto Usuario
            Usuario usuario = new Usuario();

            if (objectRow.ContainsKey("usuario_idUsuario"))
                usuario.IdUsuario = int.Parse(objectRow["usuario_idUsuario"].ToString());
            if (objectRow.ContainsKey("usuario_nombre"))
                usuario.Nombre = objectRow["usuario_nombre"].ToString();
            if (objectRow.ContainsKey("apellidos"))
                usuario.Apellidos = objectRow["apellidos"].ToString();
            if (objectRow.ContainsKey("cedula"))
                usuario.Cedula = objectRow["cedula"].ToString();
            if (objectRow.ContainsKey("correo"))
                usuario.Correo = objectRow["correo"].ToString();
            if (objectRow.ContainsKey("telefono"))
                usuario.Telefono = objectRow["telefono"].ToString();
            if (objectRow.ContainsKey("fechaNacimiento"))
                usuario.FechaNacimiento = DateTime.Parse(objectRow["fechaNacimiento"].ToString()); 
            if (objectRow.ContainsKey("eventosCreados"))
                usuario.CantidadEventos = int.Parse(objectRow["eventosCreados"].ToString());
            if (objectRow.ContainsKey("idMembresia"))
                usuario.IdMembresia = int.Parse(objectRow["idMembresia"].ToString());
            if (objectRow.ContainsKey("rol"))
                usuario.Rol = objectRow["rol"].ToString();

            evento.Usuario = usuario;

            return evento;
        }



        public List<EventoListar> ConstruirObjetos(List<Dictionary<string, object>> objectRows)
        {
            var lstResult = new List<EventoListar>();

            foreach (var objRow in objectRows)
            {
                var evento = ObtenerInformacionTotalEvento(objRow);

                lstResult.Add(evento);
            }
            return lstResult;
        }

        public SqlOperation ObtenerInfoTotalEvento(int IdEvento)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ObtenerEventoYCreador"
            };
            operation.AddIntegerParam("idEvento", IdEvento);
            return operation;
        }
        #endregion
     
            // Otros métodos del mapper

            // Método para mapear la actualización de un evento
            public SqlOperation ActualizarEvento(int IdEvento, string Nombre, int IdUsuario, string Eslogan, string Descripcion, int Grafica, string Modalidad, DateTime FechaInicio, DateTime FechaFinal,
            string Restricciones, int AforoMaximo, int IdCategoria, float Longitud, float Latitud, string Imagen1, string Imagen2, string Imagen3, string Link)
        {
                var operation = new SqlOperation()
                {
                    ProcedureName = "ActualizarEvento"
                };

                operation.AddIntegerParam("idEvento", IdEvento);
                operation.AddVarcharParam("Nombre", Nombre);
                operation.AddVarcharParam("Eslogan", Eslogan);
                operation.AddVarcharParam("Descripcion", Descripcion);
                operation.AddIntegerParam("Grafica", Grafica);
                operation.AddVarcharParam("Modalidad", Modalidad);
                operation.AddDateTimeParam("FechaInicio", FechaInicio);
                operation.AddDateTimeParam("FechaFinal", FechaFinal);
                operation.AddVarcharParam("Restricciones", Restricciones);
                operation.AddIntegerParam("AforoMaximo", AforoMaximo);
                operation.AddIntegerParam("idCategoria", IdCategoria);
                operation.AddFloatParam("Longitud", Longitud);
                operation.AddFloatParam("Latitud", Latitud);
                operation.AddVarcharParam("Imagen1", Imagen1);
                operation.AddVarcharParam("Imagen2", Imagen2);
                operation.AddVarcharParam("Imagen3", Imagen3);
                operation.AddVarcharParam("link", Link);

            return operation;
            }


        public SqlOperation ObtenerEventosVigentes()
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "BuscarEventosVigentes"
            };
            return operation;
        }

        public SqlOperation ObtenerHistorialEventos(int IdUsuario)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ObtenerHistorialEventosUsuario"
            };
            operation.AddIntegerParam("idUsuario", IdUsuario);
            return operation;
        }


        public SqlOperation ObtenerEventosUsuario(int IdUsuario)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ObtenerEventoUsuario"
            };
            operation.AddIntegerParam("idUsuario", IdUsuario);
            return operation;
        }

        public SqlOperation ObtenerEventosAsistenciaUsuario(int IdUsuario)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ObtenerEventosAsistenciaUsuario"
            };
            operation.AddIntegerParam("idUsuarioComprador", IdUsuario);
            return operation;
        }

        public SqlOperation ActualizarEstadoEvento(int idEvento, bool estado) 
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ActualizarEstadoEvento"
            };
            operation.AddIntegerParam("idEvento", idEvento);
            operation.AddBooleanParam("nuevoEstado", estado);
            return operation;
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
