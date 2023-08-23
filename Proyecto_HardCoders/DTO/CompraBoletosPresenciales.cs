using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CompraBoletosPresenciales
    {
        public string Correo { get; set; }
        public string NombreEventoAsistir { get; set; }
        public string Asiento { get; set; }
        public int IdUsuario { get; set; }

        public int IdBoleto { get; set; }

        public int IdEvento { get; set; }

        public int CantidadBoletos { get; set; }

        public float Total { get; set; }

        public float Subtotal { get; set; }

        public float Comision { get; set; }

        public float Impuesto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public string CodigoQr { get; set; }
        public string InfoPlantilla { get; set; }


    }
}
