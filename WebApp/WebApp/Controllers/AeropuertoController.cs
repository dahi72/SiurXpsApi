using DTOs;
using ExcepcionesPropias;
using LogicaAplicacion.InterfacesCU.InterfacesCUAerolinea;
using LogicaAplicacion.InterfacesCU.InterfacesCUAeropuerto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AeropuertoController : ControllerBase
    {
        private readonly IAltaAeropuerto CUAlta;
        private readonly IBuscarAeropuerto CUBuscar;
        private readonly IBajaAeropuerto CUBaja;
        private readonly IModificarAeropuerto CUModificar;
        private readonly IListadoFiltradoAeropuerto CUFiltro;


        public AeropuertoController(IAltaAeropuerto cuAlta, IBuscarAeropuerto cUBuscar, IBajaAeropuerto cUBaja, IModificarAeropuerto cUModificar, IListadoFiltradoAeropuerto cUFiltro)
        {
            CUAlta = cuAlta;
            CUBuscar = cUBuscar;
            CUBaja = cUBaja;
            CUModificar = cUModificar;
            CUFiltro = cUFiltro;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpPost("altaAeropuerto")]
        public IActionResult Post(AeropuertoDTO aeropuerto)
        {
            if (aeropuerto == null) return BadRequest("No se envió información para la creacion de la aeropuerto");
            try
            {

                CUAlta.Alta(aeropuerto);
                return Ok(new { codigo = 200, mensaje = "Aeropuerto creado exitosamente." });
            }

            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (AeropuertoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error, no se pudo realizar la creacion del aeropuerto." });
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

                AeropuertoDTO aero = CUBuscar.Buscar(id);
                if (aero == null)
                    return NotFound($"No existe el aeropuerto con el id {id}");


                CUBaja.Eliminar(aero);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (AerolineaException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al intentar eliminar el aeropuerto." });
            }
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        //  [Consumes("multipart/form-data")]

        public IActionResult Put(int id, [FromForm] AeropuertoDTO aeroDto)
        {

            try
            {

                aeroDto.Id = id;
                CUModificar.Modificar(aeroDto);


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
            catch (AerolineaException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar la información del aeropuerto." });
            }
        }

        [HttpGet("aeropuertos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BuscarAeropuertos([FromQuery] string nombre = null)
        {
            try
            {
                IEnumerable<AeropuertoDTO> aeropuertos = CUFiltro.AeropuertosPorNombre(nombre);

                if (aeropuertos.Any())
                {

                    return Ok(aeropuertos);
                }
                else
                {
                    return NotFound(new { message = "No se encontraron aeropuertos con los criterios especificados." });

                }
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (AeropuertoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al inesperado." });
            }

        }
    }
}
