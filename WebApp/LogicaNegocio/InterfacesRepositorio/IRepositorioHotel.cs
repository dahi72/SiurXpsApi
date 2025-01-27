using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Dominio;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioHotel : IRepositorio<Hotel>   
    {
         bool Existe(Hotel hotel);
        IEnumerable<Hotel> HotelesPorPaisyCiudad(string nombre, string codigoIso, string ciudad);
       
    }
}
