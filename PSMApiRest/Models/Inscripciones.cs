using System;
using System.Collections.Generic;

namespace PSMApiRest.Models
{
    public class Inscripciones
    {
        public int Id_Periodo { get; set; }
        public string Lapso { get; set; }
        public int Plan0 { get; set; }
        public int Plan1 { get; set; }
        public int Plan2 { get; set; }
        public int Plan3 { get; set; }
        public int Plan4 { get; set; }
        public int Plan5 { get; set; }
        public int Plan6 { get; set; }
        public int Plan7 { get; set; }
        public int Plan8 { get; set; }
        public int Plan9 { get; set; }
        public int Plan10 { get; set; }
        public int Id_Terceros { get; set; }
        public int Id_Inscripcion { get; set; }
        public int Id_Arancel { get; set; }
        public int Id_Plan { get; set; }
        public int Id_TipoIngreso { get; set; }
        public int Id_Carrera { get; set; }
        public string PlanDePago { get; set; }
        public string TipoIngreso { get; set; }
        public decimal Monto { get; set; }
        public string FechaVencimiento { get; set; }
        public string Telefonos { get; set; }
        public string Emails { get; set; }
        public DateTime Fecha { get; set; }
        public virtual ICollection<Factura> Factura { get; set; }
    }
}