using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class Membresia
	{
		public int IdMembresia { get; set; }
		public string NombreMembresia { get; set; }
		public int CantidadEventos { get; set; }
		public int CantidadBoletos { get; set; }
        public float Impuestos { get; set; }
        public float Costo { get; set; }

		public bool Estado { get; set; }
    }
}
