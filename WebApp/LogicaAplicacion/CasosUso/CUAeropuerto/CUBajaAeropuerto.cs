using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUAeropuerto;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUAeropuerto
{
    public class CUBajaAeropuerto : IBajaAeropuerto
    {
        private readonly IRepositorioAeropuerto RepoAeropuerto;


        public CUBajaAeropuerto(IRepositorioAeropuerto repo)
        {

            RepoAeropuerto = repo;
        }

        public void Eliminar(AeropuertoDTO dto)
        {
            {
                {
                    if (dto == null)
                        throw new ArgumentNullException(nameof(dto), "La aerolínea proporcionada es nula.");

                    // TODO: Verificar si tiene itinerarios asociados cuando se implementen los itinerarios
                    RepoAeropuerto.Remove(new Aeropuerto { Id = dto.Id });
                }
            }
        }
    }
}