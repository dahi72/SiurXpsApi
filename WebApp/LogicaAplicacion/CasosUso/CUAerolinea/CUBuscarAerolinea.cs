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
    public class CUBuscarAerolinea : IBuscarAerolinea
    {
        private readonly IRepositorioAerolinea RepoAerolinea;


        public CUBuscarAerolinea(IRepositorioAerolinea repo)
        {

            RepoAerolinea = repo;
        }

        public AerolineaDTO Buscar(int id)
        {
            AerolineaDTO dto = null;

            Aerolinea aero = RepoAerolinea.FindById(id);
            if (aero != null)
            {
                dto = new AerolineaDTO()
                {
                    Id = aero.Id,
                    Nombre = aero.Nombre,
                    PaginaWeb = aero.PaginaWeb

                };
            }
            return dto;
        }
    }
}
