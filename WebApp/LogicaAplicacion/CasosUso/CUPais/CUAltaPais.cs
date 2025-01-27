using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUPais;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUPais
{
    public class CUAltaPais : IAltaPais
    {
        private readonly IRepositorioPais RepoPais;

        public CUAltaPais(IRepositorioPais repo)
        {
            RepoPais = repo;
        }

        public void Alta(PaisDTO p)
        {


            Pais pais = new Pais()
            {

             
                Nombre = p.Nombre,
                CodigoIso= p.CodigoIso.ToLower(),
              


            };

            RepoPais.Add(pais);
            p.Id = pais.Id;
        }
    }


}
