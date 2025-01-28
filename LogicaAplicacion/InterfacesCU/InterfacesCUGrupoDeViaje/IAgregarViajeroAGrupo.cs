using System;
using DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCU.InterfaesCUGrupoDeViaje
{
    public interface IAgregarViajeroAGrupo
    {
        void AgregarViajero(int grupoId, AltaUsuarioDTO usu);
    }
}
