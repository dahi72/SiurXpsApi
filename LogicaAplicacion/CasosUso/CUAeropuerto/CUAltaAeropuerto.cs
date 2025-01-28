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
    public class CUAltaAeropuerto : IAltaAeropuerto
    {
        private readonly IRepositorioAeropuerto RepoAeropuerto;
        private readonly IRepositorioPais RepoPais;
        private readonly IRepositorioCiudad RepoCiudad;

        public CUAltaAeropuerto(IRepositorioAeropuerto repo, IRepositorioPais repoPais, IRepositorioCiudad repoCiudad)
        {

            RepoAeropuerto = repo;
            RepoPais = repoPais;
            RepoCiudad = repoCiudad;
        }

        public void Alta(AeropuertoDTO dto)
        {
            Pais pais = RepoPais.FindById(dto.PaisId);
            if (pais == null)
            {
                throw new InvalidOperationException("El país especificado no existe.");
            }

            Ciudad ciudad = RepoCiudad.FindById(dto.CiudadId);
            if (ciudad == null)
            {
                throw new InvalidOperationException("La ciudad especificada no existe.");
            }

            // Validar que la ciudad pertenece al país
            if (ciudad.PaisCodigoIso != pais.CodigoIso)
            {
                throw new InvalidOperationException($"La ciudad '{ciudad.Nombre}' no pertenece al país '{pais.Nombre}'.");
            }

            Aeropuerto aero = new Aeropuerto
            {
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                PaisId = dto.PaisId,
                CiudadId = dto.CiudadId,
                PaginaWeb = dto.PaginaWeb
            };


            if (!RepoAeropuerto.Existe(aero))
            {
                RepoAeropuerto.Add(aero);
                dto.SetId(aero.Id);

            }
            else
            {
                throw new InvalidOperationException("El aerópuerto ya existe.");
            }
        }
    }
}
