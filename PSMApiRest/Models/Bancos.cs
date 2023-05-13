namespace PSMApiRest.Models
{
    public class Bancos
    {
        public int Id_Banco { get; set; }
        public int Id_Cuenta { get; set; }
        public int Id_SubEspecifica { get; set; }
        public int Id_CuentaDebito { get; set; }
        public int Id_FormatoConciliacion { get; set; }
        public string NumeroCuenta { get; set; }
        public string Descripcion { get; set; }
        public int Tipo { get; set; }
        public byte Activa { get; set; }
    }
}