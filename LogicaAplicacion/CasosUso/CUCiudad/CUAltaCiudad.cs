using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUCiudad;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUCiudad
{
    public class CUAltaCiudad : IAltaCiudad
    {
        private readonly IRepositorioCiudad RepoCiudad;
        private readonly IRepositorioPais RepoPais;       

        public CUAltaCiudad(IRepositorioCiudad repo, IRepositorioPais repoPais)
        {
            RepoCiudad = repo;
            RepoPais = repoPais;
        }
      
        public void Alta(CiudadDTO c)
        {
            var pais = RepoPais.ObtenerPorCodigoIso(c.PaisCodigoIso).FirstOrDefault();

            if (pais == null)
            {
                throw new Exception($"El país con código ISO {c.PaisCodigoIso} no existe.");
            }

            Ciudad ciudad = new Ciudad()
            {
                Nombre = c.Nombre,
                PaisId = pais.Id,
                PaisCodigoIso = c.PaisCodigoIso 
            };

            RepoCiudad.Add(ciudad);
        }

    }

}