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
    public class CUModificarTraslado : IModificarTraslado
    {
        private readonly IRepositorioTraslado RepoTraslado;

        public CUModificarTraslado(IRepositorioTraslado repo)
        {
            RepoTraslado = repo;

        }
        public void Modificar(TrasladoDTO dto)
        {
            Traslado trasladoExistente = RepoTraslado.FindById(dto.Id);

            if (trasladoExistente == null)
            {
                throw new InvalidOperationException($"No se encontró la actividad con Id {dto.Id}.");
            }
            trasladoExistente.LugarDeEncuentro = dto.LugarDeEncuentro;
            trasladoExistente.Horario = dto.Horario;
            trasladoExistente.TipoDeTraslado = (TipoDeTraslado)dto.TipoDeTraslado;
          

           
                RepoTraslado.Update(trasladoExistente);
           
        }
    }
}
