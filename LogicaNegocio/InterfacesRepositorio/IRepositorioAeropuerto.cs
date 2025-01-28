using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioAeropuerto : IRepositorio<Aeropuerto>
    {
        bool Existe(Aeropuerto hotel);
        IEnumerable<Aeropuerto> AeropuertosPorNombre(string nombre);
    }
}
