using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCU.InterfacesCUItinerario
{
    public interface IModificarGrupoDeItinerario
    {
        void Modificar(int idItinerario,int idGrupo);
    }
}
