using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUUsuario;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUActualizarEstadoCoordinador : IActualizarEstadoCoordinador
    {
        private readonly IRepositorioUsuario RepoUsuarios;

        public CUActualizarEstadoCoordinador(IRepositorioUsuario repo)
        {
            RepoUsuarios = repo;
        }
        public void ActualizarEstado(int id, bool estado)
        {
            {
                var usuario = RepoUsuarios.FindById(id);

                if (usuario == null)
                {
                    throw new InvalidOperationException("El coordinador no existe en la base de datos.");
                }

                if (usuario.Rol != "Coordinador")
                {
                    throw new UnauthorizedAccessException("El usuario no es un coordinador.");
                }

                usuario.Estado = estado;

                RepoUsuarios.Update(usuario);
            }
        }
    
    }
}
