using DTOs;
using ExcepcionesPropias;
using LogicaAplicacion.CasosUso.CUGrupoDeViaje;
using LogicaAplicacion.InterfacesCU.InterfacesCUGrupoDeViaje;
using LogicaAplicacion.InterfacesCU.InterfaesCUGrupoDeViaje;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoDeViajeController : ControllerBase
    {
        private readonly IAltaGrupo CUAltaGrupo;
        private readonly IAgregarViajeroAGrupo CUAgregarViajero;
        private readonly IEliminarViajero CUEliminarViajero;
        private readonly IBuscarGrupoPorId CUBuscar;
        private readonly IGetGruposPorCoordinadorId CUgetGruposPorCoordinadorId;
        private readonly IBajaGrupoDeViaje CUBaja;
        private readonly IConfirmarGrupoDeViaje CUConfirmarGrupo;
        public GrupoDeViajeController(IAltaGrupo cuAltaGrupo, IAgregarViajeroAGrupo cUAgregarViajero, IEliminarViajero cUEliminarViajero, IBuscarGrupoPorId cUBuscarGrupoPorId, IGetGruposPorCoordinadorId getGruposPorCoordinadorId, IBajaGrupoDeViaje cuBaja, IConfirmarGrupoDeViaje confirmar)
        {
            CUAltaGrupo = cuAltaGrupo;
            CUAgregarViajero = cUAgregarViajero;
            CUEliminarViajero = cUEliminarViajero;
            CUBuscar = cUBuscarGrupoPorId;
            CUgetGruposPorCoordinadorId = getGruposPorCoordinadorId;
            CUBaja = cuBaja;
            CUConfirmarGrupo = confirmar;
        
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
         [ProducesResponseType(StatusCodes.Status400BadRequest)]
         [ProducesResponseType(StatusCodes.Status401Unauthorized)]
         [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpPost("altaGrupo")]
        public IActionResult Post(GrupoDeViajeDTO grupo)
        {
            if (grupo == null) return BadRequest(new { message = "No se envió información para la creacion del grupo" });
            try
            {

                CUAltaGrupo.Alta(grupo);
                return Ok(new { codigo = 200, mensaje = "Grupo creado exitosamente." });
            }
            catch (GrupoDeViajeException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error, no se pudo realizar la creacion del grupo." });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("agregarViajero/{grupoId}")]
        public IActionResult AgregarViajero(int grupoId, [FromBody] AltaUsuarioDTO viajeroDto)
        {
            try
            {
                CUAgregarViajero.AgregarViajero(grupoId, viajeroDto);
                return Ok(new { mensaje = "Viajero agregado exitosamente al grupo." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error interno." });
            }
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpDelete("{grupoId}/viajeros/{usuarioId}")]
        public IActionResult EliminarViajero(int grupoId, int usuarioId)
        {
            try
            {
                CUEliminarViajero.EliminarViajero(grupoId, usuarioId);
                return Ok("Viajero eliminado correctamente.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error interno." });
            }
        }

        [AllowAnonymous]// cambiar esto es solo para pruebas
        [HttpGet("{id}", Name = "BuscarMisGrupos")]
        public IActionResult FindById(int id)
        {
            GrupoDeViajeDTO grupo = CUBuscar.Buscar(id);
            if (grupo == null)
            {
                return NotFound(new { message = "Grupo no encontrado" });
            }
            //TODO: MAS CATCHS tiene que haber unauthorized
            return Ok(grupo);
        }

        [AllowAnonymous]
        [HttpGet("coordinador/{coordinadorId}/grupos")]
        public IActionResult GetGruposPorCoordinador(int coordinadorId)
        {
            try
            {
                IEnumerable<GrupoDeViajeDTO> grupos = CUgetGruposPorCoordinadorId.GetGruposDeViajeCoordinadorId(coordinadorId);

                if (grupos.Any())
                {
                    return Ok(grupos);
                }
                else
                {
                    return NotFound(new { message = "No se encontraron grupos para este coordinador." });
                }
                
            }
            catch (GrupoDeViajeException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno al obtener los grupos." });
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

                GrupoDeViajeDTO grupo = CUBuscar.Buscar(id);
                if (grupo == null)
                    return NotFound($"No existe el grupo con el id {id}");


                CUBaja.Eliminar(grupo);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (GrupoDeViajeException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al intentar eliminar el grupo." });
            }
        }

        /// <summary>
        /// Confirma un grupo de viaje y envía correos a los viajeros.
        /// </summary>
        /// <param name="idGrupo">ID del grupo de viaje a confirmar</param>
        /// <returns>Respuesta HTTP con estado y mensaje</returns>
        [HttpPut]
        [Route("{idGrupo}/confirmar")]
        public IActionResult ConfirmarGrupo(int idGrupo)
        {
            try
            {
                CUConfirmarGrupo.ConfirmarGrupo(idGrupo);
                return Ok(new
                {
                    mensaje = "El grupo de viaje fue confirmado exitosamente. Se enviaron correos a los viajeros."
                });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (GrupoDeViajeException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error al intentar eliminar el grupo." });
            }
            
        }
    }
 }




