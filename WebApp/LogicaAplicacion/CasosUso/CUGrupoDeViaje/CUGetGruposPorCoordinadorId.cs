using DTOs;
using LogicaAplicacion.InterfacesCU.InterfaesCUGrupoDeViaje;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUGrupoDeViaje
{
    public class CUGetGruposPorCoordinadorId : IGetGruposPorCoordinadorId
    {
        private readonly IRepositorioGrupoDeViaje RepoViajes;

        public CUGetGruposPorCoordinadorId (IRepositorioGrupoDeViaje repoViajes) 
        {

            RepoViajes = repoViajes;
        }
            
        public IEnumerable<GrupoDeViajeDTO> GetGruposDeViajeCoordinadorId(int coordinadorId)
        {
            var grupos = RepoViajes.GetGruposDeViajeDeCoordinador(coordinadorId);

            var gruposDTO = grupos.Select(grupo => new GrupoDeViajeDTO(grupo.Id)
            {
               
                Nombre = grupo.Nombre,
                PaisesDestinoIds = grupo.PaisesDestino.Select(p => p.Id).ToList(),
               // PaisesNombre = grupo.PaisesDestino.Select(p => p.Nombre).ToList(),
                CiudadesDestinoIds = grupo.CiudadesDestino.Select(c => c.Id).ToList(),
                //CiudadesNombre = grupo.CiudadesDestino.Select(c => c.Nombre).ToList(),
                FechaInicio = grupo.FechaInicio,
                FechaFin = grupo.FechaFin,
                CoordinadorId = grupo.CoordinadorId ?? 0,
                ViajerosIds = grupo.Viajeros?.Select(v => v.Id).ToList()
            });
           
            return gruposDTO;
        }
    }
}
