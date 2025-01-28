using DTOs;
using Humanizer;
using LogicaAplicacion.InterfacesCU.InterfaesCUGrupoDeViaje;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Runtime.Intrinsics.Wasm;

namespace LogicaAplicacion.CasosUso.CUGrupoDeViaje
{
    public class CUAltaGrupoDeViaje : IAltaGrupo
    {
        private readonly IRepositorioGrupoDeViaje RepoGrupo;
        private readonly IRepositorioPais RepoPais;  
        private readonly IRepositorioCiudad RepoCiudad;
        private readonly IRepositorioUsuario RepoUsuarios;

        public CUAltaGrupoDeViaje(IRepositorioGrupoDeViaje repoGrupo, IRepositorioPais repoPais, IRepositorioCiudad repoCiudad, IRepositorioUsuario repoUsuarios)
        {
            RepoGrupo = repoGrupo;
            RepoPais = repoPais;
            RepoCiudad = repoCiudad;
            RepoUsuarios = repoUsuarios;
        }

        public void Alta(GrupoDeViajeDTO grupo)
        {
            // Buscar el país y la ciudad en la base de datos
            IEnumerable<Pais> nuevoPais = RepoPais.FindAllByIds(grupo.PaisesDestinoIds);
            IEnumerable<Ciudad> nuevaCiudad = RepoCiudad.FindAllByIds(grupo.CiudadesDestinoIds);
            IEnumerable<Usuario> nuevoViajero = RepoUsuarios.FindAllByIds(grupo.ViajerosIds);

            RepoCiudad.ValidarCiudadesConPais(nuevaCiudad);

            /*  if (pais == null)
              {
                  throw new InvalidOperationException("El país de destino especificado no existe.");
              }
              if (ciudad == null || ciudad.PaisId != pais.Id)
              {
                  throw new InvalidOperationException("La ciudad de destino especificada no es válida para el país seleccionado.");
              }*/

            EstadoGrupoDeViaje estadoGrupo;

           

            DateTime hoy = DateTime.Today;
            if (grupo.FechaFin < hoy)
            {
                estadoGrupo = EstadoGrupoDeViaje.Pasado;
            }
            else if (grupo.FechaInicio <= hoy && grupo.FechaFin >= hoy)
            {
                estadoGrupo = EstadoGrupoDeViaje.EnCurso;
            }
            else
            {
                estadoGrupo = EstadoGrupoDeViaje.Proximo;
            }

            // Crear el grupo de viaje
            GrupoDeViaje g = new GrupoDeViaje()
            {
                Nombre = grupo.Nombre,
                PaisesDestino = nuevoPais.ToList(), 
                CiudadesDestino = nuevaCiudad.ToList(),
                FechaInicio = grupo.FechaInicio,
                FechaFin = grupo.FechaFin,
                CoordinadorId = grupo.CoordinadorId,
                EstadoGrupo = estadoGrupo,
                Viajeros= nuevoViajero.ToList(),
            };

            if (!RepoGrupo.Existe(g))
            {
                RepoGrupo.Add(g);
               grupo.SetId(g.Id);
            }
            else
            {
                throw new InvalidOperationException("Ya existe un grupo con ese nombre.");
            }
        }
    }
}
