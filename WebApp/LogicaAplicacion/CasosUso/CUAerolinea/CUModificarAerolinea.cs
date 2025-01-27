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
    public class CUModificarAerolinea : IModificarAerolinea
    {
        private readonly IRepositorioAerolinea RepoAerolinea;


        public CUModificarAerolinea(IRepositorioAerolinea repo)
        {

            RepoAerolinea = repo;
        }
        public void Modificar(AerolineaDTO dto)
        {
            Aerolinea aerolineaExistente = RepoAerolinea.FindById(dto.Id);

            if (aerolineaExistente == null)
            {
                throw new InvalidOperationException($"No se encontró la aerolínea con Id {dto.Id}.");
            }
            aerolineaExistente.Nombre = dto.Nombre;
            aerolineaExistente.PaginaWeb = dto.PaginaWeb;



            if (!RepoAerolinea.Existe(aerolineaExistente))
            {
                RepoAerolinea.Update(aerolineaExistente);
            }
            else
            {
                throw new InvalidOperationException("La aerolínea ya existe");
            }

        }
    }
}
