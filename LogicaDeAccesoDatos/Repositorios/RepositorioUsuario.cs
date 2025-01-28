using ExcepcionesPropias;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicaDeAccesoDatos.Repositorios
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private readonly EContext Contexto;
        public RepositorioUsuario(EContext ctx)
        {
            Contexto = ctx;
        }
        public void Add(Usuario obj)
        {
            if (obj != null)
            {
                obj.Validar();

                // encriptar constraseña
                obj.PasswordHash = BCrypt.Net.BCrypt.HashPassword(obj.PasswordHash);

                Contexto.Usuarios.Add(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se provee información del Usuario");
            }
        }

        public bool Existe(Usuario usu)
        {
            bool existeUsuario = Contexto.Usuarios
                         .Any(u => u.Pasaporte == usu.Pasaporte);
            if (existeUsuario)
            {
                throw new InvalidOperationException("Ya existe un usuario con ese pasaporte");
            }
            //ver si aca es mejor tirar excepciones de usuario

            return existeUsuario;


        }

        public IEnumerable<Usuario> FindAll()
        {
            return Contexto.Usuarios.ToList();
        }

        /*        public Usuario FindById(int id)
                {
                    return Contexto.Usuarios.Find(id);
                }*/
        /* public Usuario FindById(int id)
         {
             var usuario = Contexto.Usuarios
       .Where(u => u.Id == id)
       .Include(u => u.GruposComoViajero)
       .Include(u => u.GruposComoCoordinador)
       .Include(u => u.Actividades)

       .Select(u => new Usuario
       {
           Id = u.Id,
           PrimerNombre = u.PrimerNombre,
           SegundoNombre = u.SegundoNombre,
           PrimerApellido = u.PrimerApellido,
           SegundoApellido = u.SegundoApellido,
           Email = u.Email,
           Pasaporte = u.Pasaporte,
           PasswordHash = u.PasswordHash,
           Telefono = u.Telefono,
           FechaNac = u.FechaNac,
           Rol = u.Rol,
           Estado = u.Estado,
           PasaporteDocumentoRuta = u.PasaporteDocumentoRuta,
           VisaDocumentoRuta = u.VisaDocumentoRuta,
           VacunasDocumentoRuta = u.VacunasDocumentoRuta,
           SeguroDeViajeDocumentoRuta = u.SeguroDeViajeDocumentoRuta,
           DebeCambiarContrasena = u.DebeCambiarContrasena,
           GruposComoViajero = u.GruposComoViajero.ToList(),
           GruposComoCoordinador = u.GruposComoCoordinador.ToList(),
           Actividades = u.Actividades.Where(a => a.Opcional == true).ToList()
       })
       .FirstOrDefault();

             if (usuario == null)
             {
                 throw new UsuarioException($"No se encontró el usuario con ID {id}");
             }

             return usuario;
         }*/

        public Usuario FindById(int id)
        {
            var usuario = Contexto.Usuarios
                .Where(u => u.Id == id)
                .Include(u => u.GruposComoViajero)
                //.Include(u => u.GruposComoCoordinador)
                .Include(u => u.Actividades.Where(a => a.Opcional == true))
                .FirstOrDefault();

            if (usuario == null)
            {
                throw new UsuarioException($"No se encontró el usuario con ID {id}");
            }

            return usuario;
        }
        // TODO: ver si acá me lo trae sin tanto pamento solo con un var Usuario
        /*public Usuario FindById(int id)
{
    Usuario usuario = Contexto.Usuarios
        .Where(u => u.Id == id)
        .Include(u => u.GruposComoViajero)
        .Include(u => u.GruposComoCoordinador)
        .FirstOrDefault();

    if (usuario == null)
    {
        throw new UsuarioException($"No se encontró el usuario con ID {id}");
    }

    return usuario;
}*/
        public Usuario Log(string pasaporte, string pass)
        {
            Usuario usuarioABuscar = null;
            try
            {
                ObtenerPorPasaporte(pasaporte);
                if (usuarioABuscar != null)
                {

                    // Verificar la contrasena hasheada
                    if (BCrypt.Net.BCrypt.Verify(pass.Trim(), usuarioABuscar.PasswordHash))
                    {
                        return usuarioABuscar; // Contrasena correcta
                    }
                }
            }
            catch (UsuarioException ex)
            {
                throw new UsuarioException(ex.Message);
            }

            return null;
        }


        public void Remove(Usuario obj)
        {
            Usuario usuarioExistente = Contexto.Usuarios.Find(obj.Id);
            if (usuarioExistente == null)
            {
                throw new InvalidOperationException("El usuario no existe en la base de datos.");
            }

            Contexto.Usuarios.Remove(usuarioExistente);
            Contexto.SaveChanges();
        }

        public void Update(Usuario obj)
        {
            if (obj != null)
            {
                obj.Validar();
                Contexto.Usuarios.Update(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se puede modificar una usuario inexistente");
            }

        }


        public Usuario ObtenerPorPasaporte(string pasaporte)
        {
            Usuario usuario = Contexto.Usuarios.FirstOrDefault(u => u.Pasaporte == pasaporte);
            if (usuario == null)
            {
                return null;
            }

            return usuario;
        }

        public IEnumerable<Usuario> FindAllByIds(IEnumerable<int> usuariosIds)
        {
            return Contexto.Usuarios
                  .Where(u => usuariosIds.Contains(u.Id));

        }

        public bool ValidarContrasenaActual(string pasaporte, string contrasenaActual)
        {
            Usuario usuarioABuscar = ObtenerPorPasaporte(pasaporte);
            if (usuarioABuscar != null)
            {
                return BCrypt.Net.BCrypt.Verify(contrasenaActual.Trim(), usuarioABuscar.PasswordHash);
            }
            return false;
        }

        public bool ValidarContrasenaNueva(string nuevaContrasena)
        {

            if (string.IsNullOrEmpty(nuevaContrasena) || nuevaContrasena.Length < 8)
            {
                throw new UsuarioException("La contrasena debe tener al menos 8 caracteres.");
            }

            string patron = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$";
            if (!Regex.IsMatch(nuevaContrasena, patron))
            {
                return false;
            }

            return true;
        }

        public Usuario ObtenerPorEmail(string email)
        {
            Usuario usuario = Contexto.Usuarios.FirstOrDefault(u => u.Email == email);
            if (usuario == null)
            {
                return null;
            }

            return usuario;
        }
    }

}
