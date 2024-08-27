using System;
using System.Collections.Generic;

namespace PSMApiRest.Models
{

    public class DeudaPayload
    {
        public bool Puerta { get; set; }
        public string Lapso { get; set; }
        public string Identificador { get; set; }
    }

    public class Respuesta
    {
        public bool? NoPasa { get; set; } = false;
        public bool? EsBecado { get; set; } = false;
        public bool? PagoTodo { get; set; } = false;
        public bool? EsDesertor { get; set; } = false;
        public bool? Existe { get; set; } = false;
        public bool? EsEgresado { get; set; } = false;
        public bool? SinDocumentos { get; set; }
        public bool? EsAmonestado { get; set; }
        public string PlanDePago { get; set; }
        public List<Deuda> Deudas { get; set; }
    }
    public class Deuda
    {
        public int Id_Cuenta { get; set; }
        public int Id_Inscripcion { get; set; }
        public int Id_Arancel { get; set; }
        public byte Pagada { get; set; }
        public string Lapso { get; set; }
        public string Identificador { get; set; }
        public string Cuota { get; set; }
        public decimal Monto { get; set; }
        public decimal MontoFacturas { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaVencimiento { get; set; }
        //public bool? NoPasa { get; set; } = false;
    }
}