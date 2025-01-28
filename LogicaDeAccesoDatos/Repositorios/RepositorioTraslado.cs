using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeAccesoDatos.Repositorios
{
    public class RepositorioTraslado : IRepositorioTraslado
    {
        private readonly EContext Contexto;
        public RepositorioTraslado(EContext ctx)
        {
            Contexto = ctx;
        }
        public void Add(Traslado obj)
        {
            if (obj != null)
            {
                obj.Validar();
                Contexto.Traslados.Add(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se provee información del traslado");
            }
        }

        public IEnumerable<Traslado> FindAll()
        {
            return Contexto.Traslados
                .ToList();
        }

        public Traslado FindById(int id)
        {
            return Contexto.Traslados
                .FirstOrDefault(a => a.Id == id);
        }

        public void Remove(Traslado obj)
        {
            Traslado aBorrar = Contexto.Traslados.Find(obj.Id);
            if (aBorrar != null)
            {
                Contexto.Remove(aBorrar);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No existe el traslado a borrar");
            }
        }

        public void Update(Traslado obj)
        {
            if (obj != null)
            {

                obj.Validar();
                Contexto.Traslados.Update(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se puede modificar un traslado inexistente");
            }
        }
    }
}
