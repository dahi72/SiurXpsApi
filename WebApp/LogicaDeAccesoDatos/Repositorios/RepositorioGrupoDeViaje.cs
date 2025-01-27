using ExcepcionesPropias;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeAccesoDatos.Repositorios
{
    public class RepositorioGrupoDeViaje : IRepositorioGrupoDeViaje
    {
        private readonly EContext Contexto;
        public RepositorioGrupoDeViaje(EContext ctx)
        {
            Contexto = ctx;
        }
        public void Add(GrupoDeViaje obj)
        {
            if (obj != null)
            {
                obj.Validar();

               

                Contexto.GruposDeViaje.Add(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se provee información del grupo");
            }
        }
    

        public void AgregarViajerosAlGrupo(int grupoId, List<Usuario> viajeros)
        {
            GrupoDeViaje grupo = FindById(grupoId);
            if (grupo == null)
            {
                throw new InvalidOperationException("Grupo de viaje no encontrado.");
            }

            foreach (Usuario viajero in viajeros)
            {
                grupo.Viajeros.Add(viajero);
            }

            Contexto.GruposDeViaje.Update(grupo);
            Contexto.SaveChanges();
        }

        public IEnumerable<GrupoDeViaje> FindAll()
        {
            throw new NotImplementedException();
        }

        public GrupoDeViaje FindById(int id)
        {
            return Contexto.GruposDeViaje
                .Include(g => g.PaisesDestino)
                .Include(g => g.CiudadesDestino)
                .Include(g => g.Coordinador)
                .Include(g => g.Viajeros)
                .FirstOrDefault(g => g.Id == id);
               
        }

      

        public void Remove(GrupoDeViaje obj)
        {
            GrupoDeViaje grupo = Contexto.GruposDeViaje.FirstOrDefault(g => g.Id == obj.Id);
            if (grupo == null)
            {
                throw new GrupoDeViajeException("El grupo de viaje no existe.");
            }

            // Verificar si el grupo tiene un itinerario asociado
            bool tieneItinerarioAsociado = Contexto.Itinerarios.Any(i => i.GrupoDeViajeId == obj.Id);
            if (tieneItinerarioAsociado)
            {
                throw new GrupoDeViajeException("No se puede eliminar el grupo porque está asociado a un itinerario.");
            }

            // Verificar si el estado del grupo es 'En Curso'
            if (grupo.EstadoGrupo == EstadoGrupoDeViaje.EnCurso)
            {
                throw new GrupoDeViajeException("No se puede eliminar el grupo porque su estado es 'En Curso'.");
            }

            // Eliminar el grupo si pasa las restricciones
            Contexto.GruposDeViaje.Remove(grupo);
            Contexto.SaveChanges();
        }

        public void Update(GrupoDeViaje obj)
        {
            if (obj == null)
            {
                throw new InvalidOperationException("No se provee información del grupo");
            }

            obj.Validar(); // Puedes validar de nuevo si es necesario
            Contexto.GruposDeViaje.Update(obj);
            Contexto.SaveChanges();
        }

        public bool Existe(GrupoDeViaje grupo)
        {
           
                bool existeGrupo = Contexto.GruposDeViaje
                             .Any(g => g.Nombre == grupo.Nombre);
                if (existeGrupo)
                {
                    throw new InvalidOperationException("Ya existe un grupo con ese nombre");
                }

                return existeGrupo;


        }

        
            public void EliminarViajeroDeGrupoDeViaje(int grupoId, int usuId)
            {
                var grupo = Contexto.GruposDeViaje
                            .Include(g => g.Viajeros)
                            .FirstOrDefault(g => g.Id == grupoId);
                if (grupo == null)
                {
                    throw new InvalidOperationException("Grupo de viaje no encontrado.");
                }

                 var viajero = grupo.Viajeros.FirstOrDefault(v => v.Id == usuId);
                if (viajero == null)
                {
                    throw new InvalidOperationException("Viajero no encontrado en el grupo de viaje.");
                }

                    grupo.Viajeros.Remove(viajero);
                    Contexto.Usuarios.Remove(viajero);
                    Contexto.SaveChanges();
            }

        public IEnumerable<GrupoDeViaje> GetGruposDeViajeDeCoordinador(int coordinadorId)
        {
            return Contexto.GruposDeViaje
                .Include(g => g.PaisesDestino)
                .Include(g => g.CiudadesDestino)
                .Include(g => g.Viajeros)
                 .Where(g=>g.CoordinadorId == coordinadorId)
                 .ToList();
        }
    }
}
