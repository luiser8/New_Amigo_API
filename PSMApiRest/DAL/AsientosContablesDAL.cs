using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using PSMApiRest.Lib;
using PSMApiRest.Models;

namespace PSMApiRest.DAL
{
    public class AsientosContablesDAL
    {
        private readonly DB dbCon;
        private DataTable dt;
        private readonly Hashtable Parametros;

        public AsientosContablesDAL()
        {
            dt = new DataTable();
            dbCon = new DB();
            Parametros = new Hashtable();
        }

        public List<AsientoContableDetalleResponse> GetAsientosContables(string FechaDesde, string FechaHasta, int IdPeriodoContable)
        {
            Parametros.Clear();
            Parametros.Add("@FechaDesde", FechaDesde);
            Parametros.Add("@FechaHasta", FechaHasta);
            Parametros.Add("@IdPeriodoContable", IdPeriodoContable);
            Parametros.Add("@Tipo", 1);

            List<AsientoContableDetalleResponse> asientoContablesList = new List<AsientoContableDetalleResponse>();
            dt = dbCon.Procedure("AMIGO", "AsientosContablesAllSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AsientoContableDetalleResponse item = new AsientoContableDetalleResponse
                        {
                            IdAsientoDetalle = Convert.ToInt16(dt.Rows[i]["IdAsientoDetalle"]),
                            NroComprobante = Convert.ToInt32(dt.Rows[i]["NroComprobante"]),
                            IdPeriodoContable = Convert.ToInt16(dt.Rows[i]["IdPeriodoContable"]),
                            PeriodoContable = Convert.ToString(dt.Rows[i]["PeriodoContable"]),
                            IdPlanCuenta = Convert.ToInt16(dt.Rows[i]["IdPlanCuenta"]),
                            NroPlanCuenta = Convert.ToString(dt.Rows[i]["NroPlanCuenta"]),
                            PlanCuenta = Convert.ToString(dt.Rows[i]["PlanCuenta"]),
                            IdCentroDeCosto = Convert.ToInt16(dt.Rows[i]["IdCentroDeCosto"]),
                            CentroDeCosto = Convert.ToString(dt.Rows[i]["CentroDeCosto"]),
                            IdMoneda = Convert.ToInt16(dt.Rows[i]["IdMoneda"]),
                            Moneda = Convert.ToString(dt.Rows[i]["Moneda"]),
                            Descripcion = Convert.ToString(dt.Rows[i]["Descripcion"]),
                            Debe = Convert.ToDecimal(dt.Rows[i]["Debe"]),
                            Haber = Convert.ToDecimal(dt.Rows[i]["Haber"]),
                            Referencia = Convert.ToString(dt.Rows[i]["Referencia"]),
                            FechaAsiento = Convert.ToDateTime(dt.Rows[i]["FechaAsiento"]),
                            FechaAsientoDetalle = Convert.ToDateTime(dt.Rows[i]["FechaAsientoDetalle"]),
                            FechaSistema = Convert.ToDateTime(dt.Rows[i]["FechaSistema"]),
                            AsientoActivo = Convert.ToBoolean(dt.Rows[i]["AsientoActivo"])
                        };
                        asientoContablesList.Add(item);
                    }
                }
            }
            return asientoContablesList;
        }

        public List<AsientoContableLibroMayor> GetAsientosContablesLibroMayor(string FechaDesde, string FechaHasta, int IdPeriodoContable)
        {
            Parametros.Clear();
            Parametros.Add("@FechaDesde", FechaDesde);
            Parametros.Add("@FechaHasta", FechaHasta);
            Parametros.Add("@IdPeriodoContable", IdPeriodoContable);
            Parametros.Add("@Tipo", 2);

            List<AsientoContableLibroMayor> asientoContablesList = new List<AsientoContableLibroMayor>();
            dt = dbCon.Procedure("AMIGO", "AsientosContablesAllSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AsientoContableLibroMayor item = new AsientoContableLibroMayor
                        {
                            NroComprobante = Convert.ToInt32(dt.Rows[i]["NroComprobante"]),
                            IdPeriodoContable = Convert.ToInt16(dt.Rows[i]["IdPeriodoContable"]),
                            PeriodoContable = Convert.ToString(dt.Rows[i]["PeriodoContable"]),
                            NroCuenta = Convert.ToString(dt.Rows[i]["NroCuenta"]),
                            DescripcionCuenta = Convert.ToString(dt.Rows[i]["DescripcionCuenta"]),
                            DescripcionAsiento = Convert.ToString(dt.Rows[i]["DescripcionAsiento"]),
                            TotalDebe = Convert.ToDecimal(dt.Rows[i]["TotalDebe"]),
                            TotalHaber = Convert.ToDecimal(dt.Rows[i]["TotalHaber"]),
                            Saldo = Convert.ToDecimal(dt.Rows[i]["Saldo"]),
                            SaldoTotal = Convert.ToDecimal(dt.Rows[i]["SaldoTotal"]),
                            AsientoActivo = Convert.ToBoolean(dt.Rows[i]["AsientoActivo"]),
                            AsientoDetalleActivo = Convert.ToBoolean(dt.Rows[i]["AsientoDetalleActivo"]),
                            FechaAsientoDetalle = Convert.ToDateTime(dt.Rows[i]["FechaAsientoDetalle"])
                        };
                        asientoContablesList.Add(item);
                    }
                }
            }
            return asientoContablesList;

        }

        public List<AsientoContableBalance> GetAsientosContablesBalance(string FechaDesde, string FechaHasta, int IdPeriodoContable)
        {
            Parametros.Clear();
            Parametros.Add("@FechaDesde", FechaDesde);
            Parametros.Add("@FechaHasta", FechaHasta);
            Parametros.Add("@IdPeriodoContable", IdPeriodoContable);
            Parametros.Add("@Tipo", 3);

            List<AsientoContableBalance> asientoContablesList = new List<AsientoContableBalance>();
            dt = dbCon.Procedure("AMIGO", "AsientosContablesAllSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AsientoContableBalance item = new AsientoContableBalance
                        {
                            IdPlanCuenta = Convert.ToInt16(dt.Rows[i]["IdPlanCuenta"]),
                            NroCuenta = Convert.ToString(dt.Rows[i]["NroCuenta"]),
                            DescripcionCuenta = Convert.ToString(dt.Rows[i]["DescripcionCuenta"]),
                            Saldo = Convert.ToDecimal(dt.Rows[i]["Saldo"]),
                            AsientoActivo = Convert.ToBoolean(dt.Rows[i]["AsientoActivo"]),
                        };
                        asientoContablesList.Add(item);
                    }
                }
            }
            return asientoContablesList;

        }

        public int CreateAsientosContables(AsientoContablePayload asientoContablePayload)
        {
            int IdAsiento = 0;
            Parametros.Clear();
            Parametros.Add("@IdPeriodoContable", asientoContablePayload.IdPeriodoContable);
            Parametros.Add("@FechaAsiento", asientoContablePayload.FechaAsiento);

            dt = dbCon.Procedure("AMIGO", "AsientosContablesInsertSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IdAsiento = Convert.ToInt16(dt.Rows[i]["IdAsiento"]);
                    }
                }
            }
            return IdAsiento;
        }

        public int CreateAsientosContablesDetalle(List<AsientoContableDetallePayload> asientoContableDetallePayload, int IdAsiento)
        {
            int IdAsientoDetalle = 0;
            try
            {
                if (asientoContableDetallePayload.Count > 0)
                {
                    foreach(var item in asientoContableDetallePayload)
                    {
                        Parametros.Clear();
                        Parametros.Add("@IdAsiento", IdAsiento);
                        Parametros.Add("@IdPlanCuenta", item.IdPlanCuenta);
                        Parametros.Add("@IdCentroDeCosto", item.IdCentroDeCosto);
                        Parametros.Add("@IdMoneda", item.IdMoneda);
                        Parametros.Add("@Descripcion", item.Descripcion);
                        Parametros.Add("@Debe", item.Debe);
                        Parametros.Add("@Haber", item.Haber);
                        Parametros.Add("@Referencia", item.Referencia);
                        Parametros.Add("@FechaAsientoDetalle", item.FechaAsientoDetalle);

                        dt = dbCon.Procedure("AMIGO", "AsientosContablesDetallesInsertSys", Parametros);

                        if (dbCon.ErrorEstatus)
                        {
                            if (dt.Rows.Count != 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    IdAsientoDetalle = Convert.ToInt16(dt.Rows[i]["IdAsientoDetalle"]);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return IdAsientoDetalle;
        }

        public bool PutAsientosContables(AsientoContableEditPayload asientoContablePayload)
        {
            try
            {
                Parametros.Clear();
                Parametros.Add("@IdAsiento", asientoContablePayload.IdAsiento);
                Parametros.Add("@IdPeriodoContable", asientoContablePayload.IdPeriodoContable);
                Parametros.Add("@FechaAsiento", asientoContablePayload.FechaAsiento);
                Parametros.Add("@Activo", asientoContablePayload.Activo);

                dbCon.Procedure("AMIGO", "AsientosContablesEditSys", Parametros);

                return dbCon.ErrorEstatus;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PutAsientosContablesDetalles(List<AsientoContableDetalleEditPayload> asientoContableDetalePayload)
        {
            bool update = false;
            try
            {
                if (asientoContableDetalePayload.Count > 1)
                {
                    foreach (var item in asientoContableDetalePayload)
                    {
                        Parametros.Clear();
                        Parametros.Add("@IdAsientoDetalle", item.IdAsientoDetalle);
                        Parametros.Add("@IdAsiento", item.IdAsiento);
                        Parametros.Add("@IdPlanCuenta", item.IdPlanCuenta);
                        Parametros.Add("@IdCentroDeCosto", item.IdCentroDeCosto);
                        Parametros.Add("@IdMoneda", item.IdMoneda);
                        Parametros.Add("@Descripcion", item.Descripcion);
                        Parametros.Add("@Debe", item.Debe);
                        Parametros.Add("@Haber", item.Haber);
                        Parametros.Add("@Referencia", item.Referencia);
                        Parametros.Add("@FechaAsientoDetalle", item.FechaAsientoDetalle);
                        Parametros.Add("@Activo", item.Activo);
                        Parametros.Add("@TipoEdicion", 1);

                        dbCon.Procedure("AMIGO", "AsientosContablesDetalleEditSys", Parametros);

                        update = dbCon.ErrorEstatus;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return update;
        }

        public bool PutAsientosContablesDetallesActivoInactvo(AsientoContableDetalleEditPayload asientoContableDetalleEditPayload)
        {
            try
            {
                Parametros.Clear();
                Parametros.Add("@IdAsiento", asientoContableDetalleEditPayload.IdAsiento);
                Parametros.Add("@IdAsientoDetalle", asientoContableDetalleEditPayload.IdAsientoDetalle);
                Parametros.Add("@Activo", asientoContableDetalleEditPayload.Activo);
                Parametros.Add("@TipoEdicion", 2);

                dbCon.Procedure("AMIGO", "AsientosContablesDetalleEditSys", Parametros);

                return dbCon.ErrorEstatus;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}