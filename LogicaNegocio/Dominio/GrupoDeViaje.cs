using LogicaNegocio.InterfacesDominio;
using ExcepcionesPropias;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogicaNegocio.Dominio
{
    public class GrupoDeViaje : IValidable
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

   
        public List<Pais> PaisesDestino { get; set; } = new List<Pais>();

         public List<Ciudad> CiudadesDestino { get; set; } = new List<Ciudad>();

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public EstadoGrupoDeViaje EstadoGrupo { get; set; }

        public int? CoordinadorId { get; set; }
        public Usuario Coordinador { get; set; }
        public List<Usuario>? Viajeros { get; set; }= new List<Usuario>();

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new GrupoDeViajeException("El nombre del grupo de viaje es requerido.");
            }
            if (PaisesDestino.Count == 0) 
            {
                throw new GrupoDeViajeException("Debe seleccionar al menos un país de destino.");
            }
            if (CiudadesDestino.Count == 0) 
            {
                throw new GrupoDeViajeException("Debe seleccionar al menos una ciudad de destino.");
            }
            if (FechaInicio >= FechaFin)
            {
                throw new GrupoDeViajeException("La fecha de inicio debe ser anterior a la fecha de fin.");
            }
        }

        
    }
}
