using DTOs;
using ExcepcionesPropias;
using LogicaAplicacion.InterfacesCU.InterfacesCUCiudad;
using LogicaAplicacion.InterfacesCU.InterfacesCUPais;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace WebApp.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadController : ControllerBase
    {
        private readonly IObtenerCiudadesPorPais CUObtenerCiudades;
        private readonly IListadoCiudades CUListadoCiudades;
        private readonly IAltaCiudad CUAltaCiudad;

        public CiudadController(IObtenerCiudadesPorPais cuCiudades, IListadoCiudades cUListadoCiudades, IAltaCiudad cUAltaCiudad)
        {
            
            CUObtenerCiudades = cuCiudades;
            CUListadoCiudades = cUListadoCiudades;
            CUAltaCiudad = cUAltaCiudad;
        }

     [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{codigoISO}/ciudades", Name = "ListadoCiudadesPorPais")]
        public IActionResult GetCiudadesPorPais(string codigoISO)
        {
            try
            {
                var ciudades = CUObtenerCiudades.ObtenerPais(codigoISO);

                if (ciudades == null || !ciudades.Any())
                {
                    ciudades = ObtenerYGuardarCiudadDesdeAPI(codigoISO);
                }
                if (ciudades == null || !ciudades.Any())
                {
                    return NotFound(new { message = $"No se encontraron ciudades para el país con código ISO: {codigoISO}" });
                }
                return Ok(ciudades);
            }
            catch (CiudadException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        private IEnumerable<CiudadDTO> ObtenerYGuardarCiudadDesdeAPI(string codigoISO)
        {
            HttpClient cliente = new HttpClient();


            // Reemplaza 'tu_usuario' con tu nombre de usuario de GeoNames
            string apiUrl = $"http://api.geonames.org/searchJSON?country={codigoISO}&username=SiurXp&password=SiurXp";
            try
            {
                var tarea = cliente.GetAsync(apiUrl);
                tarea.Wait();
                HttpResponseMessage respuesta = tarea.Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    string body = respuesta.Content.ReadAsStringAsync().Result;
                    JObject jsonResponse = JObject.Parse(body);
                    JArray ciudadesArray = (JArray)jsonResponse["geonames"];

                
                    List<CiudadDTO> ciudades = new List<CiudadDTO>();

                    foreach (var item in ciudadesArray)
                    {
                        CiudadDTO ciudad = new CiudadDTO
                        {
                            Nombre = item["toponymName"]?.ToString(),

                            PaisCodigoIso = codigoISO
                        };

                        CUAltaCiudad.Alta(ciudad);
                        ciudades.Add(ciudad);
                    }

                    return ciudades;
                }
                else
                {
                    string errorBody = respuesta.Content.ReadAsStringAsync().Result;
                    throw new Exception($"Error al obtener las ciudades de la API GeoNames: {errorBody}");
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
             
            }
        }

    }
}

