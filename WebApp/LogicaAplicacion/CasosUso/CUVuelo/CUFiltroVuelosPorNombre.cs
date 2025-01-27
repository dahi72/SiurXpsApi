using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUVuelo;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUVuelo
{
    public class CUFiltroVuelosPorNombre :IListadoFiltroVuelo
    {
        private readonly IRepositorioVuelo RepoVuelo;

        public CUFiltroVuelosPorNombre(IRepositorioVuelo repo)
        {
            RepoVuelo = repo;
        }
       public IEnumerable<VueloDTO> VuelosPorNombre(string nombre)
        {
            IEnumerable<VueloDTO> dtosVuelos = new List<VueloDTO>();

            IEnumerable<Vuelo> vuelos = RepoVuelo.VuelosPorNombre(nombre);
            if (vuelos != null)
            {
                dtosVuelos = vuelos.Select(v => new VueloDTO
                {
                    Id = v.Id,
                    Nombre = v.Nombre,
                    Horario = v.Horario
                }).ToList();


            }

            return dtosVuelos;
        }


    }


}
