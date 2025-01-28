using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ItinerarioDTO
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int GrupoDeViajeId { get; set; }
        public List<int> EventosIds { get; set; } = new List<int>();
        public List<EventoItinerarioDTO> Eventos { get; set; } = new List<EventoItinerarioDTO>();

        public void SetId(int id)
        {
            Id = id;
        }


    }
}
