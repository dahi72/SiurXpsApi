using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public class CiudadException : Exception
    {
        public CiudadException()
        {
        }

        public CiudadException(string? message) : base(message)
        {
        }

        public CiudadException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

       
    }
}
