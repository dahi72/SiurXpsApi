using LogicaAplicacion.InterfacesCU.InterfacesCUUsuario;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUBuscarUsuarioPorId : IBuscarUsusarioPorId
    {
        private readonly IRepositorioUsuario RepoUsuarios;


        public CUBuscarUsuarioPorId(IRepositorioUsuario repo)
        {
            RepoUsuarios = repo;
        }
        public UsuarioDTO Buscar(int id)
        {
            Usuario usuario = RepoUsuarios.FindById(id);

            // Validar que el usuario exista
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            // Mapear la entidad Usuario a UsuarioDTO
            UsuarioDTO usuarioDTO = new UsuarioDTO
            {
                Id = usuario.Id,
                PrimerNombre = usuario.PrimerNombre,
                SegundoNombre = usuario.SegundoNombre,
                PrimerApellido = usuario.PrimerApellido,
                SegundoApellido = usuario.SegundoApellido,
                Email = usuario.Email,
                Pasaporte = usuario.Pasaporte,
                PasswordHash = usuario.PasswordHash,
                Telefono = usuario.Telefono,
                FechaNac = usuario.FechaNac,
                Rol = usuario.Rol,
                DebeCambiarContrasena = usuario.DebeCambiarContrasena,
                PasaporteDocumentoRuta = usuario.PasaporteDocumentoRuta,
                VisaDocumentoRuta = usuario.VisaDocumentoRuta,
                VacunasDocumentoRuta = usuario.VacunasDocumentoRuta,
                SeguroDeViajeDocumentoRuta = usuario.SeguroDeViajeDocumentoRuta

            };

            return usuarioDTO;
        }
    }
}
