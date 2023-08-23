using DTO;

namespace TicketCR_UI.Models
{
    public class InformeEventosModel 
    {
        public int IdEvento { get; set; }
        public string NombreEvento { get; set; }
        public DateTime FechaInicio { get; set; }
        public int BoletosVendidos { get; set; }
        public float Ganacias { get; set; }
    }
}
