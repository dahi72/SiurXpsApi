using DTOs;
using ExcepcionesPropias;
using LogicaAplicacion.CasosUso.CUHotel;
using LogicaAplicacion.InterfacesCU.InterfacesCUAerolinea;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AerolineaController : ControllerBase
    {
        private readonly IAltaAerolinea CUAlta;
        private readonly IBuscarAerolinea CUBuscar;
        private readonly IModificarAerolinea CUModificar;
        private readonly IBajaAerolinea CUBaja;
        private readonly IListadoFIltradoAerolinea CUFiltro;



        public AerolineaController (IAltaAerolinea cuAlta, IBuscarAerolinea cUBuscar, IModificarAerolinea cUModificar, IBajaAerolinea cUBaja, IListadoFIltradoAerolinea cUFiltro)
        {
            CUAlta = cuAlta;
            CUBuscar = cUBuscar;
            CUModificar = cUModificar;
            CUBaja = cUBaja;
            CUFiltro = cUFiltro;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpPost("altaAerolinea")]
        public IActionResult Post(AerolineaDTO aerolinea)
        {
            if (aerolinea == null) return BadRequest("No se envió información para la creacion de la aerolínea");
            try
            {

                CUAlta.Alta(aerolinea);
                return Ok(new { codigo = 200, mensaje = "Aerolínea creada exitosamente." });
            }

            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (AerolineaException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error, no se pudo realizar la creacion de la aerolínea." });
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

                AerolineaDTO aero = CUBuscar.Buscar(id);
                if (aero == null)
                    return NotFound($"No existe la aerolínea con el id {id}");


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
                return StatusCode(500, new { message = "Ocurrió un error al intentar eliminar la aerolínea." });
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        //  [Consumes("multipart/form-data")]

        public IActionResult Put(int id, [FromForm] AerolineaDTO aeroDto)
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
                return StatusCode(500, new { message = "Ocurrió un error al actualizar la información de la aerolínea." });
            }
        }

        [HttpGet("aerolineas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BuscarAeropuertos([FromQuery] string nombre = null)
        {
            try
            {
                IEnumerable<AerolineaDTO> aerolineas = CUFiltro.AerolineasPorNombre(nombre);

                if (aerolineas.Any())
                {

                    return Ok(aerolineas);
                }
                else
                {
                    return NotFound(new { message = "No se encontraron aerolineas con los criterios especificados." });

                }
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
                return StatusCode(500, new { message = "Ocurrió un error al inesperado." });
            }

        }


    }
}
