using DTOs;
using Humanizer;
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
    public class CUModificarHotel : IModificarHotel
    {
        private readonly IRepositorioHotel RepoHoteles;
        private readonly IRepositorioPais RepoPais;
        private readonly IRepositorioCiudad RepoCiudad;
        public CUModificarHotel(IRepositorioHotel repo, IRepositorioPais repoPais, IRepositorioCiudad repoCiudad)
        {
            RepoHoteles = repo;
            RepoPais = repoPais;
            RepoCiudad = repoCiudad;
        }

        public void Modificar(HotelDTO hotelDto)
        {
            Hotel hotelExistente = RepoHoteles.FindById(hotelDto.Id);

            if (hotelExistente == null)
            {
                throw new InvalidOperationException($"No se encontró el hotel con Id {hotelDto.Id}.");
            }
            Pais pais = RepoPais.FindById(hotelDto.PaisId);
            if (pais == null)
            {
                throw new InvalidOperationException("El país especificado no existe.");
            }

            Ciudad ciudad = RepoCiudad.FindById(hotelDto.CiudadId);
            if (ciudad == null)
            {
                throw new InvalidOperationException("La ciudad especificada no existe.");
            }

            // Validar que la ciudad pertenece al país
            if (ciudad.PaisCodigoIso != pais.CodigoIso)
            {
                throw new InvalidOperationException($"La ciudad '{ciudad.Nombre}' no pertenece al país '{pais.Nombre}'.");
            }
            hotelExistente.Nombre = hotelDto.Nombre;
            hotelExistente.Direccion = hotelDto.Direccion;
            hotelExistente.PaisId = hotelDto.PaisId;
            hotelExistente.CiudadId = hotelDto.CiudadId;
            hotelExistente.CheckIn = hotelDto.CheckIn;
            hotelExistente.CheckOut = hotelDto.CheckOut;
            hotelExistente.PaginaWeb = hotelDto.PaginaWeb;
        
                   

            if (!RepoHoteles.Existe(hotelExistente))
            { 
                RepoHoteles.Update(hotelExistente); 
            }
            else
            {
                throw new InvalidOperationException("El hotel ya existe");
            }
               

         
        }
    }
}
