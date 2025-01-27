using DTOs;
using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCU.InterfacesCUHotel
{
    public interface IFiltroHotelesPorPaisYCiudad
    {
        IEnumerable<HotelDTO> HotelesPorPaisyCiudad(string nombre, string codigoIso, string ciudad);
    }
}
