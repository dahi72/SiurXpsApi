using DTOs;
using ExcepcionesPropias;
using Humanizer;
using LogicaAplicacion.CasosUso;
using LogicaAplicacion.CasosUso.CUUsuario;
using LogicaAplicacion.InterfacesCU;
using LogicaAplicacion.InterfacesCU.InterfacesCUToken;
using LogicaAplicacion.InterfacesCU.InterfacesCUUsuario;
using LogicaNegocio.Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace WebApp.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogin CULogin;
        private readonly IAltaUsuario CUAlta;
        private readonly ICambioContrasena CUCambio;
        private readonly IBuscarUsusarioPorId CUBuscar;
        private readonly IModificarInfoPersonal CUModificar;
        private readonly IBuscarUsuarioPorPasaporte CUBuscarUsuPorPasaporte;
        private readonly IActualizarEstadoCoordinador CUActualizarEstadoCoordinador;
        private readonly IAgregarTokenABlacklist CUAgregarTokenABlacklist;
        private readonly IRecuperarContrasena CURecuperarContrasena;

 

        public UsuarioController(ILogin loginUsuario, IAltaUsuario cUAlta, ICambioContrasena cUCambio, IBuscarUsusarioPorId cUBuscar, IModificarInfoPersonal cUModificar, IBuscarUsuarioPorPasaporte cUBuscarUsuPorPasaporte, IActualizarEstadoCoordinador actualizarEstadoCoordinador, IAgregarTokenABlacklist agregarTokenABlacklist, IRecuperarContrasena recuperarContrasena)
        {
            CULogin = loginUsuario;
            CUAlta = cUAlta;
            CUCambio = cUCambio;
            CUBuscar = cUBuscar;
            CUModificar = cUModificar;
            CUBuscarUsuPorPasaporte = cUBuscarUsuPorPasaporte;
            CUActualizarEstadoCoordinador = actualizarEstadoCoordinador;
            CUAgregarTokenABlacklist = agregarTokenABlacklist;
            CURecuperarContrasena = recuperarContrasena;
      
        }



        /// <summary>
        /// Para loguearse y obtener Token
        /// </summary>
        /// <param name="usuario">Datos necesarios para el Login</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO usuario)
        {
            try
            {
                // Verificar si el usuario existe antes de intentar el login
              var usuarioExistente = CUBuscarUsuPorPasaporte.BuscarPorPasaporte(usuario.Pasaporte);
                if (usuarioExistente == null)
                {
                    return NotFound(new { message = "Usuario no encontrado" });
                }

                UsuarioDTO logueado = CULogin.Log(usuario.Pasaporte, usuario.Password);

                if (logueado == null)
                {
                    return Unauthorized(new { message = "Usuario y/o contrasena incorrecto" });
                }

                string token = ManejadorJWT.GenerarToken(logueado);
                HttpContext.Session.SetString("token", token);

                return Ok(new
                {
                    Rol = logueado.Rol,
                    token = token,
                    id = logueado.Id,
                    Pasaporte = logueado.Pasaporte,
                    DebeCambiarContrasena = logueado.DebeCambiarContrasena
                });
            }
            catch (UsuarioException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        /// <summary>
        /// Para la creacion de un nuevo usuario.
        /// </summary>
        /// <param name="usu">Datos necesarios para la creacion</param>
        /// <returns>200 Usuario Creado</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        [HttpPost("altaUsuario")]
        public IActionResult Post(AltaUsuarioDTO usu)
        {
            if (usu == null) return BadRequest("No se envió información para la creacion del usuario");
            try
            {

                CUAlta.Alta(usu);
                //return Ok(new { codigo = 200, mensaje = "Usuario creado exitosamente." });
                return CreatedAtRoute("FindById", new { id = usu.Id }, usu);
            }
            catch (UsuarioException ex)
            {
                return BadRequest( ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {message = "Ocurrió un error, no se pudo realizar la creacion del usuario." });
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "FindByIdUsuario")]
        public IActionResult FindById(int id)
        {
            try
            {
                var usuario = CUBuscar.Buscar(id);
                if (usuario == null)
                {
                    return NotFound(new { message = "Usuario no encontrado" });
                }
                // tiene que haber unauthorized
                return Ok(usuario);
            }
            catch (UsuarioException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno, no se pudo encontrar al usuario." });
            }
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("cambiarContrasena")]
       
        public IActionResult CambiarContrasena([FromBody] CambiarContrasenaDTO cambiarContrasenaDto)
        {
            var usuarioExistente = CUBuscarUsuPorPasaporte.BuscarPorPasaporte(cambiarContrasenaDto.Pasaporte);
            if (usuarioExistente == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }
            try
            {
                
                if (cambiarContrasenaDto.NuevaPassword != cambiarContrasenaDto.ConfirmPassword)
                {
                    return BadRequest(new { message = "Las contrasenas no coinciden." });
                }

                // Lógica para cambiar la contrasena
                CUCambio.CambiarContrasena(cambiarContrasenaDto);
                return Ok(new { codigo = 200, mensaje = "Contrasena cambiada exitosamente" });
            }
            catch (UsuarioException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error, no se pudo actualizar la contrasena." });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        [HttpPost("logout")]
        public IActionResult Logout([FromBody] TokenDTO tokenDto)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jsonToken;

                try
                {
                    jsonToken = handler.ReadToken(tokenDto.Token) as JwtSecurityToken;

                    if (jsonToken == null)
                    {
                        return BadRequest(new { message = "El token proporcionado no es válido." });
                    }
                }
                catch (Exception)
                {
                    // Si no se puede leer el token (por expiración o formato inválido)
                    jsonToken = null;
                }

                // Agrega el token a la blacklist, incluso si está expirado
                CUAgregarTokenABlacklist.Agregar(tokenDto);

                return Ok(new { mensaje = "Sesión cerrada exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno al cerrar sesión." });
            }
        }


        /* [ProducesResponseType(StatusCodes.Status200OK)]
           [ProducesResponseType(StatusCodes.Status400BadRequest)]
           [ProducesResponseType(StatusCodes.Status500InternalServerError)]
           [AllowAnonymous]
           [HttpPost("logout")]
           public IActionResult Logout([FromBody] TokenDTO tokenDto)
           {
               try
               {
                   // Verificar si el token tiene el formato correcto
                   var handler = new JwtSecurityTokenHandler();

                   // Intenta leer el token
                   var jsonToken = handler.ReadToken(tokenDto.Token) as JwtSecurityToken;

                   if (jsonToken == null)
                   {
                       return BadRequest(new { message = "El token proporcionado no es válido." });
                   }

                   if (jsonToken.ValidTo < DateTime.UtcNow)
                   {
                       return BadRequest(new { message = "El token ha expirado." });
                   }


                   CUAgregarTokenABlacklist.Agregar(tokenDto);

                   return Ok(new { mensaje = "Sesión cerrada exitosamente." });
               }
               catch (InvalidOperationException ex)
               {
                   return BadRequest(new { message = ex.Message });
               }
               catch (Exception ex)
               {
                   return StatusCode(500, new { message = "Ocurrió un error interno al cerrar sesión." });
               }
           }*/
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
      //  [Consumes("multipart/form-data")]

        public IActionResult Put(int id, [FromForm] ModificarUsuarioDTO usuarioDto)
        {
            if (!ModelState.IsValid)  
            {
                return BadRequest(ModelState);  
            }
            try
            {
                var usuario = CUBuscar.Buscar(id);
            if (usuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado." });
            }
              


                CUModificar.Modificar(id, usuarioDto);

                
                return Ok(new { message = "La información del usuario se ha actualizado correctamente." });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });

            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "No autorizado. Por favor inicie sesión." });
            }
            catch (UsuarioException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar la información del usuario." });
            }
        }


        [HttpGet("ver-documento/{id}/{tipoDocumento}")]
        public IActionResult VerDocumento(int id, string tipoDocumento)
        {
            var usuario = CUBuscar.Buscar(id);
            if (usuario == null)
            {
                return NotFound( new { message = "Usuario no encontrado." });
            }

        
            string documentoRuta = tipoDocumento.ToLower() switch
            {
                "pasaporte" => usuario.PasaporteDocumentoRuta,
                "visado" => usuario.VisaDocumentoRuta,
                "seguro" => usuario.SeguroDeViajeDocumentoRuta,
                "vacunas " => usuario.SeguroDeViajeDocumentoRuta,
                _ => null
            };

            if (string.IsNullOrEmpty(documentoRuta))
            {
                return NotFound(new { message = "Documento no encontrado." });
            }

            // Obtener la ruta completa del archivo
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", documentoRuta.TrimStart('/'));

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(new { message = "Archivo no encontrado." });
            }

            // Determinar el tipo MIME del archivo
            var contentType = "application/octet-stream"; // Tipo MIME por defecto
            var ext = Path.GetExtension(filePath).ToLower();
            if (ext == ".pdf") contentType = "application/pdf";
            else if (ext == ".jpg" || ext == ".jpeg") contentType = "image/jpeg";
            else if (ext == ".png") contentType = "image/png";

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, contentType, Path.GetFileName(filePath));
        }
        

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPatch("{id}/estado")]
        public IActionResult ActualizarEstadoCoordinador(int id, [FromBody] ActualizarEstadoCoordinadorDTO dto)
        {
            var usuario = CUBuscar.Buscar(id);
            if (usuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado." });
            }
          

            try
            {
                CUActualizarEstadoCoordinador.ActualizarEstado(id, dto.Estado);
                return Ok(new { message = "El estado del coordinador se ha actualizado correctamente." });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "El usuario no es un coordinador." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar el estado.", details = ex.Message });
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("RecuperarContrasena")]
        public IActionResult RecuperarContrasena(string email)
        {
            try
            {
                CURecuperarContrasena.EnviarCorreo(email);
                return Ok(new { mensaje = "Correo enviado con las instrucciones para recuperar tu contraseña." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}


