using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Usuario
    {
        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public string Clave { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        public string Cedula { get; set; }
        [Required]
        public string Correo { get; set; }

        public string Telefono { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        public float Latitud { get; set; }

        public float Longitud { get; set; }

        public string Rol { get; set; }

		public int IdMembresia { get; set; }

        public int CantidadEventos { get; set; }

        public bool Estado { get; set; }

    }
}
