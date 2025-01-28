using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AltaUsuarioDTO
    {
        public string PrimerNombre { get; set; }
         public string PrimerApellido { get; set; }
        public string Pasaporte { get; set; }
        public int Id { get; private set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        //TODO: EMAIL DE USUARIO PARA PODER ENVIARLE CORREO Y VER EL TEMA DE QUQE LLEGUE TAMBIEN POR WP AGREGANDO EL TELEFONO


        public void SetId(int id)
        {
            Id = id;
        }

    }
}
