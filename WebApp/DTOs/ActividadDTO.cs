using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ActividadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Ubicacion { get; set; }
        public int Duracion { get; set; }
        public bool Opcional { get; set; }
        public void SetId(int id)
        {
            Id = id;
        }
    }
}
