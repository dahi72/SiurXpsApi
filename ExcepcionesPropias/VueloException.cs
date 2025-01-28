using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public class VueloException : Exception
    {
        public VueloException()
        {
        }

        public VueloException(string? message) : base(message)
        {
        }

        public VueloException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        
    }
}
