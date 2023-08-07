using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using PSMApiRest.Lib;
using PSMApiRest.Models;

namespace PSMApiRest.DAL
{
    public class CuotaDAL
    {
        private readonly DB dbCon;
        private DataTable dt;
        private readonly Hashtable Parametros;

        public CuotaDAL()
        {
            dt = new DataTable();
            dbCon = new DB();
            Parametros = new Hashtable();
        }

        public List<Cuota> GetCuota(byte Tipo, byte Estado)
        {
            Parametros.Clear();
            Parametros.Add("@Tipo", Tipo);
            Parametros.Add("@Estado", Estado);

            List<Cuota> CuotaList = new List<Cuota>();
            dt = dbCon.Procedure("AMIGO", "CuotasSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Cuota cuota = new Cuota();
                        cuota.CuotaId = Convert.ToInt32(dt.Rows[i]["CuotaId"]);
                        cuota.Monto = Convert.ToDecimal(dt.Rows[i]["Monto"]);
                        cuota.Dolar = Convert.ToDecimal(dt.Rows[i]["Dolar"]);
                        cuota.Lapso = Convert.ToString(dt.Rows[i]["Lapso"]);
                        cuota.Tipo = Convert.ToByte(dt.Rows[i]["Tipo"]);
                        cuota.Tasa = Convert.ToDecimal(dt.Rows[i]["Tasa"]);
                        cuota.FechaCreacion = Convert.ToDateTime(dt.Rows[i]["FechaCreacion"]);
                        cuota.Estado = Convert.ToByte(dt.Rows[i]["Estado"]);
                        CuotaList.Add(cuota);
                    }
                }
            }
            return CuotaList;
        }
        public List<Cuota> GetCuotaByLapso(string Lapso, string FechaDesde, string FechaHasta)
        {
            Parametros.Clear();
            Parametros.Add("@Lapso", Lapso);
            Parametros.Add("@FechaDesde", FechaDesde);
            Parametros.Add("@FechaHasta", FechaHasta);

            List<Cuota> CuotaList = new List<Cuota>();
            dt = dbCon.Procedure("AMIGO", "CuotasByLapsoSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Cuota cuota = new Cuota();
                        cuota.CuotaId = Convert.ToInt32(dt.Rows[i]["CuotaId"]);
                        cuota.Monto = Convert.ToDecimal(dt.Rows[i]["Monto"]);
                        cuota.Dolar = Convert.ToDecimal(dt.Rows[i]["Dolar"]);
                        cuota.Lapso = Convert.ToString(dt.Rows[i]["Lapso"]);
                        cuota.Tipo = Convert.ToByte(dt.Rows[i]["Tipo"]);
                        cuota.Tasa = Convert.ToDecimal(dt.Rows[i]["Tasa"]);
                        cuota.FechaCreacion = Convert.ToDateTime(dt.Rows[i]["FechaCreacion"]);
                        cuota.Estado = Convert.ToByte(dt.Rows[i]["Estado"]);
                        CuotaList.Add(cuota);
                    }
                }
            }
            return CuotaList;
        }
        public List<CuotasInsertadas> GetCuotasInsertadas(string Lapso)
        {
            Parametros.Clear();
            Parametros.Add("@Lapso", Lapso);

            List<CuotasInsertadas> CuotaList = new List<CuotasInsertadas>();
            dt = dbCon.Procedure("AMIGO", "CuotasInsertadasAllSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CuotasInsertadas cuota = new CuotasInsertadas();
                        cuota.Id_Arancel = Convert.ToInt32(dt.Rows[i]["Id_Arancel"]);
                        cuota.Descripcion = Convert.ToString(dt.Rows[i]["Descripcion"]);
                        cuota.Monto = Convert.ToDecimal(dt.Rows[i]["Monto"]);
                        cuota.FechaVencimiento = Convert.ToDateTime(dt.Rows[i]["FechaVencimiento"]);
                        CuotaList.Add(cuota);
                    }
                }
            }
            return CuotaList;
        }
        public List<CuotasInsertadasDelete> GetResetCuotasInserts(CuotasResetPayload cuotasResetPayload)
        {
            Parametros.Clear();
            Parametros.Add("@Lapso", cuotasResetPayload.Lapso);
            Parametros.Add("@Arancel0", cuotasResetPayload.Arancel0);
            Parametros.Add("@Arancel1", cuotasResetPayload.Arancel1);
            Parametros.Add("@Arancel2", cuotasResetPayload.Arancel2);
            Parametros.Add("@Arancel3", cuotasResetPayload.Arancel3);
            Parametros.Add("@Arancel4", cuotasResetPayload.Arancel4);
            Parametros.Add("@Pagada", cuotasResetPayload.Pagada);

            List<CuotasInsertadasDelete> CuotaList = new List<CuotasInsertadasDelete>();
            dt = dbCon.Procedure("AMIGO", "CuotasSelectSysReset", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CuotasInsertadasDelete cuota = new CuotasInsertadasDelete();
                        cuota.IdInscripcion = Convert.ToInt32(dt.Rows[i]["Id_Inscripcion"]);
                        cuota.IdArancel = Convert.ToInt32(dt.Rows[i]["Id_Arancel"]);
                        cuota.Pagada = Convert.ToInt32(dt.Rows[i]["Pagada"]);
                        CuotaList.Add(cuota);
                    }
                }
            }
            return CuotaList;
        }
        public List<Cuota> InsertCuota(int CuotaId, byte Tipo, decimal Dolar, decimal Tasa, decimal Monto, string Lapso, byte Estado)
        {
            Parametros.Clear();
            Parametros.Add("@CuotaId", CuotaId);
            Parametros.Add("@Tipo", Tipo);
            Parametros.Add("@Dolar", Dolar);
            Parametros.Add("@Tasa", Tasa);
            Parametros.Add("@Monto", Monto);
            Parametros.Add("@Lapso", Lapso);
            Parametros.Add("@Estado", Estado);

            List<Cuota> CuotaList = new List<Cuota>();
            dt = dbCon.Procedure("AMIGO", "CuotasSysInsert", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Cuota cuota = new Cuota();
                        cuota.CuotaId = Convert.ToInt32(dt.Rows[i]["CuotaId"]);
                        CuotaList.Add(cuota);
                    }
                }
            }
            return CuotaList;
        }
        public List<Cuota> EditCuota(int CuotaId, decimal Monto, byte Estado)
        {
            Parametros.Clear();
            Parametros.Add("@CuotaId", CuotaId);
            Parametros.Add("@Monto", Monto);
            Parametros.Add("@Estado", Estado);

            List<Cuota> CuotaList = new List<Cuota>();
            dt = dbCon.Procedure("AMIGO", "CuotasSysUpdate", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Cuota cuota = new Cuota();
                        cuota.CuotaId = CuotaId;
                        CuotaList.Add(cuota);
                    }
                }
            }
            return CuotaList;
        }
        public List<Cuota> DeleteCuota(int CuotaId)
        {
            Parametros.Clear();
            Parametros.Add("@CuotaId", CuotaId);

            List<Cuota> CuotaList = new List<Cuota>();
            dt = dbCon.Procedure("AMIGO", "CuotasSysDelete", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Cuota cuota = new Cuota();
                        cuota.CuotaId = CuotaId;
                        CuotaList.Add(cuota);
                    }
                }
            }
            return CuotaList;
        }
        public decimal SingleCuota(byte Tipo, string Lapso)
        {
            Parametros.Clear();
            Parametros.Add("@Tipo", Tipo);
            Parametros.Add("@Lapso", Lapso);

            dt = dbCon.Procedure("AMIGO", "CuotasByTypeSys", Parametros);
            decimal dolar = 0;

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                   dolar = Convert.ToDecimal(dt.Rows[0].ItemArray[0]);
                }
            }
            return dolar;
        }
    }
}