using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUItinerario;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUItinerario
{
    public class CUBuscarItinerario : IBuscarItinerario
    {
        private readonly IRepositorioItinerario RepoItinerario;

        public CUBuscarItinerario(IRepositorioItinerario repo)
        {
            RepoItinerario = repo;
        }

        public ItinerarioDTO Buscar(int id)
        {
            Itinerario itinerario = RepoItinerario.FindById(id);

        
            if (itinerario == null)
            {
                throw new Exception("Itinerario no encontrado.");
            }

      
            ItinerarioDTO itinerarioDTO = new ItinerarioDTO
            {
                Id = itinerario.Id,
                FechaInicio = itinerario.FechaInicio,
                FechaFin = itinerario.FechaFin,
                GrupoDeViajeId = itinerario.GrupoDeViajeId,
                EventosIds = itinerario.Eventos.Select(e => e.Id).ToList(),


            };

            return itinerarioDTO;
        }
    }
    
}
