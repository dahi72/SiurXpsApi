using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUHotel;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUHotel
{
    public class CUFiltroHotelesPorPaisYCiudad : IFiltroHotelesPorPaisYCiudad
    {
        private readonly IRepositorioHotel RepoHoteles;

        public CUFiltroHotelesPorPaisYCiudad(IRepositorioHotel repo)
        {
            RepoHoteles = repo;
        }
        public IEnumerable<HotelDTO> HotelesPorPaisyCiudad(string nombre, string codigoIso, string ciudad)
        {
            IEnumerable<HotelDTO> dtosHotel = new List<HotelDTO>();

            IEnumerable<Hotel> hoteles = RepoHoteles.HotelesPorPaisyCiudad(nombre, codigoIso,ciudad);
            if (hoteles != null)
            {
                dtosHotel = hoteles.Select(h => new HotelDTO
                    {
                    Id = h.Id,
                    Nombre = h.Nombre,
                    Direccion = h.Direccion,
                    PaisId = h.PaisId,
                    CiudadId = h.CiudadId,
                    CheckIn = h.CheckIn,
                    CheckOut = h.CheckOut,
                    PaginaWeb = h.PaginaWeb
                }).ToList();

               
            }

            return dtosHotel;
        }
            
        
    }
}
