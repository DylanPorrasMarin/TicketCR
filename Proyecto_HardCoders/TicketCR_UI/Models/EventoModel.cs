using DTO;

namespace TicketCR_UI.Models
{
    public class EventoModel
    {
        public int IdEvento { get; set; }
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public List<BoletoModel> Boletos { get; set; }
        public Usuario Usuario { get; set; }
        public string Eslogan { get; set; }
        public string Descripcion { get; set; }
        public int Grafica { get; set; }
        public string Modalidad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Restricciones { get; set; }
        public int AforoMaximo { get; set; }
        public int idCategoria { get; set; }
        public float Longitud { get; set; }
        public float Latitud { get; set; }
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
        public bool Inactivo {get; set;}
        public string InfoPlantilla { get; set; }
        public string Link { get; set; }
        public int CantidadBoletosCreados { get; set; }


    }
}
