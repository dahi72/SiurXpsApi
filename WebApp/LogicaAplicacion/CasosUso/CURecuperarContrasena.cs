using LogicaAplicacion.InterfacesCU;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
{
    public class CURecuperarContrasena : IRecuperarContrasena
    {
        private readonly IRepositorioUsuario RepoUsuario;
        private readonly IServicioEmail servicioEmail;

        public CURecuperarContrasena(IRepositorioUsuario usuarioRepositorio, IServicioEmail emailService)
        {
            RepoUsuario = usuarioRepositorio;
            servicioEmail = emailService;
        }
        public void EnviarCorreo(string email)
        {
            Usuario usuario = RepoUsuario.ObtenerPorEmail(email);
            if (usuario == null)
            {
                throw new Exception("El usuario con ese email no existe.");
            }

            // Generar una nueva contraseña
            var nuevaContraseña = GenerarContraseña();

            // Actualizar la contraseña del usuario
            usuario.PasswordHash = HashPassword(nuevaContraseña); // Asegúrate de encriptar la contraseña
            RepoUsuario.Update(usuario);

            // Enviar el correo con la nueva contraseña
            var mensaje = $@"
    <html>
        <body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
            <h2 style='color: #007BFF;'>Hola {usuario.PrimerNombre} {usuario.PrimerApellido},</h2>
            <p>Hemos recibido tu solicitud para recuperar la contraseña de tu cuenta en <b>Siur Xperiencias</b>.</p>
            <p>Tu nueva contraseña temporal es:</p>
            <p style='font-size: 18px; font-weight: bold; color: #FF5733;'>{nuevaContraseña}</p>
            <p>
                Por motivos de seguridad, te recomendamos que accedas a tu cuenta y cambies esta contraseña 
                lo antes posible.
            </p>
            <p>Si no realizaste esta solicitud, por favor ignora este mensaje o contáctanos de inmediato.</p>
            <br>
            <p>¡Gracias por confiar en nosotros!</p>
            <p style='color: #007BFF;'>Equipo de Siur Xperiencias</p>
            <hr>
            <p style='font-size: 12px; color: #777;'>
                Este mensaje es generado automáticamente. Por favor, no respondas a este correo. 
                Si necesitas asistencia, comunícate con nuestro soporte a través de nuestro sitio web.
            </p>
        </body>
    </html>";

            servicioEmail.EnviarEmail(
                destinatario: usuario.Email,
                asunto: "Recuperación de Contraseña",
                mensaje: mensaje
            );
        }

        private string GenerarContraseña()
        {
            return Guid.NewGuid().ToString().Substring(0, 8); 
        }

        private string HashPassword(string contraseña)
        {           
            return BCrypt.Net.BCrypt.HashPassword(contraseña);
        }
    }
}