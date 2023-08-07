using System;

namespace PSMApiRest.Models
{
    public class CuotasInsertadas
    {
        public int Id_Arancel { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }

    public class CuotasInsertadasDelete
    {
        public int IdInscripcion { get; set; }
        public int IdArancel { get; set; }
        public int Pagada { get; set; }
    }
    public class CuotasResetPayload
    {
        public string Lapso { get; set; }
        public int Pagada { get; set; }
        public int Arancel0 { get; set; }
        public int Arancel1 { get; set; }
        public int Arancel2 { get; set; }
        public int Arancel3 { get; set; }
        public int Arancel4 { get; set; }
    }
}