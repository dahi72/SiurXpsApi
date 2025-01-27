using DTOs;
using Humanizer;
using LogicaAplicacion.InterfacesCU.InterfacesCUToken;
using LogicaNegocio;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;


namespace LogicaAplicacion.CasosUso.CUToken
{
    public class CUAgregarTokenABlacklist :IAgregarTokenABlacklist
    {
        private readonly IRepositorioToken RepoTokens;

        public CUAgregarTokenABlacklist(IRepositorioToken repo)
        {
            RepoTokens = repo;
        }

        public void Agregar(TokenDTO dto)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(dto.Token) as JwtSecurityToken;

            if (jsonToken == null)
            {
                // Si el token no es válido, salir.
                throw new InvalidOperationException("Token no válido.");
            }
            DateTime expiracionToken = jsonToken.ValidTo;

            TokenRevocado tkn = new TokenRevocado
            {
                Token = dto.Token,
                Expiracion= expiracionToken

            };

            if (!RepoTokens.isRevoked(tkn))
            {
                RepoTokens.Add(tkn);
             


            }
        }
    }
}
