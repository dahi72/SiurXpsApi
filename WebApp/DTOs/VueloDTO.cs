using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class VueloDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TimeOnly Horario { get; set; }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
