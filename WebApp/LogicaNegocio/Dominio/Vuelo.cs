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
    public class Vuelo : IValidable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public TimeOnly Horario { get; set; }
        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new VueloException("El nombre del hotel es requerido");
            }
            if (Horario == default)
            {
                throw new VueloException("El país en el que se encuentra hotel es requerido");
            }
        }
    }


}
