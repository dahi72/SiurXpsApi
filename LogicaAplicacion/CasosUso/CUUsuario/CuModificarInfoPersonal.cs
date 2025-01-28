using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUUsuario;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CuModificarInfoPersonal : IModificarInfoPersonal
    {
        private readonly IRepositorioUsuario RepoUsuarios;
         public CuModificarInfoPersonal(IRepositorioUsuario repo)
        {
            RepoUsuarios = repo;
        }

      public void Modificar(int id, ModificarUsuarioDTO usu)
{
    var usuarioExistente = RepoUsuarios.FindById(id);
    if (usuarioExistente == null)
        throw new InvalidOperationException("El usuario no existe en la base de datos.");

    usuarioExistente.PrimerNombre = usu.PrimerNombre;
    usuarioExistente.SegundoNombre = usu.SegundoNombre;
    usuarioExistente.PrimerApellido = usu.PrimerApellido;
    usuarioExistente.SegundoApellido = usu.SegundoApellido;
    usuarioExistente.Pasaporte = usu.Pasaporte;
    usuarioExistente.Email = usu.Email;
    usuarioExistente.Telefono = usu.Telefono;
    usuarioExistente.FechaNac = usu.FechaNac;

    // Verificar si se proporciona un nuevo documento y solo actualizar si es necesario
    if (usu.PasaporteDocumento != null)
    {
        usuarioExistente.PasaporteDocumentoRuta = GuardarArchivo(usu.PasaporteDocumento, "pasaporte", id);
    }
    // Mantener la ruta existente si no se proporciona un nuevo documento
    else if (string.IsNullOrEmpty(usuarioExistente.PasaporteDocumentoRuta))
    {
        usuarioExistente.PasaporteDocumentoRuta = usuarioExistente.PasaporteDocumentoRuta;
    }

    if (usu.VisaDocumento != null)
    {
        usuarioExistente.VisaDocumentoRuta = GuardarArchivo(usu.VisaDocumento, "visado", id);
    }
    else if (string.IsNullOrEmpty(usuarioExistente.VisaDocumentoRuta))
    {
        usuarioExistente.VisaDocumentoRuta = usuarioExistente.VisaDocumentoRuta;
    }

    if (usu.VacunasDocumento != null)
    {
        usuarioExistente.VacunasDocumentoRuta = GuardarArchivo(usu.VacunasDocumento, "vacunas", id);
    }
    else if (string.IsNullOrEmpty(usuarioExistente.VacunasDocumentoRuta))
    {
        usuarioExistente.VacunasDocumentoRuta = usuarioExistente.VacunasDocumentoRuta;
    }

    if (usu.SeguroDeViajeDocumento != null)
    {
        usuarioExistente.SeguroDeViajeDocumentoRuta = GuardarArchivo(usu.SeguroDeViajeDocumento, "seguro", id);
    }
    else if (string.IsNullOrEmpty(usuarioExistente.SeguroDeViajeDocumentoRuta))
    {
        usuarioExistente.SeguroDeViajeDocumentoRuta = usuarioExistente.SeguroDeViajeDocumentoRuta;
    }

    RepoUsuarios.Update(usuarioExistente);
}

        private string GuardarArchivo(IFormFile? archivo, string tipoDocumento, int id)
        {
            if (archivo == null) return null;

            var rutaBase = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            var extension = Path.GetExtension(archivo.FileName);
            var nombreArchivo = $"{tipoDocumento}_{id}{extension}";
            var rutaArchivo = Path.Combine(rutaBase, nombreArchivo);
            Console.WriteLine($"Guardando archivo en: {rutaArchivo}");
            // Si el archivo ya existe, renombramos para evitar sobrescribirlo
            if (File.Exists(rutaArchivo))
            {
                var nuevoNombre = $"{tipoDocumento}_{id}_{Guid.NewGuid()}{extension}";
                rutaArchivo = Path.Combine(rutaBase, nuevoNombre);

            }

            // Crear el directorio si no existe
            Directory.CreateDirectory(rutaBase);

            using (var stream = new FileStream(rutaArchivo, FileMode.Create))
            {
                archivo.CopyTo(stream);
            }

            return Path.Combine("uploads", Path.GetFileName(rutaArchivo));
        }
    }
}