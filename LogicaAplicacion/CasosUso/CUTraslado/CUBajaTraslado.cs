using DTOs;
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
    public class CUBajaTraslado : IBajaTraslado
    {
        private readonly IRepositorioTraslado RepoTraslado;

        public CUBajaTraslado(IRepositorioTraslado repo)
        {
            RepoTraslado = repo;

        }
        public void Eliminar(TrasladoDTO dto)
        {
            RepoTraslado.Remove(new Traslado() { Id = dto.Id });
        }
    }
}
