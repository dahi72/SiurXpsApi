using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioGrupoDeViaje:  IRepositorio<GrupoDeViaje>
    {
        void AgregarViajerosAlGrupo(int grupoId, List<Usuario> viajeros);
        bool Existe(GrupoDeViaje grupo);
        void EliminarViajeroDeGrupoDeViaje(int grupoId, int usuId);
        IEnumerable<GrupoDeViaje> GetGruposDeViajeDeCoordinador(int coordinadorId);
    
    }
}
