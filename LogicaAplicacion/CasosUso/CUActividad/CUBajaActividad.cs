using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUActividad;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUActividad
{
    public class CUBajaActividad : IBajaActividad
    {
        private readonly IRepositorioActividad RepoActividad;

        public CUBajaActividad(IRepositorioActividad repo)
        {
            RepoActividad = repo;

        }

        public void Eliminar(ActividadDTO dto)
        {
            RepoActividad.Remove(new Actividad() { Id = dto.Id });
        }
    }
}
