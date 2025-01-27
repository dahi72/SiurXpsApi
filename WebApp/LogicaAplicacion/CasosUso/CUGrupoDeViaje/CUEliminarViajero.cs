using DTOs;
using LogicaAplicacion.InterfacesCU.InterfaesCUGrupoDeViaje;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUGrupoDeViaje
{
    public class CUEliminarViajero : IEliminarViajero
    {
        private readonly IRepositorioGrupoDeViaje RepoGrupo;
        private readonly IRepositorioUsuario RepoUsuario;

        public CUEliminarViajero(IRepositorioGrupoDeViaje repoGrupo, IRepositorioUsuario repoUsuario)
        {
            RepoGrupo = repoGrupo;
            RepoUsuario = repoUsuario;

        }

        public void EliminarViajero(int grupoId, int usuId)
        {
            var grupo = RepoGrupo.FindById(grupoId); 

            if (grupo == null)
            {
                throw new InvalidOperationException("No se encontró el grupo.");
            }

           
            if (grupo.EstadoGrupo == EstadoGrupoDeViaje.EnCurso)
            {
                throw new InvalidOperationException("No se puede eliminar un viajero de un grupo de un viaje en curso.");
            }

           
           var viajero = RepoUsuario.FindById(usuId);

            if (viajero == null)
            {
                throw new InvalidOperationException("El viajero no existe.");
            }
  
            RepoGrupo.EliminarViajeroDeGrupoDeViaje(grupoId,usuId);
       
        }
    }
}
