using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUActividad;
using LogicaAplicacion.InterfacesCU.InterfaesCUGrupoDeViaje;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUActividad
{
    public class CUBuscarActividad : IBuscarActividad
    {
        private readonly IRepositorioActividad RepoActividad;

        public CUBuscarActividad(IRepositorioActividad repo)
        {
            RepoActividad = repo;

        }
   
       public ActividadDTO Buscar(int id)
        {
            ActividadDTO dtoAct = null;

            Actividad actividad = RepoActividad.FindById(id);
            if (actividad != null)
            {

                dtoAct = new ActividadDTO()
                {
                    Id = actividad.Id,
                    Nombre=actividad.Nombre,
                    Duracion=actividad.Duracion,
                    Descripcion=actividad.Descripcion,
                    Ubicacion=  actividad.Ubicacion,
                    Opcional=actividad.Opcional,

                };

            }
            return dtoAct;
        }
    }
}
