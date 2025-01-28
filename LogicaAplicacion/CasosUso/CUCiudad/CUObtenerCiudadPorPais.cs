using LogicaAplicacion.InterfacesCU.InterfacesCUCiudad;
using DTOs;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUCiudad
{
    public class CUObtenerCiudadPorPais : IObtenerCiudadesPorPais
    {
        private readonly IRepositorioCiudad RepoCiudad;

        public CUObtenerCiudadPorPais(IRepositorioCiudad repo)
        {
            RepoCiudad = repo;
        }
        public IEnumerable<CiudadDTO> ObtenerPais(string codigoISO)
        {
            // Intentar obtener las ciudades del repositorio
            var ciudades = RepoCiudad.ObtenerCiudadPorPais(codigoISO).Select(c => new CiudadDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                PaisId = c.PaisId,
                PaisCodigoIso=c.PaisCodigoIso
            }).ToList();

            return ciudades;
        }

    }
}
