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
    public class CUAgregarViajeroAGrupo : IAgregarViajeroAGrupo
    {
        private readonly IRepositorioGrupoDeViaje RepoGrupo;
        private readonly IRepositorioUsuario RepoUsuario;
    
        public CUAgregarViajeroAGrupo(IRepositorioGrupoDeViaje repoGrupo, IRepositorioUsuario repoUsuario)
        {
            RepoGrupo = repoGrupo;
            RepoUsuario = repoUsuario;
         
        }
        public void AgregarViajero(int grupoId, AltaUsuarioDTO usu)
        {
            GrupoDeViaje grupo = RepoGrupo.FindById(grupoId);
            if (grupo == null)
            {
                throw new InvalidOperationException("Grupo de viaje no encontrado.");
            }

            // Buscar si el usuario ya existe por pasaporte
            Usuario viajeroExistente = RepoUsuario.ObtenerPorPasaporte(usu.Pasaporte);
            if (viajeroExistente == null)
            {

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(usu.Pasaporte);

                Usuario u = new Usuario()
                {
                    PrimerNombre = usu.PrimerNombre,
                    PrimerApellido = usu.PrimerApellido,
                    PasswordHash = hashedPassword,
                    Pasaporte = usu.Pasaporte,
                    Email = usu.Email,
                    Telefono = usu.Telefono,
                    Rol = "Viajero",
                    DebeCambiarContrasena = true 

                };
                if (!RepoUsuario.Existe(u))
                {
                    RepoUsuario.Add(u);
                    usu.SetId(u.Id);
                    grupo.Viajeros.Add(u);

                }
                else
                {
                    throw new InvalidOperationException("Ya existe un usuario con ese pasaporte.");
                }
            }
          
            RepoGrupo.Update(grupo);
        }
        
    }
}
