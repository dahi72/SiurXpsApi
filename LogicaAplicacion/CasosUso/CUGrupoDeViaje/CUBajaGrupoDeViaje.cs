using DTOs;
using ExcepcionesPropias;
using LogicaAplicacion.InterfacesCU.InterfacesCUGrupoDeViaje;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUGrupoDeViaje
{
    public class CUBajaGrupoDeViaje : IBajaGrupoDeViaje
    {
        private readonly IRepositorioGrupoDeViaje RepoViaje;

        public CUBajaGrupoDeViaje(IRepositorioGrupoDeViaje repo)
        {
            RepoViaje = repo;
        }
        public void Eliminar(GrupoDeViajeDTO dto)
        {
            GrupoDeViaje grupo = RepoViaje.FindById(dto.Id);
            if (grupo == null)
            {
                throw new GrupoDeViajeException("El grupo de viaje no existe.");
            }

           
           
            RepoViaje.Remove(grupo);
        }
    }
    
}
