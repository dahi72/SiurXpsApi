using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AerolineaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string PaginaWeb { get; set; }
        public void SetId(int id)
        {
            Id = id;
        }
    }
}
