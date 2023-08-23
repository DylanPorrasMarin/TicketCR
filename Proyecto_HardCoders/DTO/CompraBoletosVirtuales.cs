using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CompraBoletosVirtuales
    {
       
        public string Correo { get; set; }
        public string NombreEventoAsistir { get; set; }

        public int IdUsuario { get; set; }

        public int IdBoleto { get; set; }

        public int IdEvento { get; set; }

        public int CantidadBoletos { get; set; }

        public string Link { get; set; }

        public float Total { get; set; }

        public float Subtotal { get; set; }

        public float Comision { get; set; }

        public float Impuesto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
    } 
}
