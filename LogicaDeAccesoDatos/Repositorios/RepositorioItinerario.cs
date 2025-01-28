using ExcepcionesPropias;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicaDeAccesoDatos.Repositorios
{
    public class RepositorioItinerario : IRepositorioItinerario
    {
        private readonly EContext Contexto;
        public RepositorioItinerario(EContext ctx)
        {
            Contexto = ctx;
        }
        public void Add(Itinerario obj)
        {
            if (obj != null)
            {

                Contexto.Itinerarios.Add(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se provee información del Itinerario");
            }
        }

     

        public void AgregarEventoAItinerario(int idItinerario, EventoItinerario evento)
        {
            // Buscar el itinerario por su ID
            var itinerario = Contexto.Itinerarios.Find(idItinerario);
            if (itinerario == null)
            {
                throw new Exception("Itinerario no encontrado");
            }

            // Validar la fecha y hora
            if (evento.FechaYHora == default)
            {
                throw new Exception("La fecha y hora del evento es obligatoria");
            }

            // Asociar el tipo de evento según el ID correspondiente
            if (evento.ActividadId.HasValue)
            {
                evento.Actividad = Contexto.Actividades.Find(evento.ActividadId.Value);
            }
            if (evento.TrasladoId.HasValue)
            {
                evento.Traslado = Contexto.Traslados.Find(evento.TrasladoId.Value);
            }
            if (evento.AeropuertoId.HasValue)
            {
                evento.Aeropuerto = Contexto.Aeropuertos.Find(evento.AeropuertoId.Value);
            }
            if (evento.AerolineaId.HasValue)
            {
                evento.Aerolinea = Contexto.Aerolineas.Find(evento.AerolineaId.Value);
            }
            if (evento.HotelId.HasValue)
            {
                evento.Hotel = Contexto.Hoteles.Find(evento.HotelId.Value);
            }
            if (evento.VueloId.HasValue)
            {
                evento.Vuelo = Contexto.Vuelos.Find(evento.VueloId.Value);
            }

            // Agregar el evento al itinerario
            itinerario.Eventos.Add(evento);

            // Guardar los cambios en la base de datos
            Contexto.SaveChanges();
        }

        public void EliminarEvento(int idItinerario, int idEvento)
        {
            Itinerario itinerario = Contexto.Itinerarios
                                   .Include(i => i.Eventos)
                                    .FirstOrDefault(i => i.Id == idItinerario);

            if (itinerario == null)
            {
                throw new Exception("Itinerario no encontrado");
            }

            var eventoAEliminar = itinerario.Eventos?.FirstOrDefault(e => e.Id == idEvento);
            if (eventoAEliminar == null)
            {
                throw new Exception("Evento no encontrado en el itinerario");
            }

            itinerario.Eventos.Remove(eventoAEliminar);

            Contexto.SaveChanges();
        }

        public bool Existe(Itinerario iti)
        {
            bool existeIti = Contexto.Itinerarios
                         .Any(i => i.GrupoDeViajeId == iti.GrupoDeViajeId
                          && i.FechaInicio == iti.FechaInicio
                         && i.FechaFin == iti.FechaFin);
            if (existeIti)
            {
                throw new InvalidOperationException("Ya existe un itinerario con esas características");
            }


            return existeIti;


        }

        public bool ExisteGrupoEnItinerario(int idGrupo)
        {
            return Contexto.Itinerarios
                 .Any(i => i.GrupoDeViajeId == idGrupo);
        }

        public IEnumerable<Itinerario> FindAll()
        {
            return Contexto.Itinerarios
         .Include(i => i.Grupo) 
         .Include(i => i.Eventos) 
           .ToList();
        }

        
        public Itinerario FindById(int id)
        {

            Itinerario itinerario = Contexto.Itinerarios
              .Include(i => i.Grupo) 
              .Include(i => i.Eventos) 
              .FirstOrDefault(i => i.Id == id);

            if (itinerario == null)
            {
                throw new UsuarioException($"No se encontró el itinerario con ID {id}");
            }

            return itinerario;
        }

        public void ModificarHorarioEvento(int idItinerario, int idEvento, DateTime nuevoHorario)
        {
            // Cargar el itinerario junto con los eventos
            Itinerario itinerario = Contexto.Itinerarios
                .Include(i => i.Eventos)
                .FirstOrDefault(i => i.Id == idItinerario);

            if (itinerario == null)
            {
                throw new Exception("Itinerario no encontrado");
            }

            // Buscar el evento en el itinerario
            EventoItinerario eventoExistente = itinerario.Eventos?.FirstOrDefault(e => e.Id == idEvento);

            if (eventoExistente == null)
            {
                throw new Exception("Evento no encontrado en el itinerario");
            }

            // Validar que el nuevo horario esté dentro del rango del itinerario
            if (nuevoHorario < itinerario.FechaInicio || nuevoHorario > itinerario.FechaFin)
            {
                throw new Exception("El nuevo horario no está dentro del rango del itinerario");
            }

            // Actualizar el horario del evento
            eventoExistente.FechaYHora = nuevoHorario;

            // Guardar los cambios en la base de datos
            Contexto.SaveChanges();
        }

        public void Remove(Itinerario obj)
        {
            Itinerario itinerarioExistente = Contexto.Itinerarios.Find(obj.Id);
            if (itinerarioExistente == null)
            {
                throw new InvalidOperationException("El itinerario no existe en la base de datos.");
            }
            if (itinerarioExistente.FechaInicio >= DateTime.Now || itinerarioExistente.FechaFin >= DateTime.Now)
            {
                throw new InvalidOperationException("No se puede eliminar un itinerario con fecha futura o en curso.");
            }
            Contexto.Itinerarios.Remove(itinerarioExistente);
            Contexto.SaveChanges();
        }

        public void Update(Itinerario obj)
        {
            if (obj != null)
            {

                Contexto.Itinerarios.Update(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("No se puede modificar una itinerario inexistente");
            }

        }

       
            public IEnumerable<EventoItinerario> ObtenerEventosDeItinerario(int idItinerario)
            {
                var eventos = Contexto.Eventos
                   .Include(e => e.Actividad) 
                    .Include(e => e.Traslado)
                    .Include(e => e.Aeropuerto) 
                    .Include(e => e.Aerolinea) 
                    .Include(e => e.Hotel) 
                    .Include(e => e.Vuelo) 
                    .Where(e => e.ItinerarioId == idItinerario) 
                    .ToList();

                if (!eventos.Any())
                {
                    throw new Exception("No se encontraron eventos para el itinerario especificado.");
                }

                return eventos;
            }

        public EventoItinerario ObtenerEventoDeItinerario(int idItinerario, int idEvento)
        {
            var evento = Contexto.Eventos
                .Include(e => e.Actividad)
                .Include(e => e.Traslado) 
                .Include(e => e.Aeropuerto) 
                .Include(e => e.Aerolinea) 
                .Include(e => e.Hotel) 
                .Include(e => e.Vuelo) 
                .FirstOrDefault(e => e.ItinerarioId == idItinerario && e.Id == idEvento); // Filtrar por itinerario y evento

            if (evento == null)
            {
                throw new Exception($"No se encontró el evento con ID {idEvento} en el itinerario con ID {idItinerario}.");
            }

            return evento;
        }
    }




    }



