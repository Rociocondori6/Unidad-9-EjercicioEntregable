using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Models
{
    public class Profesor : Usuario
    {   
        public string Especialidad { get; set; }
        public int Sueldo { get; set; }
    }
}
