using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioCiudad : IRepositorio<Ciudad>
    {
        IEnumerable<Ciudad> ObtenerCiudadPorPais(string codigoISO);
        IEnumerable<Ciudad> FindAllByIds(IEnumerable<int> ciudadesIds);
        void ValidarCiudadesConPais(IEnumerable<Ciudad> ciudades);
    }
}
