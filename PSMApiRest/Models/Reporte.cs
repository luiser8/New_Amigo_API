﻿using System;

namespace PSMApiRest.Models
{
    public class Reporte
    {
        public string Lapso { get; set; }
        public long IdFactura { get; set; }
        public string Fullnombre { get; set; }
        public string Identificador { get; set; }
        public string Telefonos { get; set; }
        public string Email { get; set; }
        public string Descripcion { get; set; }
        public string Cuota { get; set; }
        public decimal Dolar { get; set; }
        public decimal Monto { get; set; }
        public decimal MontoFacturas { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
    }
    public class ReporteMenu
    {
        public int IdPlan { get; set; }
        public string PlanPago { get; set; }
        public int Inscritos { get; set; }
    }
    public class ReporteMenuCarreras
    {
        public int IdCarrera { get; set; }
        public string Carrera { get; set; }
        public int Inscritos { get; set; }
    }
    public class ReportePlanDePago
    {
        //public int Sexo { get; set; }
        //public int Id_Alumno { get; set; }
        public string Cedula { get; set; }
        //public string Apellidos { get; set; }
        //public string Nombres { get; set; }
        public string Telefonos { get; set; }
        public string EMail { get; set; }
        public string Carrera { get; set; }
        public string TiposIngreso { get; set; }
        public string PlanDePago { get; set; }
        public DateTime Fecha { get; set; }
    }
    public class ReportePorCarreras
    {
        //public int Sexo { get; set; }
        //public int Id_Alumno { get; set; }
        public string Cedula { get; set; }
        //public string Apellidos { get; set; }
        //public string Nombres { get; set; }
        public string Telefonos { get; set; }
        public string EMail { get; set; }
        public string Carrera { get; set; }
        public string TiposIngreso { get; set; }
        public string PlanDePago { get; set; }
        public DateTime Fecha { get; set; }
    }
    public class ReporteFacturacion
    {
        public DateTime FechaDelPago { get; set; }
        public string NroReferencia { get; set; }
        public string NombresYapellidos { get; set; }
        public string Cedula { get; set; }
        public string EMail { get; set; }
        public string Escuela { get; set; }
        public decimal Monto { get; set; }
        public decimal MontoIP { get; set; }
        public string Concepto { get; set; }
        public DateTime FechaRegistroPago { get; set; }
        public string NroReciboCaja { get; set; }
        public int Tipo { get; set; }

    }
}