namespace PSMApiRest.Models
{
    public class Lapsos
    {
        public int Id_Periodo { get; set; }
        public string Lapso { get; set; }
        public byte Activo { get; set; }
        public byte Cerrado { get; set; }
        public byte tipo_lapso { get; set; }
    }
}