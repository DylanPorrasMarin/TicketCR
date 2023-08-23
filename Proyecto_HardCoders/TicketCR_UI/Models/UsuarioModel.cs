using System.ComponentModel.DataAnnotations;

namespace TicketCR_UI.Models
{
    public class UsuarioModel
    {
     
        public int IdUsuario { get; set; }

        public string Clave { get; set; }
    
        public string Nombre { get; set; }
   
        public string Apellidos { get; set; }
  
        public string Cedula { get; set; }
     
        public string Correo { get; set; }

        public string Telefono { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public float Latitud { get; set; }

        public float Longitud { get; set; }
        public string Rol { get; set; }
        public int IdMembresia { get; set; }
        public int CantidadEventos { get; set; }
        public bool Estado { get; set; }
    }
}
