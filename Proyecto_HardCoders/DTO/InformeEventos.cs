using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class InformeEventos
    {
        public int IdEvento { get; set; }
        public string NombreEvento { get; set; }
        public DateTime FechaInicio { get; set; }
        public int BoletosVendidos { get; set; }
        public float Ganacias { get; set; }
    }
}
