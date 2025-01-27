using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public class GrupoDeViajeException : Exception
    {
        public GrupoDeViajeException()
        {
        }
        public GrupoDeViajeException(string? message) : base(message)
        {
        }
        public GrupoDeViajeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
