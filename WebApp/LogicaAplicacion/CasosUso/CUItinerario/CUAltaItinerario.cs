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
    public class CUAltaItinerario : IAltaItinerario
    {
        private readonly IRepositorioItinerario RepoItinerario;
        private readonly IRepositorioGrupoDeViaje RepoGrupo;

        public CUAltaItinerario(IRepositorioItinerario repo, IRepositorioGrupoDeViaje repoGrupo)
        {
            RepoItinerario = repo;
            RepoGrupo = repoGrupo;
        }

        public void Alta(ItinerarioDTO dto)
        {
            GrupoDeViaje grupo = RepoGrupo.FindById(dto.GrupoDeViajeId);

            if (grupo.FechaInicio == null || grupo.FechaFin == null)
            {
                throw new Exception("El grupo de viaje no tiene fechas configuradas.");
            }
            Itinerario iti = new Itinerario()
            {

                FechaInicio = grupo.FechaInicio,
                FechaFin = grupo.FechaFin,
                GrupoDeViajeId = dto.GrupoDeViajeId
            };

            RepoItinerario.Add(iti);
            dto.Id = iti.Id;
        }
    }
}