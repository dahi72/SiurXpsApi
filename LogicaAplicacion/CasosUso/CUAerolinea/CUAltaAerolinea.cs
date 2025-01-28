using DTOs;
using Humanizer;
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
    public class CUAltaAerolinea : IAltaAerolinea
    {
        private readonly IRepositorioAerolinea RepoAerolinea;


        public CUAltaAerolinea (IRepositorioAerolinea repo)
        {

            RepoAerolinea = repo;
        }

        public void Alta(AerolineaDTO dto)
        {
            Aerolinea aero = new Aerolinea
            {
                Nombre = dto.Nombre,
                PaginaWeb = dto.PaginaWeb

            };


            if (!RepoAerolinea.Existe(aero))
            {
                RepoAerolinea.Add(aero);
                dto.SetId(aero.Id);

            }
            else
            {
                throw new InvalidOperationException("La aerolinea ya existe.");
            }
        }
    }
}
