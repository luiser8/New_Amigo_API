using PSMApiRest.DAL;
using System;
using System.Collections.Generic;

namespace PSMApiRest.Models
{
    public class InsertarCuotasNuevas
    {
        public List<Inscripciones> Establecer(Inscripciones inscripcionesRequest)
        {
            InscripcionesDAL inscripcionesDAL = new InscripcionesDAL();
            FacturaDAL facturaDAL = new FacturaDAL();
            DeudaDAL deudaDAL = new DeudaDAL();
            List<Inscripciones> inscripciones = inscripcionesDAL.GetInscripciones(inscripcionesRequest);

            if (inscripciones.Count > 0)
            {
                for (int i = 0; i < inscripciones.Count; i++)
                {
                    var facturaCount = facturaDAL.GetFacturaExists(inscripciones[i].Id_Inscripcion, inscripcionesRequest.Id_Arancel);
                    var deudaCount = deudaDAL.GetDeudasExists(inscripciones[i].Id_Inscripcion, inscripcionesRequest.Id_Arancel);
                    if (
                        facturaCount.Count == 0 && deudaCount.Count == 0)
                    {
                        inscripcionesDAL.InsertCuota(inscripciones[i].Id_Inscripcion, inscripcionesRequest.Id_Arancel, inscripcionesRequest.Monto, inscripcionesRequest.FechaVencimiento);
                    }
                }
            }
            return inscripciones;
        }
        public List<Inscripciones> EstablecerSAIA(Inscripciones inscripcionesRequest)
        {
            InscripcionesDAL inscripcionesDAL = new InscripcionesDAL();
            FacturaDAL facturaDAL = new FacturaDAL();
            DeudaDAL deudaDAL = new DeudaDAL();
            List<Inscripciones> inscripciones = inscripcionesDAL.GetInscripciones(inscripcionesRequest);

            if (inscripciones.Count > 0)
            {
                for (int i = 0; i < inscripciones.Count; i++)
                {
                    if (
                        facturaDAL.GetFacturaExists(inscripciones[i].Id_Inscripcion, inscripcionesRequest.Id_Arancel).Count == 0
                        &&
                        deudaDAL.GetDeudasExists(inscripciones[i].Id_Inscripcion, inscripcionesRequest.Id_Arancel).Count == 0)
                    {
                        inscripcionesDAL.InsertCuota(inscripciones[i].Id_Inscripcion, inscripcionesRequest.Id_Arancel, inscripcionesRequest.Monto, inscripcionesRequest.FechaVencimiento);
                    }
                }
            }
            return inscripciones;
        }
    }
}