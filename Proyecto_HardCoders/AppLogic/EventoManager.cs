using DataAccess.Crud;
using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AppLogic
{
    public class EventoManager
    {
        private EventoCrudFactory eventoCrudFactory;

        public EventoManager()
        {
            eventoCrudFactory = new EventoCrudFactory();
        }
        public List<Evento> ObtenerEventoVigentes()
        {
         EventoCrudFactory eventoCrud = new EventoCrudFactory();

            return eventoCrud.RetrieveAllEventos<Evento>();
        }
        public List<Evento> ObtenerEventosDelUsuarios(int idUsuario)
        {
            EventoCrudFactory eventoCrud = new EventoCrudFactory();

            return eventoCrud.ObtenerEventosUsuario<Evento>(idUsuario);
        }

        public List<Evento> ObtenerHistorialdeEventos(int idUsuario)
        {
            EventoCrudFactory eventoCrud = new EventoCrudFactory();

            return eventoCrud.ObtenerHistorialEventos<Evento>(idUsuario);
        }

        public List<Evento> ObtenerEventosAsistenciaUsuario(int idUsuario)
        {
            EventoCrudFactory eventoCrud = new EventoCrudFactory();

            return eventoCrud.ObtenerEventosAsistenciaUsuario<Evento>(idUsuario);
        }

        public void CrearEvento(Evento evento)
        {
            EventoCrudFactory eventoCrudFactory = new EventoCrudFactory();
            eventoCrudFactory.InsertarEvento(evento);
        }

        //CONSEGUIR TODA LA INFO DEL EVENTO
        public EventoListar ObtenerInformacionTotalEvento(int idEvento)
        {
            List<EventoListar> eventos = eventoCrudFactory.ObtenerInfoTotalEventos(idEvento);
            return eventos.FirstOrDefault(); // Devuelve el primer evento o null si no se encontró ninguno
        }

        // Método para actualizar evento
        public void ActualizarEvento(int IdEvento, string Nombre, int IdUsuario, string Eslogan, string Descripcion, int Grafica, string Modalidad, DateTime FechaInicio, DateTime FechaFinal,
            string Restricciones, int AforoMaximo, int IdCategoria, float Longitud, float Latitud, string Imagen1, string Imagen2, string Imagen3, string Link)
        {
            eventoCrudFactory.ActualizarEvento(IdEvento, Nombre, IdUsuario, Eslogan, Descripcion, Grafica, Modalidad, FechaInicio, FechaFinal,
             Restricciones, AforoMaximo, IdCategoria, Longitud, Latitud, Imagen1, Imagen2, Imagen3, Link); ;
        }


        public dynamic ActualizarEstadoEvento(int idMembresia, bool estado)
        {
            return eventoCrudFactory.ActualizarEstadoEvento(idMembresia, estado);

        }


    }
}
