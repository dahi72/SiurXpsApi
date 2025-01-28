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
    public class CUAgregarEventoAItinerario : IAgregarEventoAItinerario
    {
        private readonly IRepositorioItinerario RepoItinerario;


        public CUAgregarEventoAItinerario(IRepositorioItinerario repoItinerario)
        {
            RepoItinerario = repoItinerario;
        }

        public void AgregarEvento(int idItinerario, EventoItinerarioDTO evento)
        {
            // Verificar si el itinerario existe
            var itinerario = RepoItinerario.FindById(idItinerario);
            if (itinerario == null)
            {
                throw new Exception("Itinerario no encontrado");
            }

            // Validar que la fecha y hora sean correctas
            if (evento.FechaYHora == default)
            {
                throw new Exception("La fecha y hora del evento es obligatoria");
            }

            // Llamar al repositorio para agregar el evento al itinerario
            EventoItinerario eventoIti = new EventoItinerario
            {
                FechaYHora = evento.FechaYHora,
                ActividadId = evento.ActividadId,
                TrasladoId = evento.TrasladoId,
                AeropuertoId = evento.AeropuertoId,
                AerolineaId = evento.AerolineaId,
                HotelId = evento.HotelId,
                VueloId = evento.VueloId
            };

            // Llamamos al repositorio para agregar el evento (logica ya manejada por el repositorio)
            RepoItinerario.AgregarEventoAItinerario(idItinerario, eventoIti);
        }
    }
}
