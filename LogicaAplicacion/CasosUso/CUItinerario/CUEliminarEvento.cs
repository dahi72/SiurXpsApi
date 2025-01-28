using LogicaAplicacion.InterfacesCU.InterfacesCUItinerario;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUItinerario
{
    public class CUEliminarEvento : IEliminarEventoDeItinerario
    {
        private readonly IRepositorioItinerario RepoItinerario;

        public CUEliminarEvento (IRepositorioItinerario repoItinerario)
        {
            RepoItinerario = repoItinerario;
        }
        public void EliminarEvento(int idItinerario, int idEvento)
        {
           RepoItinerario.EliminarEvento(idItinerario,idEvento);
        }
    }
}
