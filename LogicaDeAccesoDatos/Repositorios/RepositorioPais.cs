using LogicaDeAccesoDatos;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioPais : IRepositorioPais
    {
        private readonly EContext Contexto;
        public RepositorioPais(EContext ctx)
        {
            Contexto = ctx;
        }
        public void Add(Pais obj)
        {
            if (obj != null)
            {
                obj.Validar();

                Contexto.Paises.Add(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se provee información del Pais");
            }
        }
        public void Remove(Pais obj)
        {
            Contexto.Paises.Remove(obj);
            Contexto.SaveChanges();
        }
        public void Update(Pais obj)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Pais> FindAll()
        {
            return Contexto.Paises
                  .ToList();
        }
        public Pais FindById(int id)
        {
            return Contexto.Paises.Find(id);
        }

        public IEnumerable<Pais> ObtenerPorCodigoIso(string codigoIso)
        {
            return Contexto.Paises
                .Where(p => p.CodigoIso == codigoIso)
                .ToList();
        }

        public IEnumerable<Pais> FindAllByIds(IEnumerable<int> paisesIds)
        {
            return Contexto.Paises
                   .Where(p => paisesIds.Contains(p.Id));
        }   
    }
}