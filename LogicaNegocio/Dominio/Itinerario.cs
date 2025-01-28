using ExcepcionesPropias;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class Itinerario : IValidable
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public GrupoDeViaje Grupo { get; set; }
        public int GrupoDeViajeId { get; set; }
         public List<EventoItinerario>? Eventos { get; set; }

        public void Validar()
        {
            if (FechaInicio < FechaFin)
            {
                throw new ItinerarioException("La fecha de inicio del viaje no puede ser menor a la fecha de fin");
            }
            if (FechaInicio < DateTime.Now || FechaFin<DateTime.Now)
            {
                throw new ItinerarioException("La fecha de inicio del viaje no puede anterior a hoy");
            }
        }
    }
}
