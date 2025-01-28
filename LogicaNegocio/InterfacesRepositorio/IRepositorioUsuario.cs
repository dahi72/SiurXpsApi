using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioUsuario : IRepositorio<Usuario>
    {
        Usuario Log(string pasaporte, string pass);
        bool Existe(Usuario usu);
        Usuario ObtenerPorPasaporte(string pasaporte);
        IEnumerable<Usuario> FindAllByIds(IEnumerable<int> usuariosIds);
        bool ValidarContrasenaActual(string pasaporte, string contrasenaActual);
        bool ValidarContrasenaNueva(string nuevaContrasena);
        Usuario ObtenerPorEmail(string email);
    }
}
