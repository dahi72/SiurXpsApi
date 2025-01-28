using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUPais;
using LogicaAplicacion.InterfacesCU.InterfacesCUCiudad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using LogicaNegocio.Dominio;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace WebApp.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly IObtenerPaises CUObtener;
        private readonly IListadoPaises CUListado;
        private readonly IAltaPais CUAlta;


        public PaisController(IObtenerPaises cuListado, IAltaPais cuAlta, IListadoPaises cUListado)
        {
            CUObtener = cuListado;
            CUAlta = cuAlta;
            CUListado = cUListado;
        }

        /// <summary>
        /// Obtiene la lista de Paises.
        /// </summary>
        /// <returns>Lista de Paises</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("listado", Name = "ListadoPaises")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<PaisDTO> paises = CUListado.Listado();

                if (paises == null || !paises.Any())
                {
                    paises = ObtenerYGuardarDesdeAPI();
                }

                return new JsonResult(paises);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        private IEnumerable<PaisDTO> ObtenerYGuardarDesdeAPI()
        {
            // Usar HttpClientHandler para habilitar la descompresión automática
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }

            using (HttpClient cliente = new HttpClient(handler))
            {
                string apiUrl = "https://restcountries.com/v3.1/all";
                List<PaisDTO> paises = new List<PaisDTO>();

                try
                {
                    // Realiza la solicitud HTTP GET de forma síncrona
                    HttpResponseMessage respuesta = cliente.GetAsync(apiUrl).Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        // Lee el cuerpo de la respuesta como una cadena de texto (automáticamente descomprimida si está comprimida)
                        string body = respuesta.Content.ReadAsStringAsync().Result;
                        JArray jsonArray = JArray.Parse(body);

                        // Itera sobre la respuesta y guarda cada país en la base de datos
                        foreach (JObject item in jsonArray)
                        {
                            // Obtén los datos del país
                            string nombre = item.SelectToken("name.common")?.ToString();
                            string codigoIso = item.SelectToken("cca2")?.ToString();

                            // Crea el DTO para el país
                            PaisDTO pais = new PaisDTO
                            {
                                Nombre = nombre,
                                CodigoIso = codigoIso
                            };

                            // Guarda el país en la base de datos
                            CUAlta.Alta(pais);
                            paises.Add(pais);
                        }
                    }
                    else
                    {
                        throw new Exception("Error al obtener los países de la API.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en la llamada a la API: " + ex.Message);
                }

                // Devuelve la lista de países obtenidos y guardados
                return paises;
            }
        }






    }
}