using DTOs;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class ManejadorJWT
{
    public static string GenerarToken(UsuarioDTO usuario)
    {
        try
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] clave = Encoding.ASCII.GetBytes("ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE="); // Clave secreta

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Role, usuario.Rol),
                new Claim("pasaporte", usuario.Pasaporte),
                new Claim("id", usuario.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                Issuer = "TuAplicacion",
                Audience = "TuAplicacion",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(clave), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        catch (Exception ex)
        {
            // Registra el error de la excepción
            Console.WriteLine($"Error al generar el token: {ex.Message}");
            throw;  // Re-lanzar la excepción o retornar un mensaje adecuado
        }
    }


}
