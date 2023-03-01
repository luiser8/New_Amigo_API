using System;

namespace PSMApiRest.Lib
{
    public class Calculo
    {
        public static decimal TotalMonto(decimal MontoFacturas, decimal Cuota)
        {
            string decMath = Math.Abs(MontoFacturas).ToString();
            decimal dec = Convert.ToDecimal(decMath);
            decimal total = Cuota - dec;
            decimal totalToSave = total - (total % 0.01M);
            return totalToSave;
        }
        public static decimal ConvertMonto(decimal Monto)
        {
            string decMath = Math.Abs(Monto).ToString();
            decimal dec = Convert.ToDecimal(decMath);
            return dec;
        }
    }
}