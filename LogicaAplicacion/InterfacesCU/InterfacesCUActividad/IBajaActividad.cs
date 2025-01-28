using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCU.InterfacesCUActividad
{
    public interface IBajaActividad
    {
        void Eliminar(ActividadDTO dto);
    }
}
