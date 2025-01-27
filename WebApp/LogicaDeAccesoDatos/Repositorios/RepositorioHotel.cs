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
    public class RepositorioHotel : IRepositorioHotel
    {
        private readonly EContext Contexto;
        public RepositorioHotel(EContext ctx)
        {
            Contexto = ctx;
        }

        public void Add(Hotel obj)
        {
            if (obj != null)
            {
                obj.Validar();
                Contexto.Hoteles.Add(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se provee información del hotel");
            }
        }

        public void Remove(Hotel obj)
        {
            Hotel aBorrar = Contexto.Hoteles.Find(obj.Id);
            if (aBorrar != null)
            {
                Contexto.Remove(aBorrar);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No existe el hotel a borrar");
            }
        }

        public void Update(Hotel obj)
        {
            if (obj != null)
            {

                obj.Validar();
                Contexto.Hoteles.Update(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se puede modificar un hotel inexistente");
            }

        }

        public IEnumerable<Hotel> FindAll()
        {
            return Contexto.Hoteles
             .Include(h => h.Pais)
             .Include(h => h.Ciudad)
             .ToList();
        }
        public Hotel FindById(int id)
        {

            return Contexto.Hoteles
                   .Include(h => h.Pais)
                   .Include(h => h.Ciudad)
                   .FirstOrDefault(h => h.Id == id);


        }

        public bool Existe(Hotel hotel)
        {
            bool existeHotel = Contexto.Hoteles
                               .Any(h => h.Nombre == hotel.Nombre &&
                                h.PaisId == hotel.PaisId &&
                                h.CiudadId == hotel.CiudadId);

            if (existeHotel)
            {
                throw new InvalidOperationException("Ya existe un hotel con el mismo nombre, país y ciudad.");
            }

            return existeHotel;
        }

        public IEnumerable<Hotel> HotelesPorPaisyCiudad(string nombre, string codigoIso, string ciudad)
        {
            var hoteles = Contexto.Hoteles.AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
            {
                hoteles = hoteles.Where(h => h.Nombre.Contains(nombre));
            }

            if (!string.IsNullOrEmpty(codigoIso))
            {
                hoteles = hoteles.Join(Contexto.Paises,
                                       h => h.PaisId,
                                       p => p.Id,
                                       (h, p) => new { Hotel = h, Pais = p })
                                 .Where(h => h.Pais.CodigoIso == codigoIso)
                                 .Select(h => h.Hotel);
            }

            if (!string.IsNullOrEmpty(ciudad))
            {
                hoteles = hoteles.Join(Contexto.Ciudades,
                                       h => h.CiudadId,
                                       c => c.Id,
                                       (h, c) => new { Hotel = h, Ciudad = c })
                                 .Where(h => h.Ciudad.Nombre.ToLower() == ciudad.ToLower())
                                 .Select(h => h.Hotel);
            }

            return hoteles.ToList();
        }




    }
}
