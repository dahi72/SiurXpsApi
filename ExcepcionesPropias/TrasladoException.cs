using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public class TrasladoException : Exception
    {
        public TrasladoException()
        {
        }

        public TrasladoException(string? message) : base(message)
        {
        }

        public TrasladoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        
    }
}
