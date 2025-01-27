using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio;

namespace LogicaAplicacion.InterfacesCU.InterfacesCUToken
{
    public interface IEstaRevocado
    {
        bool IsRevocado(TokenRevocado obj);
    }
}
