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

        public Respuesta GetDeuda(bool Puerta, string Lapso, string Identificador)
        {
            Parametros.Clear();
            Parametros.Add("@Puerta", Puerta ? 1 : 0);
            Parametros.Add("@Lapso", Lapso);
            Parametros.Add("@Identificador", Identificador != "" ? Identificador : null);

            Respuesta respuesta = new Respuesta();
            List<Deuda> deudasList = new List<Deuda>();
            dt = dbCon.Procedure("AMIGO", "DeudasSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var deudas = new Deuda
                        {
                            Id_Cuenta = dt.Rows[i]["Id_Cuenta"] != null ? Convert.ToInt32(dt.Rows[i]["Id_Cuenta"]) : 0,
                            Id_Inscripcion = dt.Rows[i]["Id_Inscripcion"] != null ? Convert.ToInt32(dt.Rows[i]["Id_Inscripcion"]) : 0,
                            Id_Arancel = dt.Rows[i]["Id_Arancel"] != null ? Convert.ToInt32(dt.Rows[i]["Id_Arancel"]) : 0,
                            Identificador = dt.Rows[i]["Identificador"] != null ? Convert.ToString(dt.Rows[i]["Identificador"]) : "",
                            Cuota = dt.Rows[i]["Cuota"] != null ? Convert.ToString(dt.Rows[i]["Cuota"]) : "",
                            Lapso = dt.Rows[i]["Lapso"] != null ? Convert.ToString(dt.Rows[i]["Lapso"]) : "",
                            Pagada = Convert.ToByte(dt.Rows[i]["Pagada"]),
                            Monto = dt.Rows[i]["Monto"] != null ? Convert.ToInt32(dt.Rows[i]["Monto"]) : 0,
                            MontoFacturas = Puerta ? 0 : Convert.ToInt32(dt.Rows[i]["MontoFacturas"]),
                            FechaVencimiento = Puerta || dt.Rows[i]["FechaVencimiento"] == null ? DateTime.Now : Convert.ToDateTime(dt.Rows[i]["FechaVencimiento"]),
                            Total = dt.Rows[i]["Total"] != null ? Math.Floor(Convert.ToDecimal(dt.Rows[i]["Total"]) * 100) / 100 : 0,
                        };
                        if (Convert.ToInt32(dt.Rows[i]["Monto"]) > 0)
                        {
                            deudasList.Add(deudas);
                        }
                    }
                }
                respuesta.Deudas = deudasList;
            }
            return respuesta;
        }
        public List<Deuda> GetAllDeudas(string Lapso, int Pagada, int Tipo, byte TodasCuota, int Cuota1, int Cuota2, int Cuota3, int Cuota4, int Cuota5)
        {
            Parametros.Clear();
            Parametros.Add("@Lapso", Lapso);
            Parametros.Add("@Pagada", Pagada);
            Parametros.Add("@Tipo", Tipo);
            Parametros.Add("@TodasCuota", TodasCuota);
            Parametros.Add("@Cuota1", Cuota1);
            Parametros.Add("@Cuota2", Cuota2);
            Parametros.Add("@Cuota3", Cuota3);
            Parametros.Add("@Cuota4", Cuota4);
            Parametros.Add("@Cuota5", Cuota5);

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
        public bool DeudaListSinDocumentos(long identificador)
        {
            bool existe = false;

            int[] alumnosId = SinDocumentos.getSinDocumentos();

            if (alumnosId.Length > 0)
            {
                foreach (var item in alumnosId)
                {
                    if (item == identificador)
                    {
                        existe = true; break;
                    }
                }
            }

            return existe;
        }
    }
}