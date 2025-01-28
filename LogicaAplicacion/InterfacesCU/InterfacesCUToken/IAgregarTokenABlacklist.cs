using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCU.InterfacesCUToken
{
    public interface IAgregarTokenABlacklist
    {
        void Agregar(TokenDTO token);
    }
}
