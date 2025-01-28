using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUToken;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUToken
{
    public class CUEstaRevocado : IEstaRevocado
    {
        private readonly IRepositorioToken RepoTokens;

        public CUEstaRevocado(IRepositorioToken repo)
        {
            RepoTokens = repo;
        }
        public bool IsRevocado(TokenRevocado obj)
        {
            return RepoTokens.isRevoked(obj);
        }

        
    }
}
