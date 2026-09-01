using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Models
{
    public class Estudiante : Usuario
    {
        public string Legajo { get; set; }
        public double Promedio { get; set; }
    }
}
