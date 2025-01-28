using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class Actividad : IValidable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Ubicacion { get; set; }
        // si vamos a poner la actividad en un dia especifico no le tendriamos que poner doble la fecha
      //  public DateTime FechaInicio { get; set; }
        public int Duracion { get; set; }
        public bool Opcional { get; set; }

        public List<Usuario>? Usuarios { get; set; } 

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                throw new InvalidOperationException("El nombre de la actividad no puede ser nulo o vacío.");
            }

              if (string.IsNullOrEmpty(Descripcion))
            {
                throw new InvalidOperationException("La descripción de la actividad no puede ser nula o vacía.");
            }

            if (string.IsNullOrWhiteSpace(Ubicacion))
            {
                throw new InvalidOperationException("La ubicación de la actividad no puede ser nula o vacía.");
            }

               if (Duracion <= 0)
            {
                throw new InvalidOperationException("La duración de la actividad debe ser mayor a 0.");
            }

            
        }
    }
}
