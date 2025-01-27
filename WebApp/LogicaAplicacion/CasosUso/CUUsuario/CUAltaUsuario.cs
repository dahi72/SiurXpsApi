using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUUsuario;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUAltaUsuario : IAltaUsuario
    {
        private readonly IRepositorioUsuario RepoUsuarios;

        public CUAltaUsuario(IRepositorioUsuario repo)
        {
            RepoUsuarios = repo;
        }

        public void Alta(AltaUsuarioDTO usu)
            
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(usu.Pasaporte);

            Usuario u = new Usuario()
            {
                PrimerNombre = usu.PrimerNombre,
                PrimerApellido = usu.PrimerApellido,
                PasswordHash = hashedPassword,
                Pasaporte = usu.Pasaporte.ToUpper(),
                Rol = "Coordinador",
                DebeCambiarContrasena = true 
            };

            if (!RepoUsuarios.Existe(u))
            {
                RepoUsuarios.Add(u);
                usu.SetId(u.Id);

            }
            else
            {
                throw new InvalidOperationException("Ya existe un usuario con ese pasaporte.");
            }
        }

     

    }
}
