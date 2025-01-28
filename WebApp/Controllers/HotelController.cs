using DTOs;
using ExcepcionesPropias;
using LogicaAplicacion.InterfacesCU.InterfacesCUHotel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IAltaHotel CUAltaHotel;
        private readonly IBuscarHotelPorId CUBuscar;
        private readonly IModificarHotel CUModificar;
        private readonly IBajaHotel CUBaja;
        private readonly IFiltroHotelesPorPaisYCiudad CUFiltro;

        public HotelController(IAltaHotel cuAlta, IBuscarHotelPorId cuBuscar, IModificarHotel cuModificar, IBajaHotel cuBajaHotel, IFiltroHotelesPorPaisYCiudad cuFiltro)

        {
            CUAltaHotel = cuAlta;
            CUBuscar = cuBuscar;
            CUModificar = cuModificar;
            CUBaja = cuBajaHotel;
            CUFiltro = cuFiltro;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpPost("altaHotel")]
        public IActionResult Post(HotelDTO hotel)
        {
            if (hotel == null) return BadRequest("No se envió información para la creacion del hotel");
            try
            {

                CUAltaHotel.Alta(hotel);
                return Ok(new { codigo = 200, mensaje = "Hotel creado exitosamente." });
            }

            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (HotelException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error, no se pudo realizar la creacion del hotel." });
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        //  [Consumes("multipart/form-data")]

        public IActionResult Put(int id, [FromForm] HotelDTO hotelDto)
        {

            try
            {

                hotelDto.Id = id;
                CUModificar.Modificar(hotelDto);


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
            catch (HotelException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar la información del hotel." });
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

                HotelDTO hotel = CUBuscar.Buscar(id);
                if (hotel == null)
                    return NotFound($"No existe el hotel con el id {id}");


                CUBaja.Eliminar(hotel);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (HotelException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al intentar eliminar el hotel." });
            }
        }

        [HttpGet("hoteles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BuscarHoteles([FromQuery] string nombre = null, [FromQuery] string codigoIso = null, [FromQuery] string ciudad = null)
        {
            try
            {
                IEnumerable<HotelDTO> hoteles = CUFiltro.HotelesPorPaisyCiudad(nombre, codigoIso, ciudad);

                if (hoteles.Any())
                {

                    return Ok(hoteles);
                }
                else
                {
                    return NotFound(new { message = "No se encontraron hoteles con los criterios especificados." });

                }
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (HotelException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al buscar el hotel." });
            }

        }



        [HttpGet("saludo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ObtenerSaludo()
        {
            return Ok("Hola Mundo");
        }


        [HttpGet] // accept get
        [HttpPost] // accept post
        [Route("")] // route default request to this method.
        public IActionResult Get()
        {
            return Ok();
        }


    }

}