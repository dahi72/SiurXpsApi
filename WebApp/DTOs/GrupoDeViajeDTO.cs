using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class GrupoDeViajeDTO
    {
        public int Id { get; private set; }
        public string Nombre { get; set; }

       
        public List<int> PaisesDestinoIds { get; set; } = new List<int>();
        //public List<string> PaisesNombre { get; set; }
        public List<int> CiudadesDestinoIds { get; set; } = new List<int>();
       // public List<string> CiudadesNombre { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int CoordinadorId { get; set; }
        public List<int>? ViajerosIds { get; set; } = new List<int>();

        public void SetId(int id)
        {
            Id = id;
        }
        public GrupoDeViajeDTO(int id)
        {
            Id = id;
        }
    }
}

