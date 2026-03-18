using PSMApiRest.Models;
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
    
    public class SaldoAFavorDto
    {
        public int Id_Monto { get; set; }
        public int Id_Factura { get; set; }
        public string Cedula { get; set; }
        public decimal Monto { get; set; }
    }

    public class DepositoDto
    {
        public int Id_Deposito { get; set; }
        public int Id_Factura { get; set; }
        public string Id_Banco { get; set; }
        public string Referencia { get; set; }
        public string Fecha { get; set; }
        public decimal Monto { get; set; }
        public int Tipo { get; set; }
    }
}
