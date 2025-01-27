using LogicaNegocio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeAccesoDatos.Repositorios
{
    public class RepositorioToken : IRepositorioToken
    {
        private readonly EContext Contexto;
        public RepositorioToken (EContext ctx)
        {
            Contexto = ctx;
        }
        public void Add(TokenRevocado obj)
        {
            Contexto.TokensRevocados.Add(obj);
            Contexto.SaveChanges();
        }

        public IEnumerable<TokenRevocado> FindAll()
        {
            throw new NotImplementedException();
        }

        public TokenRevocado FindById(int id)
        {
            throw new NotImplementedException();
        }

        public bool isRevoked(TokenRevocado token)
        {
            bool revoked = Contexto.TokensRevocados
                    .Any(t => t.Token == token.Token && t.Expiracion > DateTime.UtcNow);

            return revoked;

        }

        public void Remove(TokenRevocado obj)
        {
            throw new NotImplementedException();
        }

        public void Update(TokenRevocado obj)
        {
            throw new NotImplementedException();
        }
    }

}
