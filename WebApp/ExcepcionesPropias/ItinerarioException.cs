using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public class ItinerarioException : Exception
    {
        public ItinerarioException()
        {
        }

        public ItinerarioException(string? message) : base(message)
        {
        }

        public ItinerarioException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

           }
}
