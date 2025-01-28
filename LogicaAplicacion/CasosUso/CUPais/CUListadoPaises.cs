using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUPais;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUPais
{
    public class CUListadoPaises : IListadoPaises
    {
        private readonly IRepositorioPais RepoPaises;

        public CUListadoPaises(IRepositorioPais repo)
        {
            RepoPaises = repo;
        }

        public IEnumerable<PaisDTO> Listado()
        {
            return RepoPaises.FindAll().Select(p => new PaisDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                CodigoIso=p.CodigoIso
               

            });
        }

    }

}