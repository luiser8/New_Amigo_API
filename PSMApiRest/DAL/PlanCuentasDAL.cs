using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using PSMApiRest.Lib;
using PSMApiRest.Models;

namespace PSMApiRest.DAL
{
    public class PlanCuentasDAL
    {
        private readonly DB dbCon;
        private DataTable dt;
        private readonly Hashtable Parametros;

        public PlanCuentasDAL()
        {
            dt = new DataTable();
            dbCon = new DB();
            Parametros = new Hashtable();
        }
        public List<PlanCuentas> GetPlanCuentas(byte Todos, string NombreFiltro)
        {
            Parametros.Clear();
            Parametros.Add("@Todos", Todos);
            Parametros.Add("@IdCuenta", null);
            Parametros.Add("@NombreFiltro", NombreFiltro);

            List<PlanCuentas> planCuentasList = new List<PlanCuentas>();
            dt = dbCon.Procedure("AMIGO", "PlanCuentasSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        PlanCuentas planCuentas = new PlanCuentas
                        {
                            IdCuenta = Convert.ToInt32(dt.Rows[i]["Id_Cuenta"]),
                            IdTipoCuenta = Convert.ToInt32(dt.Rows[i]["Id_TipoCuenta"]),
                            IdInstitucion = Convert.ToInt32(dt.Rows[i]["Id_Institucion"]),
                            Numero = Convert.ToString(dt.Rows[i]["Numero"]),
                            Nombre = Convert.ToString(dt.Rows[i]["Nombre"]),
                            NombreIdioma2 = Convert.ToString(dt.Rows[i]["NombreIdioma2"]),
                            Descripcion = Convert.ToString(dt.Rows[i]["Descripcion"]),
                            ManejaAuxiliares = Convert.ToByte(dt.Rows[i]["ManejaAuxiliares"]),
                            Movimiento = Convert.ToByte(dt.Rows[i]["Movimiento"]),
                            Debe = Convert.ToDecimal(dt.Rows[i]["Debe"]),
                            Haber = Convert.ToDecimal(dt.Rows[i]["Haber"]),
                            Activa = Convert.ToByte(dt.Rows[i]["Activa"]),
                            AumentaDebe = Convert.ToByte(dt.Rows[i]["AumentaDebe"]),
                        };
                        planCuentasList.Add(planCuentas);
                    }
                }
            }
            return planCuentasList;
        }

        public List<PlanCuentas> GetPlanCuentasById(int IdCuenta)
        {
            Parametros.Clear();
            Parametros.Add("@Todos", 4);
            Parametros.Add("@IdCuenta", IdCuenta);
            Parametros.Add("@NombreFiltro", null);

            List<PlanCuentas> planCuentasList = new List<PlanCuentas>();
            dt = dbCon.Procedure("AMIGO", "PlanCuentasSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        PlanCuentas planCuentas = new PlanCuentas
                        {
                            IdCuenta = Convert.ToInt32(dt.Rows[i]["Id_Cuenta"]),
                            IdTipoCuenta = Convert.ToInt32(dt.Rows[i]["Id_TipoCuenta"]),
                            IdInstitucion = Convert.ToInt32(dt.Rows[i]["Id_Institucion"]),
                            Numero = Convert.ToString(dt.Rows[i]["Numero"]),
                            Nombre = Convert.ToString(dt.Rows[i]["Nombre"]),
                            NombreIdioma2 = Convert.ToString(dt.Rows[i]["NombreIdioma2"]),
                            Descripcion = Convert.ToString(dt.Rows[i]["Descripcion"]),
                            ManejaAuxiliares = Convert.ToByte(dt.Rows[i]["ManejaAuxiliares"]),
                            Movimiento = Convert.ToByte(dt.Rows[i]["Movimiento"]),
                            Debe = Convert.ToDecimal(dt.Rows[i]["Debe"]),
                            Haber = Convert.ToDecimal(dt.Rows[i]["Haber"]),
                            Activa = Convert.ToByte(dt.Rows[i]["Activa"]),
                            AumentaDebe = Convert.ToByte(dt.Rows[i]["AumentaDebe"]),
                        };
                        planCuentasList.Add(planCuentas);
                    }
                }
            }
            return planCuentasList;
        }

        public bool PutPlanCuentas (PlanCuentas planCuentas)
        {
            try
            {
                Parametros.Clear();
                Parametros.Add("@IdCuenta", planCuentas.IdCuenta);
                Parametros.Add("@IdTipoCuenta", planCuentas.IdTipoCuenta);
                Parametros.Add("@IdInstitucion", planCuentas.IdInstitucion);
                Parametros.Add("@Numero", planCuentas.Numero);
                Parametros.Add("@Nombre", planCuentas.Nombre);
                Parametros.Add("@NombreIdioma2", planCuentas.NombreIdioma2);
                Parametros.Add("@Descripcion", planCuentas.Descripcion);
                Parametros.Add("@ManejaAuxiliares", planCuentas.ManejaAuxiliares);
                Parametros.Add("@Movimiento", planCuentas.Movimiento);
                Parametros.Add("@Debe", planCuentas.Debe);
                Parametros.Add("@Haber", planCuentas.Haber);
                Parametros.Add("@AumentaDebe", planCuentas.AumentaDebe);
                Parametros.Add("@Activa", planCuentas.Activa);

                dbCon.Procedure("AMIGO", "PlanCuentasEditSys", Parametros);

                return dbCon.ErrorEstatus;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}