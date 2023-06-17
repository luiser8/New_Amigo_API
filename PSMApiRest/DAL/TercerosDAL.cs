using System.Collections;
using System.Data;
using PSMApiRest.Lib;

namespace PSMApiRest.DAL
{
    public class TercerosDAL
    {
        private readonly DB dbCon;
        private DataTable dt;
        private readonly Hashtable Parametros;

        public TercerosDAL()
        {
            dt = new DataTable();
            dbCon = new DB();
            Parametros = new Hashtable();
        }
        public bool UpdateTerceros(string Id_Terceros, string Identificador, string Telefonos, string Emails)
        {
            Parametros.Clear();
            Parametros.Add("@Id_Terceros", Id_Terceros);
            Parametros.Add("@Identificador", Identificador);
            Parametros.Add("@Telefonos", Telefonos != "" ? Telefonos : null);
            Parametros.Add("@Emails", Emails != "" ? Emails : null);

            dbCon.Procedure("AMIGO", "TercerosSysUpdate", Parametros);

            return dbCon.ErrorEstatus;
        }
    }
}