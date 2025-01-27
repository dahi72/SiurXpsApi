using DTOs;
using ExcepcionesPropias;
using LogicaAplicacion.InterfacesCU.InterfacesCUUsuario;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LogicaNegocio.Dominio;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUCambioConstrasena : ICambioContrasena
    {
        private readonly IRepositorioUsuario RepoUsuarios;


        public CUCambioConstrasena(IRepositorioUsuario repo)
        {
            RepoUsuarios = repo;
        }

        public bool CambiarContrasena(CambiarContrasenaDTO cambiarContrasenaDto)
        {
            if (cambiarContrasenaDto.NuevaPassword != cambiarContrasenaDto.ConfirmPassword)
            {
                throw new UsuarioException("Las contrasenas no coinciden.");
            }


            Usuario usuario = RepoUsuarios.ObtenerPorPasaporte(cambiarContrasenaDto.Pasaporte);

            if (usuario == null)
            {
                throw new UsuarioException("Usuario no encontrado.");
            }
            if (ValidarContrasenaActual(cambiarContrasenaDto.Pasaporte, cambiarContrasenaDto.ContrasenaActual))
            {
                throw new UsuarioException("La contrasena actual es incorrecta.");
            }

            if (!RepoUsuarios.ValidarContrasenaNueva(cambiarContrasenaDto.NuevaPassword))
            {
                throw new UsuarioException("La nueva contraseña debe contener al menos un número, una letra mayúscula, una minúscula y un carácter especial o un guion bajo.");
            }

            usuario.EstablecerContrasena(cambiarContrasenaDto.NuevaPassword);
            usuario.ConfirmarCambioContrasena();
            RepoUsuarios.Update(usuario);
            return true;
        }

        public bool ValidarContrasenaActual(string pasaporte, string contrasenaActual)
        {
            Usuario usuarioABuscar = RepoUsuarios.ObtenerPorPasaporte(pasaporte);
            if (usuarioABuscar != null)
            {
                return BCrypt.Net.BCrypt.Verify(contrasenaActual.Trim(), usuarioABuscar.PasswordHash);
            }
            return false;
        }




    }
}

