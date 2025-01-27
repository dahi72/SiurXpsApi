using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUActividad;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicaAplicacion.CasosUso.CUActividad
{
    public class CUModificarActividad : IModificarActividad
    {
        private readonly IRepositorioActividad RepoActividad;

        public CUModificarActividad(IRepositorioActividad repo)
        {
            RepoActividad = repo;

        }
        public void Modificar(ActividadDTO dto)
        {
            Actividad actividadExistente = RepoActividad.FindById(dto.Id);

            if (actividadExistente == null)
            {
                throw new InvalidOperationException($"No se encontró la actividad con Id {dto.Id}.");
            }
            actividadExistente.Nombre = dto.Nombre;
            actividadExistente.Descripcion = dto.Descripcion;
            actividadExistente.Ubicacion = dto.Ubicacion;
            actividadExistente.Duracion = dto.Duracion;
            actividadExistente.Opcional = dto.Opcional;


            if (!RepoActividad.Existe(actividadExistente.Nombre,actividadExistente.Ubicacion))
            {
                RepoActividad.Update(actividadExistente);
            }
            else
            {
                throw new InvalidOperationException("La actividad ya existe");
            }
        }
    }
}
