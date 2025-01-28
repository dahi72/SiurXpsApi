using DTOs;
using Humanizer;
using LogicaAplicacion.InterfacesCU.InterfacesCUAeropuerto;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUAeropuerto
{
    public class CUListadoFiltroAeropuertos : IListadoFiltradoAeropuerto
    {
        private readonly IRepositorioAeropuerto RepoAeropuerto;


        public CUListadoFiltroAeropuertos(IRepositorioAeropuerto repo)
        {

            RepoAeropuerto = repo;
        }
        public IEnumerable<AeropuertoDTO> AeropuertosPorNombre(string nombre)
        {
            IEnumerable<AeropuertoDTO> dtosAeros = new List<AeropuertoDTO>();

            IEnumerable<Aeropuerto> aeropuertos = RepoAeropuerto.AeropuertosPorNombre(nombre);
            if (aeropuertos != null)
            {
                dtosAeros = aeropuertos.Select(a => new AeropuertoDTO
                {
                    Id = a.Id,
                    Nombre = a.Nombre,
                    Direccion = a.Direccion,
                    PaisId = a.PaisId,
                    CiudadId = a.CiudadId,
                    PaginaWeb = a.PaginaWeb
                }).ToList();


            }

            return dtosAeros;
        }
    }
}
