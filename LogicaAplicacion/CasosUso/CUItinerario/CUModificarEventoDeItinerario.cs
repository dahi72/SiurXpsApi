using LogicaAplicacion.InterfacesCU.InterfacesCUItinerario;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUItinerario
{
    public class CUModificarEventoDeItinerario : IModificarEvento
    {
        private readonly IRepositorioItinerario RepoItinerario;

        public CUModificarEventoDeItinerario(IRepositorioItinerario repoItinerario)
        {
            RepoItinerario = repoItinerario;
        }

        public void ModificarEvento(int idItinerario, int idEvento, DateTime nuevoHorario)
        {
            if (nuevoHorario == default)
            {
                throw new ArgumentException("El nuevo horario no puede ser nulo o por defecto.");
            }

            // Delegar al repositorio para realizar la lógica principal
            RepoItinerario.ModificarHorarioEvento(idItinerario, idEvento, nuevoHorario);
        }
    }
}
