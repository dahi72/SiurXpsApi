using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class TrasladoDTO
    {
        public int Id { get; set; }
        public string LugarDeEncuentro { get; set; }
        public TimeOnly Horario { get; set; }
        public int TipoDeTraslado { get; set; }
        public void SetId(int id)
        {
            Id = id;
        }
    }
}
