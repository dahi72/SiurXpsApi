using DTOs;
using Humanizer;
using LogicaAplicacion.InterfacesCU.InterfacesCUActividad;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUActividad
{
    public class CUListadoActividad : IListadoActividades
    {
        private readonly IRepositorioActividad RepoActividad;

        public CUListadoActividad(IRepositorioActividad repo)
        {
            RepoActividad = repo;

        }
        public IEnumerable<ActividadDTO> Listado()
        {
            return RepoActividad.FindAll().Select(a => new ActividadDTO
            {
                Id= a.Id,
                Nombre = a.Nombre,
                Descripcion = a.Descripcion,
                Ubicacion = a.Ubicacion,
                Duracion = a.Duracion,
                Opcional = a.Opcional,


            });
        }
    }
}
