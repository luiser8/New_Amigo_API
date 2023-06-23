using PSMApiRest.DAL;
using PSMApiRest.Models;
using System.Collections.Generic;

namespace PSMApiRest.Lib
{
    public class ActualizarCuotas
    {
        public List<Deuda> Establecer(decimal Cuota, byte Abono, string Lapso, int Pagada, int Tipo, byte TodasCuota, int Cuota1, int Cuota2, int Cuota3, int Cuota4, int Cuota5)
        {
            DeudaDAL deudaDAL = new DeudaDAL();
            List<Deuda> deudas = deudaDAL.GetAllDeudas(Lapso, Pagada, Tipo, TodasCuota, Cuota1, Cuota2, Cuota3, Cuota4, Cuota5);

            if (deudas.Count > 0)
            {
                for (int i = 0; i < deudas.Count; i++)
                {
                    List<Abono> abonos = deudaDAL.GetAbono(deudas[i].Id_Inscripcion, deudas[i].Id_Arancel, Abono);
                    if (abonos.Count > 0)
                    {
                        for (int j = 0; j < abonos.Count; j++)
                        {
                            deudaDAL.EditDeuda(deudas[i].Id_Cuenta, Pagada, Calculo.TotalMonto(abonos[j].Monto, Cuota), 0);
                        }
                    }
                    else
                    {
                        deudaDAL.EditDeuda(deudas[i].Id_Cuenta, Pagada, Calculo.ConvertMonto(Cuota), 0);
                    }
                }
            }
            return deudas;
        }
    }
}