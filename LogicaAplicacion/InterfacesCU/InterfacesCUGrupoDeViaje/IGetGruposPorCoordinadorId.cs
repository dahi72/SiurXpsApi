using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCU.InterfaesCUGrupoDeViaje
{
    public interface IGetGruposPorCoordinadorId
    {
        IEnumerable<GrupoDeViajeDTO> GetGruposDeViajeCoordinadorId(int coordinadorId);
    }
}
