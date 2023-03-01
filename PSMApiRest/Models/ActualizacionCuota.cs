namespace PSMApiRest.Models
{
    public class ActualizacionCuota
    {
        public decimal Cuota { get; set; }
        public byte Abono { get; set; }
        public string Lapso { get; set; }
        public int Pagada { get; set; }
        public int Tipo { get; set; }
    }
}