using DTOs;
using ExcepcionesPropias;
using LogicaAplicacion.CasosUso.CUItinerario;
using LogicaAplicacion.InterfacesCU.InterfacesCUAerolinea;
using LogicaAplicacion.InterfacesCU.InterfacesCUItinerario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItinerarioController : ControllerBase
    {
        private readonly IAltaItinerario CUAlta;
        private readonly IAgregarEventoAItinerario CUAgregarEvento;
        private readonly IEliminarEventoDeItinerario CUEliminarEvento;
        private readonly IModificarEvento CUModificarEvento;
        private readonly IEliminarItinerario CUEliminarItinerario;
        private readonly IBuscarItinerario CUBuscar;
        private readonly IListadoItinerarios CUListado;
        private readonly IModificarGrupoDeItinerario CUModificar;
        private readonly IObtenerEventosDelItinerario CUObtenerEventos;
        public ItinerarioController(IAltaItinerario cUAlta, IAgregarEventoAItinerario cuAgregar, IEliminarEventoDeItinerario cUEliminarEvento, IModificarEvento cUModificarEvento, IEliminarItinerario cUEliminarItinerario, IBuscarItinerario cUBuscar, IListadoItinerarios cUListado, IModificarGrupoDeItinerario cUModificar, IObtenerEventosDelItinerario cUObtenerEventos)
        {
            CUAlta = cUAlta;
            CUAgregarEvento = cuAgregar;
            CUEliminarEvento = cUEliminarEvento;
            CUModificarEvento = cUModificarEvento;
            CUEliminarItinerario = cUEliminarItinerario;
            CUBuscar = cUBuscar;
            CUListado = cUListado;
            CUModificar = cUModificar;
            CUObtenerEventos = cUObtenerEventos;

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpPost("altaItinerario")]
        public IActionResult Post(ItinerarioDTO itinerario)
        {
            if (itinerario == null) return BadRequest("No se envió información para la creacion del itinerario");
            try
            {

                CUAlta.Alta(itinerario);
                return Ok(new { codigo = 200, mensaje = "Itinerario creado exitosamente." });
            }

            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ItinerarioException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error, no se pudo realizar la creacion del itinerario." });
            }
        }
        [HttpPost("{idItinerario}/evento")]
        public IActionResult AgregarEventoAItinerario(int idItinerario, [FromBody] EventoItinerarioDTO evento)
        {
            try
            {
                // Llamar al caso de uso para agregar el evento al itinerario
                CUAgregarEvento.AgregarEvento(idItinerario, evento);

                // Retornar una respuesta exitosa
                return Ok(new { message = "Evento agregado exitosamente al itinerario" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exep: ", ex);
                // Manejar cualquier excepción y retornar un error adecuado
                return StatusCode(500, new { message = "Ocurrió un error, no se pudo agregar el evento." });
            }

        }

        [HttpDelete("{idItinerario}/eventos/{idEvento}")]
        public IActionResult EliminarEvento(int idItinerario, int idEvento)
        {
            try
            {

                CUEliminarEvento.EliminarEvento(idItinerario, idEvento);

                // Retornamos una respuesta de éxito
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{idItinerario}/eventos/{idEvento}/horario")]
        public IActionResult ModificarHorarioEvento(int idItinerario, int idEvento, [FromBody] DateTime nuevoHorario)
        {
            try
            {
                if (nuevoHorario == default)
                {
                    return BadRequest("El nuevo horario no puede ser nulo o por defecto.");
                }

                CUModificarEvento.ModificarEvento(idItinerario, idEvento, nuevoHorario);
                return NoContent(); // Indica que la operación se completó sin problemas
            }
            catch (Exception ex)
            {
                // Devuelve un mensaje de error general al cliente
                return BadRequest(new { error = ex.Message });
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

                ItinerarioDTO iti = CUBuscar.Buscar(id);
                if (iti == null)
                    return NotFound($"No existe el aeropuerto con el id {id}");


                CUEliminarItinerario.Eliminar(iti);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ItinerarioException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al intentar eliminar el itinerario." });
            }
        }
        [AllowAnonymous] // TODO: cambiar esto es solo para pruebas
        [HttpGet("{id}", Name = "BuscarItinerarios")]
        public IActionResult FindById(int id)
        {
            try
            {
                ItinerarioDTO iti = CUBuscar.Buscar(id);
                if (iti == null)
                {
                    return NotFound(new { message = "Itinerario no encontrado" });
                }

                return Ok(iti);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ItinerarioException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            /*catch (UnauthorizedAccessException)
            {
               return Unauthorized(new { message = "No tiene autorización para acceder a este itinerario." });
            }*/
            catch (Exception)
            {

                return StatusCode(500, new { message = "Ocurrió un error al intentar buscar el itinerario." });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("listado", Name = "ListadoItinerarios")]
        public IActionResult Get()
        {
            try
            {
                return Ok(CUListado.Listado());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{idItinerario}/grupo_itinerario/{idGrupo}")]
        //  [Consumes("multipart/form-data")]

        public IActionResult Put(int idItinerario, int idGrupo)
        {

            try
            {


                CUModificar.Modificar(idItinerario, idGrupo);


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
            catch (ItinerarioException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar la información del itinerario." });
            }
        }
        [HttpGet("{idItinerario}/eventos")]
        public ActionResult<IEnumerable<EventoItinerarioDTO>> ObtenerEventos(int idItinerario)
        {
            try
            {
                IEnumerable<EventoItinerarioDTO> eventos = CUObtenerEventos.ObtenerEventos(idItinerario);
                return Ok(eventos);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });

            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "No autorizado. Por favor inicie sesión." });
            }
            catch (ItinerarioException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar la información del itinerario." });
            }
        }

    }


}