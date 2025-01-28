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
    public class RepositorioAerolinea : IRepositorioAerolinea
    {

        private readonly EContext Contexto;
        public RepositorioAerolinea(EContext ctx)
        {
            Contexto = ctx;
        }

        public void Add(Aerolinea obj)
        {
            if (obj != null)
            {
                obj.Validar();
                Contexto.Aerolineas.Add(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se provee información de la aerolínea");
            }
        }

        public void Remove(Aerolinea obj)
        {
            Aerolinea aBorrar = Contexto.Aerolineas.Find(obj.Id);
            if (aBorrar != null)
            {
                Contexto.Remove(aBorrar);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No existe la aerolínea a borrar");
            }
        }

        public void Update(Aerolinea obj)
        {
            if (obj != null)
            {

                obj.Validar();
                Contexto.Aerolineas.Update(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se puede modificar una aerolínea inexistente");
            }

        }

        public IEnumerable<Aerolinea> FindAll()
        {
            return Contexto.Aerolineas
                  .ToList();
        }
        public Aerolinea FindById(int id)
        {

            return Contexto.Aerolineas
                 .FirstOrDefault(a => a.Id == id);


        }
        public bool Existe(Aerolinea aero)
        {

            bool existe = Contexto.Aerolineas
                         .Any(a => a.Nombre == aero.Nombre);
            if (existe)
            {
                throw new InvalidOperationException("Ya existe una aerolínea con ese nombre");
            }

            return existe;


        }

        public IEnumerable<Aerolinea> AerolineasPorNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return Contexto.Aerolineas.ToList();
            }

            return Contexto.Aerolineas
                           .Where(a => a.Nombre.Contains(nombre))
                           .ToList();
        }
    }
}
