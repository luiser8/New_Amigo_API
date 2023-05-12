using System;

namespace PSMApiRest.Models
{
    public class PeriodosContables
    {
        public int Id_Periodo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public string Descripcion { get; set; }
        public byte Bloqueado { get; set; }
    }
}