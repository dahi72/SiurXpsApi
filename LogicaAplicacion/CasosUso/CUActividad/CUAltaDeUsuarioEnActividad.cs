using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUActividad;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUActividad
{
    public class CUAltaDeUsuarioEnActividad : IAltaDeUsuarioEnActividadOpcional
    {
        private readonly IRepositorioActividad RepoActividad;
      
        public CUAltaDeUsuarioEnActividad(IRepositorioActividad repo)
        {
            RepoActividad = repo;
          
        }
        public void InscripcionDeUsuario(InscripcionAActividadDTO dto)
        {
            RepoActividad.InscribirUsuarioEnActividad(dto.ActividadId, dto.UsuarioId);

            
        }
    }
    
}
