using DTOs;
using LogicaAplicacion.InterfacesCU.InterfaesCUGrupoDeViaje;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUGrupoDeViaje
{
    public  class CUBuscarGrupoPorId : IBuscarGrupoPorId
    {
        private readonly IRepositorioGrupoDeViaje RepoViaje;

        public CUBuscarGrupoPorId(IRepositorioGrupoDeViaje repo)
        {
            RepoViaje = repo;
        }
        public GrupoDeViajeDTO Buscar(int id)
        {
            GrupoDeViaje grupo = RepoViaje.FindById(id);
            if (grupo == null)
                return null;

            var dto = new GrupoDeViajeDTO(grupo.Id)
            {
                Nombre = grupo.Nombre,
                PaisesDestinoIds = grupo.PaisesDestino.Select(d => d.Id).ToList(),
                CiudadesDestinoIds = grupo.CiudadesDestino.Select(d => d.Id).ToList(),
                FechaInicio = grupo.FechaInicio,
                FechaFin = grupo.FechaFin,
                CoordinadorId = grupo.Coordinador.Id,
                ViajerosIds= grupo.Viajeros.Select(d=>d.Id).ToList(),
            };

         

            return dto;
        }
    }
}

    


