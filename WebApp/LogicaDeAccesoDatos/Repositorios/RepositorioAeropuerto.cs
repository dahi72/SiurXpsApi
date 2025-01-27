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
    public class RepositorioAeropuerto : IRepositorioAeropuerto
    {
         private readonly EContext Contexto;
        public RepositorioAeropuerto(EContext ctx)
        {
            Contexto = ctx;
        }

        public void Add(Aeropuerto obj)
        {
            if (obj != null)
            {
                obj.Validar();
                Contexto.Aeropuertos.Add(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se provee información de un aeropuerto");
            }
        }

        public void Remove(Aeropuerto obj)
        {
            Aeropuerto aBorrar = Contexto.Aeropuertos.Find(obj.Id);
            if (aBorrar != null)
            {
                Contexto.Remove(aBorrar);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No existe el aeropuerto a borrar");
            }
        }

        public void Update(Aeropuerto obj)
        {
            if (obj != null)
            {

                obj.Validar();
                Contexto.Aeropuertos.Update(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se puede modificar un aeropuerto inexistente");
            }

        }

        public IEnumerable<Aeropuerto> FindAll()
        {
            return Contexto.Aeropuertos
                    .Include(a=>a.Pais)
                    .Include(a=>a.Ciudad)
                     .ToList();
        }
        public Aeropuerto FindById(int id)
        {

            return Contexto.Aeropuertos
                  .Include(a => a.Pais)
                  .Include(a => a.Ciudad)
                 .FirstOrDefault(a => a.Id == id);


        }
        public bool Existe(Aeropuerto aero)
        {

            bool existe = Contexto.Aeropuertos
                         .Any(a => a.Nombre == aero.Nombre);
            if (existe)
            {
                throw new InvalidOperationException("Ya existe un aeropuerto con ese nombre");
            }

            return existe;


        }
        public IEnumerable<Aeropuerto> AeropuertosPorNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return Contexto.Aeropuertos.ToList();
            }

            return Contexto.Aeropuertos
                           .Where(a => a.Nombre.Contains(nombre))
                           .ToList();
        }

    }
}
