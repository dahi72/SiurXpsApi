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
    public class RepositorioVuelo : IRepositorioVuelo
    {
        private readonly EContext Contexto;
        public RepositorioVuelo(EContext ctx)
        {
            Contexto = ctx;
        }
        public void Add(Vuelo obj)
        {
            if (obj != null)
            {
                obj.Validar();
                Contexto.Vuelos.Add(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se provee información del hotel");
            }
        }

        public void Remove(Vuelo obj)
        {
            Vuelo aBorrar = Contexto.Vuelos.Find(obj.Id);
            if (aBorrar != null)
            {
                Contexto.Remove(aBorrar);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No existe el vuelo a borrar");
            }
        }

        public void Update(Vuelo obj)
        {
            if (obj != null)
            {

                obj.Validar();
                Contexto.Vuelos.Update(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se puede modificar un vuelo inexistente");
            }

        }

        public IEnumerable<Vuelo> FindAll()
        {
            return Contexto.Vuelos
                  .ToList();
        }
        public Vuelo FindById(int id)
        {

            return Contexto.Vuelos
                  .FirstOrDefault(v => v.Id == id);


        }

        public bool Existe(Vuelo vuelo)
        {
            bool existeVuelo = Contexto.Vuelos
                               .Any(v => v.Nombre == vuelo.Nombre && v.Horario ==vuelo.Horario);          // TODO: Verificar con cliente que esto sea asi                

            if (existeVuelo)
            {
                throw new InvalidOperationException("Ya existe un vuelo con el mismo nombre.");
            }

            return existeVuelo;
        }
        public IEnumerable<Vuelo> VuelosPorNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return Contexto.Vuelos.ToList();
            }

            return Contexto.Vuelos
                           .Where(v => v.Nombre.Contains(nombre))
                           .ToList();
        }
    }
}