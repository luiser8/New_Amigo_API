using System;
using System.Collections.Generic;

namespace PSMApiRest.Models
{
    public class AsientoContablePayload
    {
        public int IdPeriodoContable { get; set; }
        public DateTime FechaAsiento { get; set; }
        public List<AsientoContableDetallePayload> AsientoContableDetalle { get; set; }
    }

    public class AsientoContableEditPayload
    {
        public int IdAsiento { get; set; }
        public int IdPeriodoContable { get; set; }
        public DateTime FechaAsiento { get; set; }
        public bool Activo { get; set; }
        public List<AsientoContableDetalleEditPayload> AsientoContableDetalle { get; set; }
    }

    public class AsientoContableDetallePayload
    {
        public int IdPlanCuenta { get; set; }
        public int IdCentroDeCosto { get; set; }
        public int IdMoneda { get; set; }
        public string Descripcion { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
        public string Referencia { get; set; }
        public DateTime FechaAsientoDetalle { get; set; }
        public bool Activo { get; set; }
    }

    public class AsientoContableDetalleEditPayload
    {
        public int IdAsientoDetalle { get; set; }
        public int IdAsiento { get; set; }
        public int IdPlanCuenta { get; set; }
        public int IdCentroDeCosto { get; set; }
        public int IdMoneda { get; set; }
        public string Descripcion { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
        public string Referencia { get; set; }
        public DateTime FechaAsientoDetalle { get; set; }
        public bool Activo { get; set; }
    }

    public class AsientoContableDetalleResponse
    {
        public int IdAsientoDetalle { get; set; }
        public int NroComprobante { get; set; }
        public int IdPeriodoContable { get; set; }
        public string PeriodoContable { get; set; }
        public int IdPlanCuenta { get; set; }
        public string NroPlanCuenta { get; set; }
        public string PlanCuenta { get; set; }
        public int IdCentroDeCosto { get; set; }
        public string CentroDeCosto { get; set; }
        public int IdMoneda { get; set; }
        public string Moneda { get; set; }
        public string Descripcion { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
        public string Referencia { get; set; }
        public DateTime FechaAsiento { get; set; }
        public DateTime FechaAsientoDetalle { get; set; }
        public DateTime FechaSistema { get; set; }
        public bool AsientoActivo { get; set; }
    }

    public class AsientoContableLibroMayor
    {
        public int NroComprobante { get; set; }
        public int IdPeriodoContable { get; set; }
        public string PeriodoContable { get; set; }
        public int IdAsientoDetalle { get; set; }
        public string NroCuenta { get; set; }
        public string DescripcionCuenta { get; set; }
        public decimal TotalDebe { get; set; }
        public decimal TotalHaber { get; set; }
        public decimal Saldo { get; set; }
        public decimal SaldoTotal { get; set; }
        public bool AsientoActivo { get; set; }
        public bool AsientoDetalleActivo { get; set; }
    }

    public class AsientoContableBalance
    {
        public int NroComprobante { get; set; }
        public int IdPlanCuenta { get; set; }
        public string NroCuenta { get; set; }
        public string DescripcionCuenta { get; set; }
        public decimal Saldo { get; set; }
        public bool AsientoActivo { get; set; }
    }
}