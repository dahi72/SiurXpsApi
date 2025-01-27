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
    public class CUBuscarUsuarioPorPasaporte : IBuscarUsuarioPorPasaporte
    {
        private readonly IRepositorioUsuario RepoUsuario;


        public CUBuscarUsuarioPorPasaporte(IRepositorioUsuario repo)
        {
            RepoUsuario = repo;
        }


        public Usuario BuscarPorPasaporte(string pasaporte)
        {
            return RepoUsuario.ObtenerPorPasaporte(pasaporte);
        }
    }
}
