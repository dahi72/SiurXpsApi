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
    public class RepositorioCiudad : IRepositorioCiudad
    {
        private readonly EContext Contexto;
        public RepositorioCiudad(EContext ctx)
        {
            Contexto = ctx;
        }
        public void Add(Ciudad obj)
        {
            Contexto.Ciudades.Add(obj);
            Contexto.SaveChanges();
        }

        public IEnumerable<Ciudad> FindAll()
        {
            return Contexto.Ciudades.ToList();
        }

        public Ciudad FindById(int id)
        {
            return Contexto.Ciudades.Find(id);
        }

        public IEnumerable<Ciudad> FindByPaisId(int paisId)
        {
            return Contexto.Ciudades.Where(c => c.PaisId == paisId).ToList();
        }

        public void Remove(Ciudad obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Ciudad obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ciudad> ObtenerCiudadPorPais(string codigoISO)
        {
            return Contexto.Ciudades
                .Where(c => c.PaisCodigoIso == codigoISO).ToList();
        }

        public IEnumerable<Ciudad> FindAllByIds(IEnumerable<int> ciudadesIds)
        {
            return Contexto.Ciudades
                   .Where(c => ciudadesIds.Contains(c.Id));
        }

        public void ValidarCiudadesConPais(IEnumerable<Ciudad> ciudades)
        {
            // Obtener todos los países desde el contexto (base de datos)
            IEnumerable<Pais> paises = Contexto.Paises.ToList();

            foreach (Ciudad ciudad in ciudades)
            {
                // Buscar el país correspondiente a la ciudad usando el código ISO
                // Pais pais = paises.FirstOrDefault(p => p.CodigoIso == ciudad.PaisCodigoIso);
                Pais pais = paises.FirstOrDefault(p => p.CodigoIso.Trim().ToUpper() == ciudad.PaisCodigoIso.Trim().ToUpper());

                // Si no se encuentra el país, lanzamos una excepción
                if (pais == null)
                {
                    throw new InvalidOperationException($"No se encontró el país con el código ISO {ciudad.PaisCodigoIso}.");
                }

                // Verificar que la ciudad pertenece al país correcto
                /* if (ciudad.PaisCodigoIso != pais.CodigoIso)
                 {
                     throw new InvalidOperationException($"La ciudad {ciudad.Nombre} no pertenece al país {pais.Nombre}.");
                 }*/
                if (!string.Equals(ciudad.PaisCodigoIso, pais.CodigoIso, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException($"La ciudad {ciudad.Nombre} no pertenece al país {pais.Nombre}.");
                }
            }
        }

    }
}



