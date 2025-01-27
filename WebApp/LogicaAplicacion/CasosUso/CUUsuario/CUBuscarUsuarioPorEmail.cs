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
    public class CUBuscarUsuarioPorEmail : IBuscarUsuarioPorEmail
    {
        private readonly IRepositorioUsuario RepoUsuarios;


        public CUBuscarUsuarioPorEmail(IRepositorioUsuario repo)
        {
            RepoUsuarios = repo;
        }
        public Usuario BuscarPorEmail(string email)
        {
            return RepoUsuarios.ObtenerPorEmail(email);
        }
    }
}
