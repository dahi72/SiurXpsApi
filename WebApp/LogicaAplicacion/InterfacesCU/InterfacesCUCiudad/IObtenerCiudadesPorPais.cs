using LogicaNegocio.Dominio;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCU.InterfacesCUCiudad
{
    public interface IObtenerCiudadesPorPais
    {
        IEnumerable<CiudadDTO> ObtenerPais(string codigoISO);
    }
}
