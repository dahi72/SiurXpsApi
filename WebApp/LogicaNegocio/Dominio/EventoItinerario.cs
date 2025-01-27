using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class EventoItinerario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ItinerarioId { get; set; }
         public Itinerario Iti { get; set; }
        public DateTime FechaYHora { get; set; }

        public int? ActividadId { get; set; }
        public Actividad? Actividad { get; set; } 

        public int? TrasladoId { get; set; }
        public Traslado? Traslado { get; set; }

        public Aeropuerto? Aeropuerto { get; set; }
        public int? AeropuertoId { get; set; }

        public Aerolinea? Aerolinea { get; set; }
        public int? AerolineaId { get; set; }

        public Hotel? Hotel { get; set; }
        public int? HotelId { get; set; }
 
        public Vuelo? Vuelo { get; set; }
        public int? VueloId { get; set; }


    }
}
