using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUHotel;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;

namespace LogicaAplicacion.CasosUso.CUHotel
{
    public class CUBajaHotel : IBajaHotel
    {
        private readonly IRepositorioHotel RepoHoteles;

        public CUBajaHotel(IRepositorioHotel repo)
        {
            RepoHoteles = repo;
        }

       
            public void Eliminar(HotelDTO hotelDto)
            {
            Hotel aBorrar = RepoHoteles.FindById(hotelDto.Id);
                if (hotelDto == null)
                    throw new ArgumentNullException(nameof(hotelDto), "El hotel proporcionado es nulo.");

            // TODO: Verificar si tiene esta dentro de algun evento corriente y ahi no se puede eliminar
            RepoHoteles.Remove(aBorrar);
        }

        }
}
