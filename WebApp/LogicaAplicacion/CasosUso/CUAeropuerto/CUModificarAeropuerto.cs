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
    public class CUModificarAeropuerto : IModificarAeropuerto
    {

        private readonly IRepositorioAeropuerto RepoAeropuerto;
        private readonly IRepositorioPais RepoPais;
        private readonly IRepositorioCiudad RepoCiudad;

        public CUModificarAeropuerto(IRepositorioAeropuerto repo, IRepositorioPais repoPais, IRepositorioCiudad repoCiudad)
        {

            RepoAeropuerto = repo;
            RepoPais = repoPais;
            RepoCiudad = repoCiudad;
        }

        public void Modificar(AeropuertoDTO dto)
        {
            Aeropuerto aeropuertoExistente = RepoAeropuerto.FindById(dto.Id);

            if (aeropuertoExistente == null)
            {
                throw new InvalidOperationException($"No se encontró el aeropuerto con Id {dto.Id}.");


            }

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
            aeropuertoExistente.Nombre = dto.Nombre;
            aeropuertoExistente.Direccion = dto.Direccion;
            aeropuertoExistente.PaisId = dto.PaisId;
            aeropuertoExistente.CiudadId = dto.CiudadId;
            aeropuertoExistente.PaginaWeb = dto.PaginaWeb;



            if (!RepoAeropuerto.Existe(aeropuertoExistente))
            {
                RepoAeropuerto.Update(aeropuertoExistente);
            }
            else
            {
                throw new InvalidOperationException("El aeropuerto ya existe");
            }


        }
    }
}
