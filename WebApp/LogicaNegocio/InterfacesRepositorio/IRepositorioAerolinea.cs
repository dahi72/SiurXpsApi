using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioAerolinea : IRepositorio<Aerolinea>
    {
        bool Existe(Aerolinea hotel);
        IEnumerable<Aerolinea> AerolineasPorNombre(string nombre);
    }
}
