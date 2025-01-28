using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUAerolinea;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUAerolinea
{
    public class CUBajaAerolinea : IBajaAerolinea
    {
        private readonly IRepositorioAerolinea RepoAerolinea;


        public CUBajaAerolinea(IRepositorioAerolinea repo)
        {

            RepoAerolinea = repo;
        }

        public void Eliminar(AerolineaDTO dto)
        {
            {
                {
                    if (dto == null)
                        throw new ArgumentNullException(nameof(dto), "La aerolínea proporcionada es nula.");

                    // TODO: Verificar si tiene itinerarios asociados cuando se implementen los itinerarios
                    RepoAerolinea.Remove(new Aerolinea { Id = dto.Id });
                }
            }
        }
    }
}
