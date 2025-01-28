using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public class AeropuertoException : Exception
    {
        public AeropuertoException()
        {
        }

        public AeropuertoException(string? message) : base(message)
        {
        }

        public AeropuertoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

  
    }
}
