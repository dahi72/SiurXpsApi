using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUCiudad;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUCiudad
{
    public class CUListadoCiudades : IListadoCiudades
    {
        private readonly IRepositorioCiudad RepoCiudad;

        public CUListadoCiudades(IRepositorioCiudad repo)
        {
            RepoCiudad = repo;
        }

        public IEnumerable<CiudadDTO> Listado()
        {
            return RepoCiudad.FindAll().Select(c=> new CiudadDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                PaisId = c.PaisId,
                PaisCodigoIso = c.PaisCodigoIso
              

            });
        }

      
    }

}
