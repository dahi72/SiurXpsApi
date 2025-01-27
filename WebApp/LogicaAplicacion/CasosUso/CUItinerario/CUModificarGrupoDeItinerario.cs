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
    public class CUModificarGrupoDeItinerario : IModificarGrupoDeItinerario
    {
        private readonly IRepositorioItinerario RepoItinerario;

        public CUModificarGrupoDeItinerario(IRepositorioItinerario repo)
        {
            RepoItinerario = repo;
        }

        public void Modificar(int idItinerario, int idGrupo)
        {
            Itinerario itinerarioExistente = RepoItinerario.FindById(idItinerario);

            if (itinerarioExistente == null)
            {
                throw new InvalidOperationException($"No se encontró el itinerario");
            }
            itinerarioExistente.GrupoDeViajeId = idGrupo;

            if (!RepoItinerario.ExisteGrupoEnItinerario(idGrupo))
            {
                RepoItinerario.Update(itinerarioExistente);

            }
            else
            {
                throw new InvalidOperationException($"El grupo ya tiene un itinerario asociado.");
            }
           



        }
    }
}
