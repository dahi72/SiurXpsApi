using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCU.InterfacesCUAeropuerto
{
    public interface IModificarAeropuerto
    {
        void Modificar(AeropuertoDTO dto);
    }
}
