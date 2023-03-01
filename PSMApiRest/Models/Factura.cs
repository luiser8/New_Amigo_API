using System;

namespace PSMApiRest.Models
{
    public class Factura
    {
        public int Id_Factura { get; set; }
        public int Id_Detalle { get; set; }
        public int Id_Arancel { get; set; }
        public int Id_Inscripcion { get; set; }
        public decimal Monto { get; set; }
        public byte Abono { get; set; }
        public decimal Anulada { get; set; }
        public string Descripcion { get; set; }
        public DateTime Hora { get; set; }
    }
}