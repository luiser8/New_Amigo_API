using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PSMApiRest.Lib;
using PSMApiRest.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PSMApiRest.DAL
{
    public class FacturaDAL
    {
        private readonly DB dbCon;
        private DataTable dt;
        private readonly Hashtable Parametros;

        public FacturaDAL()
        {
            dt = new DataTable();
            dbCon = new DB();
            Parametros = new Hashtable();
        }
        public List<Factura> GetFactura(int Id_Inscripcion)
        {
            Parametros.Clear();
            Parametros.Add("@Id_Inscripcion", Id_Inscripcion);

            List<Factura> FacturaList = new List<Factura>();
            dt = dbCon.Procedure("AMIGO", "FacturasSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Factura factura = new Factura();
                        factura.Id_Factura = Convert.ToInt32(dt.Rows[i]["Id_Factura"]);
                        factura.Id_Detalle = Convert.ToInt32(dt.Rows[i]["Id_Detalle"]);
                        factura.Id_Arancel = Convert.ToInt32(dt.Rows[i]["Id_Arancel"]);
                        factura.Id_Inscripcion = Convert.ToInt32(dt.Rows[i]["Id_Inscripcion"]);
                        factura.Monto = Convert.ToDecimal(dt.Rows[i]["Monto"]);
                        factura.Abono = Convert.ToByte(dt.Rows[i]["Abono"]);
                        factura.Anulada = Convert.ToByte(dt.Rows[i]["Anulada"]);
                        factura.Descripcion = Convert.ToString(dt.Rows[i]["Descripcion"]);
                        factura.Hora = Convert.ToDateTime(dt.Rows[i]["Hora"]);
                        FacturaList.Add(factura);
                    }
                }
            }
            return FacturaList;
        }
        public List<Factura> GetFacturaExists(int Id_Inscripcion, int Id_Arancel)
        {
            Parametros.Clear();
            Parametros.Add("@Id_Inscripcion", Id_Inscripcion);
            Parametros.Add("@Id_Arancel", Id_Arancel);

            List<Factura> FacturaList = new List<Factura>();
            dt = dbCon.Procedure("AMIGO", "FacturasExistentesSys", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Factura factura = new Factura();
                        factura.Id_Factura = Convert.ToInt32(dt.Rows[i]["Id_Factura"]);
                        FacturaList.Add(factura);
                    }
                }
            }
            return FacturaList;
        }
        public List<FacturaEstudiante> GetFacturaMontoYDepositos(int Id_Factura)
        {
            Parametros.Clear();
            Parametros.Add("@IdFactura", Id_Factura);

            List<FacturaEstudiante> FacturaList = new List<FacturaEstudiante>();
            dt = dbCon.Procedure("AMIGO_PUERTA", "FacturaSelect", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];

                        FacturaEstudiante factura = new FacturaEstudiante
                        {
                            // Datos básicos de factura
                            Id_Factura = Convert.ToInt32(row["Id_Factura"]),
                            Id_Detalle = Convert.ToInt32(row["Id_Detalle"]),
                            Id_Arancel = Convert.ToInt32(row["Id_Arancel"]),
                            Id_Inscripcion = Convert.ToInt32(row["Id_Inscripcion"]),
                            Monto = Convert.ToString(row["Monto"]),

                            // Nuevos campos del SELECT
                            Identificador = row["Identificador"]?.ToString(),
                            FechaFactura = Convert.ToString(row["FechaFactura"]),
                            Concepto = row["Concepto"]?.ToString(),
                            MontoConcepto = Convert.ToString(row["MontoConcepto"]),
                            Id_Nivel = Convert.ToInt32(row["Id_Nivel"]),
                            Id_Periodo = Convert.ToInt32(row["Id_Periodo"]),
                            Periodo = row["Periodo"]?.ToString(),

                            SaldoAFavorJson = JsonConvert.DeserializeObject<SaldoAFavor>(row["SaldoAFavorJson"]?.ToString()),
                            DepositosArray = DeserializarDepositos(row),
                        }
                    ;

                        FacturaList.Add(factura);
                    }
                }
            }
            return FacturaList;
        }
        public int InsertSaldoAFavor(SaldoAFavorDto saldoAFavorDto)
        {
            Parametros.Clear();
            Parametros.Add("@IdFactura", saldoAFavorDto.Id_Factura);
            Parametros.Add("@Cedula", saldoAFavorDto.Cedula);
            Parametros.Add("@Monto", Math.Round(saldoAFavorDto.Monto, 2));

            var result = 0;

            dt = dbCon.Procedure("AMIGO_PUERTA", "SaldoAFavorInsert", Parametros);

            if (dbCon.ErrorEstatus)
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result = Convert.ToInt32(dt.Rows[i]["Id"]);
                    }
                }
            }

            return result;
        }
        public bool InsertDeposito(DepositoDto depositoDto)
        {
            var result = false;
            try
            {
                Parametros.Clear();
                Parametros.Add("@IdFactura", depositoDto.Id_Factura);
                Parametros.Add("@IdBanco", depositoDto.Id_Banco);
                Parametros.Add("@Referencia", depositoDto.Referencia);
                Parametros.Add("@Fecha", depositoDto.Fecha);
                Parametros.Add("@Monto", Math.Round(depositoDto.Monto, 2));
                Parametros.Add("@Tipo", depositoDto.Tipo);

                dt = dbCon.Procedure("AMIGO_PUERTA", "DepositoInsert", Parametros);

                result = dbCon.ErrorEstatus;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        private List<DepositosArray> DeserializarDepositos(DataRow row)
        {
            var jsonString = row["DepositosArray"]?.ToString();

            // 🔴 DEPURACIÓN: Ver qué JSON está llegando
            System.Diagnostics.Debug.WriteLine("========== JSON RECIBIDO ==========");
            System.Diagnostics.Debug.WriteLine(jsonString);
            System.Diagnostics.Debug.WriteLine("===================================");

            if (string.IsNullOrEmpty(jsonString) || jsonString == "[]")
                return new List<DepositosArray>();

            try
            {
                // Limpiar el JSON si es necesario
                jsonString = System.Text.RegularExpressions.Regex.Replace(jsonString, @"\s+", " ");

                // 🔴 Ver JSON después de limpiar
                System.Diagnostics.Debug.WriteLine("JSON después de limpiar:");
                System.Diagnostics.Debug.WriteLine(jsonString);

                var settings = new JsonSerializerSettings
                {
                    DateFormatString = "yyyy-MM-ddTHH:mm:ss",
                    NullValueHandling = NullValueHandling.Ignore
                };

                return JsonConvert.DeserializeObject<List<DepositosArray>>(jsonString, settings);
            }
            catch (JsonException ex)
            {
                // Log del error
                Console.WriteLine($"Error deserializando JSON: {ex.Message}");
                Console.WriteLine($"JSON original: {jsonString}");

                // 🔴 Intentar una solución más directa
                try
                {
                    // Si el JSON viene como objeto en lugar de array
                    if (jsonString.Trim().StartsWith("{"))
                    {
                        var singleObject = JsonConvert.DeserializeObject<DepositosArray>(jsonString);
                        return new List<DepositosArray> { singleObject };
                    }

                    // Intentar reparar el JSON común de SQL Server 2014
                    jsonString = jsonString
                        .Replace("\"{", "{")
                        .Replace("}\"", "}")
                        .Replace("\\\"", "\"");

                    // Quitar comillas dobles extras alrededor del array
                    if (jsonString.StartsWith("\"[") && jsonString.EndsWith("]\""))
                    {
                        jsonString = jsonString.Substring(2, jsonString.Length - 4);
                    }

                    var jArray = JArray.Parse(jsonString);
                    return jArray.ToObject<List<DepositosArray>>();
                }
                catch (Exception ex2)
                {
                    Console.WriteLine($"Error en fallback: {ex2.Message}");
                    Console.WriteLine($"JSON problemático: {jsonString}");
                    return new List<DepositosArray>();
                }
            }
        }
    }
}