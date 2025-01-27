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
    public class CUBuscarHotelPorId : IBuscarHotelPorId
    {
        private readonly IRepositorioHotel RepoHoteles;

        public CUBuscarHotelPorId(IRepositorioHotel repo)
        {
            RepoHoteles = repo;
        }


        public HotelDTO Buscar(int id)
        {
            HotelDTO hotelDto = null;

            Hotel hotel = RepoHoteles.FindById(id);
            if (hotel != null)
            {
                hotelDto = new HotelDTO()
                {
                    Id = hotel.Id,
                    Nombre = hotel.Nombre,
                    Direccion = hotel.Direccion,
                    PaisId = hotel.PaisId,
                    CiudadId = hotel.CiudadId,
                    CheckIn = hotel.CheckIn,
                    CheckOut = hotel.CheckOut,
                    PaginaWeb = hotel.PaginaWeb
                };
            }
            return hotelDto;
        }
    }
}
