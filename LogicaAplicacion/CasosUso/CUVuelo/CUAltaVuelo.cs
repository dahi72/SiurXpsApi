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
    public class CUAltaVuelo : IAltaVuelo
    {
        private readonly IRepositorioVuelo RepoVuelo;

        public CUAltaVuelo(IRepositorioVuelo repo)
        {
            RepoVuelo = repo;
        }
        public void Alta(VueloDTO dto)
        {
            Vuelo vuelo = new Vuelo
            {
                Nombre = dto.Nombre,
                Horario = dto.Horario

            };


            if (!RepoVuelo.Existe(vuelo))
            {
                RepoVuelo.Add(vuelo);
                dto.SetId(vuelo.Id);

            }
            else
            {
                throw new InvalidOperationException("El vuelo ya existe.");
            }


        }
    }
}
