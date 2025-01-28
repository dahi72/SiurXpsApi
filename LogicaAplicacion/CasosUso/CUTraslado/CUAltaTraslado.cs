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
    public class CUAltaTraslado : IAltaTraslado
    {
        private readonly IRepositorioTraslado RepoTraslado;

        public CUAltaTraslado(IRepositorioTraslado repo)
        {
            RepoTraslado = repo;

        }
        public void Alta(TrasladoDTO dto)
        {
            Traslado t = new Traslado
            {
                LugarDeEncuentro = dto.LugarDeEncuentro,
                Horario = dto.Horario,
                TipoDeTraslado = (TipoDeTraslado)dto.TipoDeTraslado


            };

                       
                RepoTraslado.Add(t);
                dto.SetId(t.Id);

          
        }
    }
}
