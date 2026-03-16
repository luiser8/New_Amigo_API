using System;
using System.Collections.Generic;

namespace PSMApiRest.Models
{
    public class FacturaEstudiante
    {
        public string Identificador { get; set; }
        public int Id_Factura { get; set; }
        public string Monto { get; set; }
        public string FechaFactura { get; set; }
        public int Id_Detalle { get; set; }
        public int Id_Arancel { get; set; }
        public string Concepto { get; set; }
        public string MontoConcepto { get; set; }
        public SaldoAFavor SaldoAFavorJson { get; set; }
        public int Id_Inscripcion { get; set; }
        public int Id_Nivel { get; set; }
        public int Id_Periodo { get; set; }
        public string Periodo { get; set; }
        public List<DepositosArray> DepositosArray { get; set; }
    }

    public class SaldoAFavor
    {
        public int Id_Monto { get; set; }
        public string Saldo { get; set; }
    }

    public class DepositosArray
    {
        public string Id_Deposito { get; set; }
        public int Id_Banco { get; set; }
        public string Banco { get; set; }
        public string Referencia { get; set; }
        public string FechaDeposito { get; set; }
        public int Tipo { get; set; }
        public string TipoDescripcion { get; set; }
        public string MontoDeposito { get; set; }
    }
}