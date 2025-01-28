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
    public class CUModificarVuelo : IModificarVuelo
    {
        private readonly IRepositorioVuelo RepoVuelo;

        public CUModificarVuelo(IRepositorioVuelo repo)
        {
            RepoVuelo = repo;
        }
        public void Modificar(VueloDTO vueloDto)
        {
            Vuelo vueloExistente = RepoVuelo.FindById(vueloDto.Id);

            if (vueloExistente == null)
            {
                throw new InvalidOperationException($"No se encontró el hotel con Id {vueloDto.Id}.");
            }
            vueloExistente.Nombre = vueloDto.Nombre;
            vueloExistente.Horario = vueloDto.Horario;



            if (!RepoVuelo.Existe(vueloExistente))
            {
                RepoVuelo.Update(vueloExistente);
            }
            else
            {
                throw new InvalidOperationException("El vuelo ya existe");
            }


        }
    }
}
