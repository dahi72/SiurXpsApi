using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Dominio;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioVuelo : IRepositorio<Vuelo>
    {
        bool Existe(Vuelo vuelo);
        IEnumerable<Vuelo> VuelosPorNombre(string nombre);
    }
}
