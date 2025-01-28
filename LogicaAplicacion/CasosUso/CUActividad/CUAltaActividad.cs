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
    public class CUAltaActividad : IAltaActividad
    {
        private readonly IRepositorioActividad RepoActividad;
    
        public CUAltaActividad(IRepositorioActividad repo)
        {
            RepoActividad = repo;
           
        }
        public void Alta(ActividadDTO dto)
        {
            Actividad act = new Actividad
            {
                Nombre = dto.Nombre,
                Descripcion =dto.Descripcion,
                Ubicacion =dto.Ubicacion,
                Duracion =dto.Duracion,
                Opcional =dto.Opcional,

    };


            if (!RepoActividad.Existe(act.Nombre,act.Ubicacion))
            {
                RepoActividad.Add(act);
                dto.SetId(act.Id);

            }
            else
            {
                throw new InvalidOperationException("La actividad ya existe.");
            }
        }
    }
}
