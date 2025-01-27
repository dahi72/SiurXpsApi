using ExcepcionesPropias;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class Aeropuerto : IValidable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int PaisId { get; set; } 
        public Pais Pais { get; set; } 
        public int CiudadId { get; set; }
        public Ciudad Ciudad { get; set; }
        public string PaginaWeb { get; set; }
        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new AeropuertoException("El nombre del aeropuerto es requerido");
            }
            if (string.IsNullOrEmpty(Direccion))
            {
                throw new AeropuertoException("La dirección del aeropuerto es requerido");
            }
            if (PaisId<0)
            {
                throw new AeropuertoException("El país del aeropuerto es requerido");
            }
            if (CiudadId<0)
            {
                throw new AeropuertoException("La ciudad del aeropuerto es requerido");
            }
        }
    }
}
