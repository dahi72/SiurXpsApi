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
    public class CUBuscarTraslado : IBuscarTraslado
    {
        private readonly IRepositorioTraslado RepoTraslado;

        public CUBuscarTraslado(IRepositorioTraslado repo)
        {
            RepoTraslado = repo;

        }
        public TrasladoDTO Buscar(int id)
        {
            TrasladoDTO dtoTraslado = null;

            Traslado traslado = RepoTraslado.FindById(id);
            if (traslado != null)
            {

                dtoTraslado = new TrasladoDTO()
                {
                    Id = traslado.Id,
                    LugarDeEncuentro=traslado.LugarDeEncuentro,
                    Horario = traslado.Horario,
                    TipoDeTraslado = (int)traslado.TipoDeTraslado

                };

            }
            return dtoTraslado;
        }
    }
}
