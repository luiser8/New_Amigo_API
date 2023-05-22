namespace PSMApiRest.Models
{
    public class PlanCuentas
    {
        public int IdCuenta { get; set; }
        public int IdTipoCuenta { get; set; }
        public int IdInstitucion { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public string NombreIdioma2 { get; set; }
        public string Descripcion { get; set; }
        public byte ManejaAuxiliares { get; set; }
        public byte Movimiento { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
        public byte Activa { get; set; }
        public byte AumentaDebe { get; set; }
    }
}