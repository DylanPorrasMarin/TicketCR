using DataAccess.Crud;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class MembresiaManager
    {
        private MembresiaCrudFactory membresiaCrudFactory;

        public MembresiaManager()
        {
            membresiaCrudFactory = new MembresiaCrudFactory();
        }

        public Membresia ObtenerMembresiaPorId(int idMembresia)
        {
            return membresiaCrudFactory.RetrieveById<Membresia>(idMembresia);
        }
        public List<Membresia> ObtenerMembresias()
        {
            return membresiaCrudFactory.RetrieveAll<Membresia>();
        }

        public void ActualizarMembresiaUsuarioYRegistrarCompra(int IdNuevaMembresia, float CostoTotal, float SubTotal, float Impuestos, int idUsuario, string IdPagoPaypal) {

           membresiaCrudFactory.ActualizarMembresiaUsuarioYRegistrarCompra(IdNuevaMembresia, CostoTotal, SubTotal, Impuestos, idUsuario, IdPagoPaypal);
        
        }

        public dynamic CrearMembresia(string nombreMembresia, float costo, int cantidadEventos, int cantidadBoletos)
        {
            return membresiaCrudFactory.CrearMembresia(nombreMembresia, costo, cantidadEventos, cantidadBoletos);
        }

        public dynamic ActualizarEstadoMembresia(int idMembresia, bool estado)
        {
            return membresiaCrudFactory.ActualizarEstadoMembresia(idMembresia, estado);

        }

    }
}
