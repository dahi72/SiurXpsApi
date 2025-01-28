using DTOs;
using Humanizer;
using LogicaAplicacion.InterfacesCU.InterfacesCUTraslado;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUTraslado
{
    public class CUListadoTraslado   : IListadoTraslados
    {
        private readonly IRepositorioTraslado RepoTraslado;

        public CUListadoTraslado(IRepositorioTraslado repo)
        {
            RepoTraslado = repo;

        }

        public IEnumerable<TrasladoDTO> Listado()
        {
            return RepoTraslado.FindAll().Select(t => new TrasladoDTO
            {
                Id = t.Id,
                LugarDeEncuentro = t.LugarDeEncuentro,
                Horario = t.Horario,
                TipoDeTraslado = (int)t.TipoDeTraslado


            });
        }
    }
}
