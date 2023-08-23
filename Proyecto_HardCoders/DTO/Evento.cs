using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Eslogan { get; set; }
        public string Descripcion { get; set; }
        public int Grafica { get; set; }
        public string Modalidad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Restricciones { get; set; }
        public int AforoMaximo { get; set; }
        public int IdCategoria { get; set; }
        public float Longitud { get; set; }
        public float Latitud { get; set; }
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
        public string InfoPlantilla { get; set; }
        public string Link { get; set; }
        public int CantidadBoletosCreados { get; set; }
        public bool Inactivo { get; set; }
    }
}
