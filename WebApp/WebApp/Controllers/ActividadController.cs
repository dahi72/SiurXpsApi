using DTOs;
using ExcepcionesPropias;
using LogicaAplicacion.CasosUso.CUActividad;
using LogicaAplicacion.InterfacesCU.InterfacesCUActividad;
using LogicaAplicacion.InterfacesCU.InterfacesCUAerolinea;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadController : ControllerBase
    {
        private readonly IAltaActividad CUAlta;
        private readonly IBuscarActividad CUBuscar;
        private readonly IModificarActividad CUModificar;
        private readonly IBajaActividad CUBaja;
        private readonly IListadoActividades CUListado;
        private readonly IAltaDeUsuarioEnActividadOpcional CUInscripcionDeUsu;



        public ActividadController(IAltaActividad cuAlta, IBuscarActividad buscarActividad,IModificarActividad modificarActividad, IBajaActividad bajaActividad,IListadoActividades listado,IAltaDeUsuarioEnActividadOpcional inscr)
            
        {
            CUAlta = cuAlta;
            CUBuscar = buscarActividad;
            CUModificar = modificarActividad;
            CUBaja = bajaActividad;
            CUListado = listado;
            CUInscripcionDeUsu = inscr;
                 }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpPost("altaActividad")]
        public IActionResult Post(ActividadDTO actividad)
        {
            if (actividad == null) return BadRequest("No se envió información para la creacion de la actividad");
            try
            {

                CUAlta.Alta(actividad);
                return Ok(new { codigo = 200, mensaje = "Actividad creada exitosamente." });
            }

            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ActividadException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error, no se pudo realizar la creacion de la actividad." });
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("ID inválido.");

            try
            {

                ActividadDTO act = CUBuscar.Buscar(id);
                if (act == null)
                    return NotFound($"No existe la actividad con el id {id}");


                CUBaja.Eliminar(act);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ActividadException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al intentar eliminar la actividad." });
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ActividadDTO dto)
        {
            try
            {
                ActividadDTO vuelo = CUBuscar.Buscar(id);

                dto.Id = id;

                CUModificar.Modificar(dto);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "No autorizado. Por favor inicie sesión." });
            }
            catch (ActividadException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar la información de la actividad." });
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "FindByIdActividad")]
        public IActionResult FindById(int id)
        {
            try
            {
                ActividadDTO actividad = CUBuscar.Buscar(id);
                if (actividad == null)
                {
                    return NotFound(new { message = "Actividad no encontrado" });
                }
                // TODO: tiene que haber unauthorized
                return Ok(actividad);
            }
            catch (ActividadException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno, no se pudo encontrar la actividad." });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("listado", Name = "ListadoActividades")]
        public IActionResult Get()
        {
            try
            {
                return Ok(CUListado.Listado());
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "No autorizado. Por favor inicie sesión." });
            }
            catch (ActividadException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno, no se pudo encontrar la actividad." });
            }

        }
        [HttpPost("inscribir")]
        public IActionResult InscribirUsuario([FromBody] InscripcionAActividadDTO dto)
        {
            try
            {
               
                CUInscripcionDeUsu.InscripcionDeUsuario(dto);

       
                return Ok("Inscripción exitosa");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "No autorizado. Por favor inicie sesión." });
            }
            catch (ActividadException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno, no se pudo encontrar la actividad." });
            }
        }
    
    }
}
