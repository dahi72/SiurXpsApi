using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUVuelo;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUVuelo
{
    public class CUBajaVuelo : IBajaVuelo
    {
        private readonly IRepositorioVuelo RepoVuelo;

        public CUBajaVuelo(IRepositorioVuelo repo)
        {
            RepoVuelo = repo;
        }
        public void Eliminar(VueloDTO vueloDto)
        {
            {
                if (vueloDto == null)
                    throw new ArgumentNullException(nameof(vueloDto), "El vuelo proporcionado es nulo.");

                // TODO: Verificar si tiene itinerarios asociados cuando se implementen los itinerarios
                RepoVuelo.Remove(new Vuelo { Id = vueloDto.Id });
            }
        }
    }
}
