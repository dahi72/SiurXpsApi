using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUItinerario;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUItinerario
{
    public class CUListadoItinerarios : IListadoItinerarios
    {
        private readonly IRepositorioItinerario RepoItinerario;

        public CUListadoItinerarios(IRepositorioItinerario repo)
        {
            RepoItinerario= repo;
        }
        public IEnumerable<ItinerarioDTO> Listado()
        {
            return RepoItinerario.FindAll().Select(i => new ItinerarioDTO
            {
                Id = i.Id,
                FechaInicio = i.FechaInicio,
                FechaFin = i.FechaFin,
                GrupoDeViajeId = i.GrupoDeViajeId,
                EventosIds = i.Eventos.Select(e => e.Id).ToList()
            });
        }
    }
}
