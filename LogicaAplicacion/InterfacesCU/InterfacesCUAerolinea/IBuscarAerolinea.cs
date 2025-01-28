using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCU.InterfacesCUAerolinea
{
    public interface IBuscarAerolinea
    {
        AerolineaDTO Buscar(int id);
    }
}
