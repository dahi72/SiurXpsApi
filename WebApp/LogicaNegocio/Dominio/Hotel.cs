using ExcepcionesPropias;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class Hotel : IValidable
    {
        [Key]
        public int Id { get; set; }

       [Required(ErrorMessage = "El nombre del hotel es obligatorio.")]
       [StringLength(100, ErrorMessage = "El nombre del hotel no puede tener más de 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200, ErrorMessage = "La dirección no puede tener más de 200 caracteres.")]
            public string Direccion { get; set; }

        [Required(ErrorMessage = "El país es obligatorio.")]
        public int PaisId { get; set; }  
        public Pais Pais { get; set; }   

      
     [Required(ErrorMessage = "La ciudad es obligatoria.")]
        public int CiudadId { get; set; } 
        public Ciudad Ciudad { get; set; }  

      [Required(ErrorMessage = "La hora de Check-In es obligatoria.")]
        public TimeSpan CheckIn { get; set; }

       [Required(ErrorMessage = "La hora de Check-Out es obligatoria.")]
        public TimeSpan CheckOut { get; set; }

        public string? PaginaWeb { get; set; }
 

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new HotelException("El nombre del hotel es requerido");
            }
            if (PaisId<0)
            {
                throw new HotelException("El país en el que se encuentra hotel es requerido");
            }
            if (CiudadId < 0)
            {
                throw new HotelException("La ciudad en el que se encuentra hotel es requerido");
            }
            if (CheckIn == default)
            {
                throw new HotelException("El horario de check-in es requerido");
            }
            if (CheckOut == default)
            {
                throw new HotelException("El horario de check-out es requerido");
            }
        }
    }
}
