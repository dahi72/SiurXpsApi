using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public class ActividadException : Exception
    {
        public ActividadException()
        {
        }

        public ActividadException(string? message) : base(message)
        {
        }

        public ActividadException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
