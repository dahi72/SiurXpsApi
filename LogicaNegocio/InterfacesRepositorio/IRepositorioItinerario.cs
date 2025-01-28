using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioItinerario : IRepositorio<Itinerario>
    {
        void AgregarEventoAItinerario(int idItinerario, EventoItinerario evento);
        void ModificarHorarioEvento(int idItinerario, int idEvento, DateTime nuevoHorario);
        void EliminarEvento(int idItinerario, int idEvento);
        bool ExisteGrupoEnItinerario(int idGrupo);
        IEnumerable<EventoItinerario> ObtenerEventosDeItinerario(int idItinerario);
        EventoItinerario ObtenerEventoDeItinerario(int idItinerario, int idEvento);
    }
}
