using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUItinerario;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.RequestDecompression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUItinerario
{
    public class CUEliminarItinerario : IEliminarItinerario
    {
        private readonly IRepositorioItinerario RepoItinerario;

        public CUEliminarItinerario(IRepositorioItinerario repoItinerario)
        {
            RepoItinerario = repoItinerario;
        }

        public void Eliminar(ItinerarioDTO dto)
        {
            {
                {
                    if (dto == null)
                        throw new ArgumentNullException(nameof(dto), "El itinerario proporcionado es nulo.");

                    // TODO: Verificar si tiene itinerarios asociados cuando se implementen los itinerarios
                    RepoItinerario.Remove(new Itinerario { Id = dto.Id });
                }
            }
        }
    }
}
