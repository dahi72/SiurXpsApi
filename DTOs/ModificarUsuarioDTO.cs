using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ModificarUsuarioDTO {
        public string PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string Pasaporte { get; set; }
        public string? Email { get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaNac { get; set; }


        [NotMapped]
        public IFormFile? PasaporteDocumento { get; set; }

        [NotMapped]
        public IFormFile? VisaDocumento { get; set; }

        [NotMapped]
        public IFormFile? VacunasDocumento { get; set; }

        [NotMapped]
        public IFormFile? SeguroDeViajeDocumento { get; set; }
    


    }
}
