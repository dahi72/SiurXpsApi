using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCU.InterfacesCUItinerario
{
    public interface IModificarEvento
    {
        void ModificarEvento(int idItinerario, int idEvento, DateTime nuevoHorario);
    }
}
