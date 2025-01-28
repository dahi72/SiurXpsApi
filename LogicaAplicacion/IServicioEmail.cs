using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion
{
    public interface IServicioEmail
    {
        void EnviarEmail(string destinatario, string asunto, string mensaje);
    }
}
