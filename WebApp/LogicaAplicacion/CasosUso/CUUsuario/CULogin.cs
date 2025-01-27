using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUUsuario;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CULogin : ILogin
    {
        private readonly IRepositorioUsuario RepoUsuarios;


        public CULogin(IRepositorioUsuario repo)
        {
            RepoUsuarios = repo;
        }

        public UsuarioDTO Log(string pasaporte, string pass)
        {
            UsuarioDTO dto = null;
            Usuario usu = RepoUsuarios.ObtenerPorPasaporte(pasaporte);

            // Si el usuario existe
            if (usu != null)
            {
                // Si es la primera vez que inicia sesión, usa el pasaporte como contrasena
                if (usu.DebeCambiarContrasena && pass == pasaporte)
                {
                    dto = new UsuarioDTO()
                    {
                        Id = usu.Id,
                        Pasaporte = usu.Pasaporte,
                        PasswordHash = usu.PasswordHash,
                        Rol = usu.Rol,
                        DebeCambiarContrasena = usu.DebeCambiarContrasena // Indicar que debe cambiar su contrasena
                    };
                    return dto; // Permitir el login con el pasaporte
                }

                // Si no es la primera vez, verificar la contrasena
                if (usu.VerificarContrasena(pass))
                {
                    dto = new UsuarioDTO()
                    {
                        Id = usu.Id,
                        Pasaporte = usu.Pasaporte,
                        PasswordHash = usu.PasswordHash,
                        Rol = usu.Rol,
                        DebeCambiarContrasena = usu.DebeCambiarContrasena
                    };
                }
            }

            return dto; // Retorna null si no se pudo autenticar
        }
    }
}
