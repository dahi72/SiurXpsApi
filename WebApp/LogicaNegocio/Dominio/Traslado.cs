using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class Traslado : IValidable
    {
        public int Id { get; set; }

        public string LugarDeEncuentro { get; set; }
        public TimeOnly Horario { get; set; }

        public TipoDeTraslado TipoDeTraslado { get; set; }

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(LugarDeEncuentro))
            {
                throw new InvalidOperationException("El lugar de encuentro no puede ser nulo o vacío.");
            }

              if (Horario == default(TimeOnly))
            {
                throw new InvalidOperationException("El horario no puede ser el valor predeterminado.");
            }
            if (TipoDeTraslado==0)
            {
                throw new InvalidOperationException("El tipo de traslado no puede estar vacío");
            }


        }
    }
}