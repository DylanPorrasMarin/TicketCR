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
    public class InformeEventosManager
    {
        private InformeEventosCrudFactory informeEventosCrudFactory;
        

        public InformeEventosManager() { 
             informeEventosCrudFactory = new InformeEventosCrudFactory();
        }
        public List<InformeEventos> ObtenerInformeEventos()
        {
           InformeEventosCrudFactory informeEventosCrud = new InformeEventosCrudFactory();

            return informeEventosCrud.RetrieveAllInformeEventos<InformeEventos>();
        }
        public List<InformeMembresias> ObtenerInformeMembresias()
        {
            InformeEventosCrudFactory informeEventosCrud = new InformeEventosCrudFactory();

            return informeEventosCrud.RetrieveAllInformeMembresias<InformeMembresias>();
        }


        public List<InformeEventos> ObtenerInformeEventosGestor(int idUsuario)
        {
            InformeEventosCrudFactory informeEventosCrud = new InformeEventosCrudFactory();

            return informeEventosCrud.RetrieveAllInformeEventosGestor<InformeEventos>(idUsuario);
        }
    }
}
