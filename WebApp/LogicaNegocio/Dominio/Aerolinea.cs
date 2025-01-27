using ExcepcionesPropias;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class Aerolinea : IValidable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string PaginaWeb { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new AerolineaException("El nombre de la aerolínea es requerido");
            }
        }
    }



}
