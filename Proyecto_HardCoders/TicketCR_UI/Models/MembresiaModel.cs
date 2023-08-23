namespace TicketCR_UI.Models
{
    public class MembresiaModel
    {
        public int IdMembresia { get; set; }
        public string NombreMembresia { get; set; }
        public int CantidadEventos { get; set; }
        public int CantidadBoletos { get; set; }
        public float Impuestos { get; set; }
        public float Costo { get; set; }

        public bool Estado { get; set; }
        public int IdMembresiaUsuario { get; set; }
    }
}
