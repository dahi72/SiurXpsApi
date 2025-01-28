using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioPais : IRepositorio<Pais>
    {
        IEnumerable<Pais> ObtenerPorCodigoIso(string codigoIso);
        IEnumerable<Pais> FindAllByIds(IEnumerable<int> paisesIds);
    }
}
