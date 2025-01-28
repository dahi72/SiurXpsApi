using DTOs;
using ExcepcionesPropias;
using LogicaAplicacion.InterfacesCU.InterfacesCUActividad;
using LogicaAplicacion.InterfacesCU.InterfacesCUTraslado;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrasladoController : ControllerBase
    {
        private readonly IAltaTraslado CUAlta;
        private readonly IBuscarTraslado CUBuscar;
        private readonly IModificarTraslado CUModificar;
        private readonly IBajaTraslado CUBaja;
        private readonly IListadoTraslados CUListado;



        public TrasladoController(IAltaTraslado cuAlta, IBuscarTraslado buscar, IModificarTraslado modificar, IBajaTraslado baja, IListadoTraslados listado)

        {
            CUAlta = cuAlta;
            CUBuscar = buscar;
            CUModificar = modificar;
            CUBaja = baja;
            CUListado = listado;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpPost("altaTraslado")]
        public IActionResult Post(TrasladoDTO traslado)
        {
            if (traslado == null) return BadRequest("No se envió información para la creacion del traslado");
            try
            {

                CUAlta.Alta(traslado);
                return Ok(new { codigo = 200, mensaje = "Traslado creado exitosamente." });
            }

            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (TrasladoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error, no se pudo realizar la creacion del traslado." });
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

                TrasladoDTO act = CUBuscar.Buscar(id);
                if (act == null)
                    return NotFound($"No existe el traslado con el id {id}");


                CUBaja.Eliminar(act);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (TrasladoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al intentar eliminar el traslado." });
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TrasladoDTO dto)
        {
            try
            {
                TrasladoDTO vuelo = CUBuscar.Buscar(id);

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
            catch (TrasladoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar la información de la actividad." });
            }
        }

        [AllowAnonymous]
        [HttpGet("api/Traslado/{id}", Name = "FindByIdTraslado")]
        public IActionResult FindById(int id)
        {
            try
            {
                TrasladoDTO traslado = CUBuscar.Buscar(id);
                if (traslado == null)
                {
                    return NotFound(new { message = "Traslado no encontrado" });
                }
                // tiene que haber unauthorized
                return Ok(traslado);
            }
            catch (TrasladoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno, no se pudo encontrar el traslado." });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("listado", Name = "ListadoTraslados")]
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
            catch (TrasladoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno, no se pudo encontrar el traslado." });
            }

        }
    }
}
