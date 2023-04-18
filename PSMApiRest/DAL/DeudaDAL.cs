using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using PSMApiRest.Lib;
using PSMApiRest.Models;

namespace PSMApiRest.DAL
{
    public class DeudaDAL
    {
        private readonly DB dbCon;
        private DataTable dt;
        private readonly Hashtable Parametros;

        public DeudaDAL()
        {
            dt = new DataTable();
            dbCon = new DB();
            Parametros = new Hashtable();
        }

        public List<Deuda> GetDeuda(string Lapso, string Identificador)
        {
            Parametros.Clear();
            Parametros.Add("@Lapso", Lapso);
            //Parametros.Add("@Pagada", Pagada);
            Parametros.Add("@Identificador", Identificador != "" ? Identificador : null);

            List<Deuda> DeudaList = new List<Deuda>();
            dt = dbCon.Procedure("AMIGO", "DeudasSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Deuda deuda = new Deuda();
                        deuda.Id_Cuenta = Convert.ToInt32(dt.Rows[i]["Id_Cuenta"]);
                        deuda.Id_Inscripcion = Convert.ToInt32(dt.Rows[i]["Id_Inscripcion"]);
                        deuda.Id_Arancel = Convert.ToInt32(dt.Rows[i]["Id_Arancel"]);
                        deuda.Identificador = Convert.ToString(dt.Rows[i]["Identificador"]);
                        deuda.Cuota = Convert.ToString(dt.Rows[i]["Cuota"]);
                        deuda.Lapso = Convert.ToString(dt.Rows[i]["Lapso"]);
                        deuda.Pagada = Convert.ToByte(dt.Rows[i]["Pagada"]);
                        deuda.Monto = Convert.ToDecimal(dt.Rows[i]["Monto"]);
                        deuda.MontoFacturas = Convert.ToDecimal(dt.Rows[i]["MontoFacturas"]);
                        deuda.FechaVencimiento = Convert.ToDateTime(dt.Rows[i]["FechaVencimiento"]);
                        deuda.Total = Math.Floor(Convert.ToDecimal(dt.Rows[i]["Total"]) * 100) / 100;
                        DeudaList.Add(deuda);
                    }
                }
            }
            return DeudaList;
        }
        public List<Deuda> GetAllDeudas(string Lapso, int Pagada, int Tipo)
        {
            Parametros.Clear();
            Parametros.Add("@Lapso", Lapso);
            Parametros.Add("@Pagada", Pagada);
            Parametros.Add("@Tipo", Tipo);

            List<Deuda> DeudaAllList = new List<Deuda>();
            dt = dbCon.Procedure("AMIGO", "DeudasSysNegativos", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Deuda deuda = new Deuda();
                        deuda.Id_Cuenta = Convert.ToInt32(dt.Rows[i]["Id_Cuenta"]);
                        deuda.Id_Inscripcion = Convert.ToInt32(dt.Rows[i]["Id_Inscripcion"]);
                        deuda.Id_Arancel = Convert.ToInt32(dt.Rows[i]["Id_Arancel"]);
                        deuda.Identificador = Convert.ToString(dt.Rows[i]["Identificador"]);
                        deuda.Monto = Convert.ToDecimal(dt.Rows[i]["Monto"]);
                        deuda.MontoFacturas = Convert.ToDecimal(dt.Rows[i]["MontoFacturas"]);
                        deuda.FechaVencimiento = Convert.ToDateTime(dt.Rows[i]["FechaVencimiento"]);
                        deuda.Total = Math.Floor(Convert.ToDecimal(dt.Rows[i]["Total"]) * 100) / 100;
                        DeudaAllList.Add(deuda);
                    }
                }
            }
            return DeudaAllList;
        }
        public List<Abono> GetAbono(int Id_Inscripcion, int Id_Arancel, byte Abono)
        {
            Parametros.Clear();
            Parametros.Add("@Id_Inscripcion", Id_Inscripcion);
            Parametros.Add("@Id_Arancel", Id_Arancel);
            Parametros.Add("@Abono", Abono);

            List<Abono> AbonoList = new List<Abono>();
            dt = dbCon.Procedure("AMIGO", "DeudasSysAbonos", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Abono abono = new Abono();
                        abono.Monto = Convert.ToDecimal(dt.Rows[i]["Monto"]);
                        AbonoList.Add(abono);
                    }
                }
            }
            return AbonoList;
        }
        public List<Deuda> EditDeuda(int Id_Cuenta, int Pagada, decimal Monto, decimal? MontoFacturas)
        {
            Parametros.Clear();
            Parametros.Add("@Id_Cuenta", Id_Cuenta);
            Parametros.Add("@Pagada", Pagada);
            Parametros.Add("@Monto", Monto);
            Parametros.Add("@MontoFacturas", MontoFacturas != null ? MontoFacturas : null);

            List<Deuda> DeudaList = new List<Deuda>();
            dt = dbCon.Procedure("AMIGO", "DeudasSysUpdate", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Deuda deuda = new Deuda();
                        deuda.Id_Cuenta = Id_Cuenta;
                        DeudaList.Add(deuda);
                    }
                }
            }
            return DeudaList;
        }
        public List<Deuda> DeleteDeuda(int Pagada, int Id_Inscripcion, int Id_Arancel)
        {
            Parametros.Clear();
            Parametros.Add("@Pagada", Pagada);
            Parametros.Add("@Id_Inscripcion", Id_Inscripcion);
            Parametros.Add("@Id_Arancel", Id_Arancel);

            List<Deuda> DeudaList = new List<Deuda>();
            dt = dbCon.Procedure("AMIGO", "DeudasSysDelete", Parametros);

            try
            {
                if (dbCon.ErrorEstatus)
                {
                    if (dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Deuda deuda = new Deuda();
                            deuda.Id_Arancel = Id_Arancel;
                            DeudaList.Add(deuda);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return DeudaList;
        }
        public List<Deuda> InsertDeuda(int Id_Inscripcion, int Id_Arancel, decimal Monto, DateTime FechaVencimiento)
        {
            Parametros.Clear();
            Parametros.Add("@Id_Inscripcion", Id_Inscripcion);
            Parametros.Add("@Id_Arancel", Id_Arancel);
            Parametros.Add("@Monto", Monto);
            Parametros.Add("@FechaVencimiento", FechaVencimiento);

            List<Deuda> DeudaList = new List<Deuda>();
            dt = dbCon.Procedure("AMIGO", "DeudasSysInsert", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Deuda deuda = new Deuda();
                        deuda.Id_Inscripcion = Id_Inscripcion;
                        DeudaList.Add(deuda);
                    }
                }
            }
            return DeudaList;
        }
        public List<Deuda> GetDeudasExists(int Id_Inscripcion, int Id_Arancel, int Pagada)
        {
            Parametros.Clear();
            Parametros.Add("@Id_Inscripcion", Id_Inscripcion);
            Parametros.Add("@Id_Arancel", Id_Arancel);
            Parametros.Add("@Pagada", Pagada);

            List<Deuda> DeudaList = new List<Deuda>();
            dt = dbCon.Procedure("AMIGO", "DeudasExistentesSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Deuda deuda = new Deuda();
                        deuda.Id_Cuenta = Convert.ToInt32(dt.Rows[i]["Id_Cuenta"]);
                        deuda.Id_Inscripcion = Convert.ToInt32(dt.Rows[i]["Id_Inscripcion"]);
                        deuda.Id_Arancel = Convert.ToInt32(dt.Rows[i]["Id_Arancel"]);
                        deuda.Pagada = Convert.ToByte(dt.Rows[i]["Pagada"]);
                        DeudaList.Add(deuda);
                    }
                }
            }
            return DeudaList;
        }
    }
}