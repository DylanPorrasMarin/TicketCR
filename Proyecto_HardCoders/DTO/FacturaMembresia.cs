using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public  class FacturaMembresia
    {
        public int IdNuevaMembresia { get; set; }
        public string NombreMembresia { get; set; }
        public DateTime FechaCompra { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public float Impuestos { get; set; }
        public float CostoTotal { get; set; }
        public int idUsuario { get; set; }
        public string correoUsuario { get; set; }
        public float SubTotal { get; set; }
        public string IdPagoPaypal { get; set; }
  
    }
}
