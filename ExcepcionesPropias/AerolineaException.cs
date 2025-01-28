using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public class AerolineaException : Exception
    {
        public AerolineaException()
        {
        }

        public AerolineaException(string? message) : base(message)
        {
        }

        public AerolineaException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
