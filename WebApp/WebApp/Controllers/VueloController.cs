using DTOs;
using ExcepcionesPropias;
using LogicaAplicacion.CasosUso.CUHotel;
using LogicaAplicacion.InterfacesCU.InterfacesCUHotel;
using LogicaAplicacion.InterfacesCU.InterfacesCUVuelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VueloController : ControllerBase
    {
        private readonly IAltaVuelo CUAlta;
        private readonly IModificarVuelo CUModificar;
        private readonly IBuscarVuelo CUBuscar;
        private readonly IBajaVuelo CUBaja;
        private readonly IListadoFiltroVuelo CUFiltro;
        public VueloController(IAltaVuelo cuAlta, IModificarVuelo cUModificar, IBuscarVuelo cUBuscar, IBajaVuelo cUBaja, IListadoFiltroVuelo cUFiltro)
        {
            CUAlta = cuAlta;
            CUModificar = cUModificar;
            CUBuscar = cUBuscar;
            CUBaja = cUBaja;
            CUFiltro = cUFiltro;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpPost("altaVuelo")]
        public IActionResult Post(VueloDTO vuelo)
        {
            if (vuelo == null) return BadRequest("No se envió información para la creacion del vuelo");
            try
            {

                CUAlta.Alta(vuelo);
                return Ok(new { codigo = 200, mensaje = "Vuelo creado exitosamente.", id = vuelo.Id });
            }

            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (VueloException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error, no se pudo realizar la creacion del vuelo." });
            }
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] VueloDTO vueloDto)
        {
            try
            {
                VueloDTO vuelo = CUBuscar.Buscar(id);
              
                vueloDto.Id = id;

                CUModificar.Modificar(vueloDto);

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
            catch (VueloException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar la información del vuelo." });
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

                VueloDTO vuelo = CUBuscar.Buscar(id);
                if (vuelo == null)
                    return NotFound($"No existe el vuelo con el id {id}");

                
                CUBaja.Eliminar(vuelo);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (VueloException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al intentar eliminar el vuelo." });
            }
        }

        [HttpGet("vuelos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BuscarVuelos([FromQuery] string nombre = null)
        {
            try
            {
                IEnumerable<VueloDTO> vuelos = CUFiltro.VuelosPorNombre(nombre);

                if (vuelos.Any())
                {

                    return Ok(vuelos);
                }
                else
                {
                    return NotFound(new { message = "No se encontraron vuelos con los criterios especificados." });

                }
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (VueloException ex)
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
