using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUItinerario;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUItinerario
{
    public class CUObtenerEventosDelItinerario : IObtenerEventosDelItinerario
    {
        private readonly IRepositorioItinerario RepoItinerario;


        public CUObtenerEventosDelItinerario(IRepositorioItinerario repoItinerario)
        {
            RepoItinerario = repoItinerario;
        }
        public IEnumerable<EventoItinerarioDTO> ObtenerEventos(int id)
        {
            
            IEnumerable <EventoItinerario> eventos = RepoItinerario.ObtenerEventosDeItinerario(id);

            var eventosDTO = eventos.Select(e => new EventoItinerarioDTO
            {
                Id = e.Id,
                ItinerarioId = e.ItinerarioId,
                FechaYHora = e.FechaYHora,
                ActividadId = e.ActividadId,
                TrasladoId = e.TrasladoId,
                AeropuertoId = e.AeropuertoId,
                AerolineaId = e.AerolineaId,
                HotelId = e.HotelId,
                VueloId = e.VueloId,
                
            }).ToList();

            return eventosDTO;
        }
    }
}
