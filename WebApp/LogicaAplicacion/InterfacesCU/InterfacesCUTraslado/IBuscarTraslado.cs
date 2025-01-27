using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCU.InterfacesCUTraslado
{
    public interface IBuscarTraslado
    {
       TrasladoDTO Buscar(int id);
    }
}
