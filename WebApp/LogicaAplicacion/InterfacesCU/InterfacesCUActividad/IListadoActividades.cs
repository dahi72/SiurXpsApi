using DTOs;
using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCU.InterfacesCUActividad
{
    public interface IListadoActividades
    {
        IEnumerable<ActividadDTO> Listado();
    }
}
