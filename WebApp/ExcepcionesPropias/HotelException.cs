using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public class HotelException : Exception
    {
        public HotelException()
        {
        }

        public HotelException(string? message) : base(message)
        {
        }

        public HotelException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
