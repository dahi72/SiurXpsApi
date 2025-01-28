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
    public class CUAltaHotel : IAltaHotel
    {
        private readonly IRepositorioHotel RepoHoteles;
        private readonly IRepositorioPais RepoPais;
        private readonly IRepositorioCiudad RepoCiudad;
        public CUAltaHotel(IRepositorioHotel repo, IRepositorioPais repoPais, IRepositorioCiudad repoCiudad)
        {
            RepoHoteles = repo;
            RepoPais = repoPais;
            RepoCiudad = repoCiudad;
        }
        public void Alta(HotelDTO dto)
        {
            Pais pais = RepoPais.FindById(dto.PaisId);
            if (pais == null)
            {
                throw new InvalidOperationException("El país especificado no existe.");
            }

            Ciudad ciudad = RepoCiudad.FindById(dto.CiudadId);
            if (ciudad == null)
            {
                throw new InvalidOperationException("La ciudad especificada no existe.");
            }

            // Validar que la ciudad pertenece al país
            if (ciudad.PaisCodigoIso != pais.CodigoIso)
            {
                throw new InvalidOperationException($"La ciudad '{ciudad.Nombre}' no pertenece al país '{pais.Nombre}'.");
            }
  

            Hotel hotel = new Hotel
            {
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                PaisId = dto.PaisId,
                CiudadId = dto.CiudadId,
                CheckIn = dto.CheckIn,
                CheckOut = dto.CheckOut,
                PaginaWeb = dto.PaginaWeb
            };

            if (!RepoHoteles.Existe(hotel))
            {
                RepoHoteles.Add(hotel);
                dto.SetId(hotel.Id);

            }


        }
    }
}

