using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using PSMApiRest.Lib;
using PSMApiRest.Models;

namespace PSMApiRest.DAL
{
    public class FacturaDAL
    {
        private readonly DB dbCon;
        private DataTable dt;
        private readonly Hashtable Parametros;

        public FacturaDAL()
        {
            dt = new DataTable();
            dbCon = new DB();
            Parametros = new Hashtable();
        }
        public List<Factura> GetFactura(int Id_Inscripcion)
        {
            Parametros.Clear();
            Parametros.Add("@Id_Inscripcion", Id_Inscripcion);

            List<Factura> FacturaList = new List<Factura>();
            dt = dbCon.Procedure("AMIGO", "FacturasSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Factura factura = new Factura();
                        factura.Id_Factura = Convert.ToInt32(dt.Rows[i]["Id_Factura"]);
                        factura.Id_Detalle = Convert.ToInt32(dt.Rows[i]["Id_Detalle"]);
                        factura.Id_Arancel = Convert.ToInt32(dt.Rows[i]["Id_Arancel"]);
                        factura.Id_Inscripcion = Convert.ToInt32(dt.Rows[i]["Id_Inscripcion"]);
                        factura.Monto = Convert.ToDecimal(dt.Rows[i]["Monto"]);
                        factura.Abono = Convert.ToByte(dt.Rows[i]["Abono"]);
                        factura.Anulada = Convert.ToByte(dt.Rows[i]["Anulada"]);
                        factura.Descripcion = Convert.ToString(dt.Rows[i]["Descripcion"]);
                        factura.Hora = Convert.ToDateTime(dt.Rows[i]["Hora"]);
                        FacturaList.Add(factura);
                    }
                }
            }
            return FacturaList;
        }
        public List<Factura> GetFacturaExists(int Id_Inscripcion, int Id_Arancel)
        {
            Parametros.Clear();
            Parametros.Add("@Id_Inscripcion", Id_Inscripcion);
            Parametros.Add("@Id_Arancel", Id_Arancel);

            List<Factura> FacturaList = new List<Factura>();
            dt = dbCon.Procedure("AMIGO", "FacturasExistentesSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Factura factura = new Factura();
                        factura.Id_Factura = Convert.ToInt32(dt.Rows[i]["Id_Factura"]);
                        FacturaList.Add(factura);
                    }
                }
            }
            return FacturaList;
        }
    }
}