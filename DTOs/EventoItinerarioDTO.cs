using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class EventoItinerarioDTO
    {
        public int Id { get; set; }
        public int ItinerarioId { get; set; }
        public DateTime FechaYHora { get; set; }
        public int? ActividadId { get; set; }
        public int? TrasladoId { get; set; }
        public int? AeropuertoId { get; set; }
        public int? AerolineaId { get; set; }
        public int? HotelId { get; set; }
        public int? VueloId { get; set; }
    }
}
