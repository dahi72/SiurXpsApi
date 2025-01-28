using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CambiarContrasenaDTO
    {
        public string Pasaporte { get; set; }
        public string ContrasenaActual { get; set; }
        public string NuevaPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}

