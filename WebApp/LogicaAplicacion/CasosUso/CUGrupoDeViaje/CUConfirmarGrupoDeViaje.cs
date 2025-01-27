using LogicaAplicacion.InterfacesCU.InterfacesCUGrupoDeViaje;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LogicaAplicacion.CasosUso
    {
        public class CUConfirmarGrupoDeViaje :IConfirmarGrupoDeViaje
        {
            private readonly IRepositorioGrupoDeViaje RepoGrupoDeViaje;
            private readonly IRepositorioUsuario RepoUsuario;
            private readonly IServicioEmail ServicioEmail;

            public CUConfirmarGrupoDeViaje(
                IRepositorioGrupoDeViaje grupoDeViajeRepositorio,
                IRepositorioUsuario usuarioRepositorio,
                IServicioEmail servicioEmail)
            {
                RepoGrupoDeViaje = grupoDeViajeRepositorio;
                RepoUsuario = usuarioRepositorio;
                ServicioEmail = servicioEmail;
            }

            public void ConfirmarGrupo(int idGrupo)
            {
                // Obtener el grupo de viaje por su ID
                GrupoDeViaje grupo = RepoGrupoDeViaje.FindById(idGrupo);
                if (grupo == null)
                {
                    throw new Exception("El grupo de viaje no existe.");
                }

                if (grupo.EstadoGrupo != EstadoGrupoDeViaje.Proximo)
                {
                    throw new Exception("El grupo de viaje ya ha sido confirmado, cancelado o no está en estado pendiente.");
                }

                // Actualizar el estado del grupo
                grupo.EstadoGrupo = EstadoGrupoDeViaje.Confirmado;
                RepoGrupoDeViaje.Update(grupo);

                // Enviar correo a cada usuario del grupo
                foreach (Usuario usuario in grupo.Viajeros)
                {
                    EnviarCorreoUsuario(usuario, grupo.Nombre);
                }
            }

            private void EnviarCorreoUsuario(Usuario usuario, string nombreGrupo)
            {
                // Generar la contraseña (igual al pasaporte)
                string contraseña = usuario.Pasaporte;

                // Guardar la contraseña hasheada en el usuario
                usuario.PasswordHash = HashPassword(contraseña);
                RepoUsuario.Update(usuario);

                // Preparar el mensaje
                var mensaje = $@"
                <html>
                    <body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                        <h2 style='color: #007BFF;'>Hola {usuario.PrimerNombre} {usuario.PrimerApellido},</h2>
                        
                        <p>Te damos la bienvenida al grupo de viaje <b>{nombreGrupo}</b> en <b>Siur Xperiencias</b>.</p>
                        
                        <p>Estas son tus credenciales para acceder a nuestra plataforma:</p>
                        
                        <ul>
                            <li><b>Usuario:</b> {usuario.Pasaporte}</li>
                            <li><b>Contraseña:</b> {contraseña}</li>
                        </ul>
                        
                        <p>Puedes acceder a nuestra aplicación desde el siguiente enlace:</p>
                        <a href='https://app.siurxperiencias.com' style='color: #007BFF;'>https://app.siurxperiencias.com</a>
                        
                        <p>Te recomendamos cambiar tu contraseña la primera vez que accedas por seguridad.</p>
                        
                        <br>
                        
                        <p>¡Gracias por ser parte de nuestra experiencia!</p>
                        
                        <p style='color: #007BFF;'>Equipo de Siur Xperiencias</p>
                        
                        <hr style='border: none; border-top: 1px solid #ccc; margin: 20px 0;'>
                        
                        <p style='font-size: 12px; color: #777;'>
                            Este mensaje es generado automáticamente. Por favor, no respondas a este correo. 
                            Si necesitas asistencia, comunícate con nuestro soporte a través de nuestro sitio web.
                        </p>
                    </body>
                </html>";

                // Enviar el correo
                ServicioEmail.EnviarEmail(
                    destinatario: usuario.Email,
                    asunto: $"Bienvenido al grupo de viaje {nombreGrupo}",
                    mensaje: mensaje
                );
            }

            private string HashPassword(string contraseña)
            {
                return BCrypt.Net.BCrypt.HashPassword(contraseña);
            }
        }
    }



