using System;

namespace PSMApiRest.Models
{
    public class LibroDiario
    {
        public int IdLibroDiario { get; set; }
        public int IdCuentaBanco { get; set; }
        public int IdCentroDeCosto { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public byte Debe { get; set; }
        public byte Haber { get; set; }
        public string Referencia { get; set; }
        public string Moneda { get; set; }
        public DateTime Fecha { get; set; }
    }
}