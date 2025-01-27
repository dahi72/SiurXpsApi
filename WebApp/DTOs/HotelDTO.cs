using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTOs
{
    public class HotelDTO
    {

        public int Id { get; set; } 
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int PaisId { get; set; }
        public int CiudadId { get; set; }
        public TimeSpan CheckIn { get; set; }
        public TimeSpan CheckOut { get; set; }
        public string? PaginaWeb { get; set; }

      

        public void SetId(int id)
        {
            Id = id;
        }

    }
}
