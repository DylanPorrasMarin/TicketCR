namespace TicketCR_UI.Models
{
    public class BoletoModel
    {
        public int IdBoleto { get; set; }
        public int IdEvento { get; set; }
        public int IdUsuarioComprador { get; set; }
        public string TipoBoleto { get; set; }
        public string Asiento { get; set; }
        public float Costo { get; set; }
        public bool Estado { get; set; }
        public bool EstadoComprado { get; set; }
        public string CodigoQr { get; set; }
        public float Impuesto { get; set; }
        public float Comision { get; set; }
    }
}
