using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioActividad : IRepositorio<Actividad>
    {
        bool Existe(string nombre, string ubicacion);
        void InscribirUsuarioEnActividad(int actividadId, int usuarioId);
    }
}
