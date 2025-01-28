using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using LogicaAplicacion.InterfacesCU.InterfacesCUToken;
using Microsoft.EntityFrameworkCore;
using LogicaDeAccesoDatos;
using LogicaNegocio;

namespace WebApp
{

    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly EContext _dbContext;
        private readonly IEstaRevocado _cuEstaRevocado;

        public TokenValidationMiddleware(RequestDelegate next, EContext dbContext, IEstaRevocado cuEstaRevocado)
        {
            _next = next;
            _dbContext = dbContext;
            _cuEstaRevocado = cuEstaRevocado;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                // Verificamos si el token está revocado usando el caso de uso
                bool isRevoked = _cuEstaRevocado.IsRevocado(new TokenRevocado { Token = token });

                if (isRevoked)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token revocado");
                    return;
                }
            }

            await _next(context);
        }
    }

}
