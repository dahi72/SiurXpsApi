using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUAerolinea;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUAerolinea
{
    public class CUListadoFiltroAerolinea : IListadoFIltradoAerolinea
    {
        private readonly IRepositorioAerolinea RepoAerolinea;


        public CUListadoFiltroAerolinea(IRepositorioAerolinea repo)
        {

            RepoAerolinea = repo;
        }
        public IEnumerable<AerolineaDTO> AerolineasPorNombre(string nombre)
        {
            IEnumerable<AerolineaDTO> dtosAeros = new List<AerolineaDTO>();

            IEnumerable<Aerolinea> aerolineas = RepoAerolinea.AerolineasPorNombre(nombre);
            if (aerolineas != null)
            {
                dtosAeros = aerolineas.Select(v => new AerolineaDTO
                {
                    Id = v.Id,
                    Nombre = v.Nombre,
                    PaginaWeb = v.PaginaWeb
                }).ToList();


            }

            return dtosAeros;
        }
    }
}
