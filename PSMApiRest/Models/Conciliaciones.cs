using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSMApiRest.Models
{
    public class Conciliaciones
    {
        public int Id_Archivo { get; set; }
        public int Id_Banco { get; set; }
        public string Banco { get; set; }
        public int Id_Usuario { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoFinal { get; set; }
        public byte Cerrado { get; set; }
    }
}