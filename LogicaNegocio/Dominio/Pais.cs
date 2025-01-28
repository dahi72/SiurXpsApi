using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcepcionesPropias;
using LogicaNegocio.InterfacesDominio;

namespace LogicaNegocio.Dominio
{
    public class Pais : IValidable
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [MinLength(2)]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string Nombre { get; set; }
        public string CodigoIso { get; set; }
        public IEnumerable<Ciudad> Ciudades { get; set; }
        public IEnumerable<Aeropuerto> Aeropuertos { get; set; }
        public IEnumerable<Hotel> Hoteles { get; set; }


        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new PaisException("El nombre del país es requerido");
            }

            if (string.IsNullOrEmpty(CodigoIso))
            {
                throw new PaisException("El nombre del país es requerido");
            }

        }
    }
}
