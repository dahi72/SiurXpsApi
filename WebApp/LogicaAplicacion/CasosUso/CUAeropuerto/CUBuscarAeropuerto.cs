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
    public class CUBuscarAeropuerto : IBuscarAeropuerto
    {
        private readonly IRepositorioAeropuerto RepoAeropuerto;


        public CUBuscarAeropuerto(IRepositorioAeropuerto repo)
        {

            RepoAeropuerto = repo;
        }
        public AeropuertoDTO Buscar(int id)
        {
            AeropuertoDTO dto = null;

            Aeropuerto aero = RepoAeropuerto.FindById(id);
            if (aero != null)
            {
                dto = new AeropuertoDTO()
                {
                    Id = aero.Id,
                    Nombre = aero.Nombre,
                    PaginaWeb = aero.PaginaWeb,
                    Direccion=  aero.Direccion,
                    PaisId = aero.PaisId,
                    CiudadId = aero.CiudadId,


     
    };
            }
            return dto;
        }
    
    }
}
