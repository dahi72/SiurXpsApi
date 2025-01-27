using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeAccesoDatos.Repositorios
{
    public class RepositorioActividad : IRepositorioActividad
    {
        private readonly EContext Contexto;
        public RepositorioActividad(EContext ctx)
        {
            Contexto = ctx;
        }
        public void Add(Actividad obj)
        {
            if (obj != null)
            {
                obj.Validar();
                Contexto.Actividades.Add(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se provee información de la actividad");
            }
        }

        public bool Existe(string nombre, string ubicacion)
        {
            return Contexto.Actividades.Any(a => a.Nombre == nombre && a.Ubicacion == ubicacion);
        }

        public IEnumerable<Actividad> FindAll()
        {
            return Contexto.Actividades
                 .ToList();
        }

        public Actividad FindById(int id)
        {
            return Contexto.Actividades
                .FirstOrDefault(a => a.Id == id);
        }

        public void Remove(Actividad obj)
        {
            Actividad aBorrar = Contexto.Actividades.Find(obj.Id);
            if (aBorrar != null)
            {
                Contexto.Remove(aBorrar);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No existe la actividad a borrar");
            }
        }

        public void Update(Actividad obj)
        {
            if (obj != null)
            {

                obj.Validar();
                Contexto.Actividades.Update(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se puede modificar una actividad inexistente");
            }
        }

        public void InscribirUsuarioEnActividad(int actividadId, int usuarioId)
        {
            Actividad actividad = Contexto.Actividades
                .FirstOrDefault(a => a.Id == actividadId);

            if (actividad == null)
            {
                throw new InvalidOperationException("Actividad no encontrada.");
            }

            // Verificar si la actividad permite inscripción opcional
            if (actividad.Opcional)
            {
                Usuario usuario = Contexto.Usuarios
                    .FirstOrDefault(u => u.Id == usuarioId);

                if (usuario == null)
                {
                    throw new InvalidOperationException("Usuario no encontrado.");
                }

                // Verificar si el usuario ya está inscrito en la actividad
                if (usuario.Actividades != null && usuario.Actividades.Any(a => a.Id == actividadId))
                {
                    throw new InvalidOperationException("El usuario ya está inscrito en esta actividad.");
                }

                // Inscribir al usuario
                if (usuario.Actividades == null)
                {
                    usuario.Actividades = new List<Actividad>();
                }
                usuario.Actividades.Add(actividad);

                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("La actividad no permite inscripción opcional.");
            }
        }
    }
}
