using System;

namespace PSMApiRest.Models
{
    public class Alumno
    {
        public int IdAl { get; set; }
        public string Cedula { get; set; }
        public string Fullnombre { get; set; }
        public Byte[] Foto { get; set; }
        public byte Sexo { get; set; }
        public string LapCur { get; set; }
        public string EstAca { get; set; }
        public int codcarrera { get; set; }
    }
}