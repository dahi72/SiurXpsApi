using ExcepcionesPropias;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class Ciudad 
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Clave foránea para relacionar con País
        public int PaisId { get; set; }
       public string PaisCodigoIso { get; set; }
        public Pais Pais { get; set; }
        public IEnumerable<Hotel> Hoteles { get; set; } = new List<Hotel>();
        public IEnumerable<Aeropuerto> Aeropuertos { get; set; } = new List<Aeropuerto>();



    }
}