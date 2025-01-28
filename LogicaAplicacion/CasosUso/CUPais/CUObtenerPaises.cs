using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUPais;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUPais
{
    public class CUObtenerPaises :IObtenerPaises
    {
        private readonly IRepositorioPais RepoPais;

        public CUObtenerPaises(IRepositorioPais repo)
        {
            RepoPais = repo;
        }


        public IEnumerable<PaisDTO> ObtenerPorCodigoIso(string codigoIso)
        {
            var paises = RepoPais.ObtenerPorCodigoIso(codigoIso);
            return paises.Select(p => new PaisDTO { Id = p.Id, Nombre = p.Nombre, CodigoIso = p.CodigoIso });
        }
    }
}
