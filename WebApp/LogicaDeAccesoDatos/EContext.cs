using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LogicaNegocio.Dominio;
using LogicaNegocio;
namespace LogicaDeAccesoDatos
{
    public class EContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Aerolinea> Aerolineas { get; set; }
        public DbSet<Aeropuerto> Aeropuertos { get; set; }
        public DbSet<GrupoDeViaje> GruposDeViaje { get; set; }
        public DbSet<Hotel> Hoteles { get; set; }
        public DbSet<Itinerario> Itinerarios { get; set; }
        public DbSet<Traslado> Traslados { get; set; }
        public DbSet<Vuelo> Vuelos { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<TokenRevocado> TokensRevocados { get; set; }

        public DbSet<EventoItinerario> Eventos { get; set; }


        public EContext(DbContextOptions<EContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Usuario>()
       .HasMany(g => g.GruposComoViajero)
       .WithMany(g => g.Viajeros)
       .UsingEntity(j => j.ToTable("GrupoDeViajeUsuarios"));*/

            modelBuilder.Entity<GrupoDeViaje>()
        .HasOne(g => g.Coordinador)
        .WithMany(u => u.GruposComoCoordinador)
        .HasForeignKey(g => g.CoordinadorId)
        .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada

            // Relación GrupoDeViaje - Viajeros
            modelBuilder.Entity<GrupoDeViaje>()
                .HasMany(g => g.Viajeros)
                .WithMany(u => u.GruposComoViajero)
                .UsingEntity<Dictionary<string, object>>(
                    "GrupoDeViajeViajero",
                    j => j.HasOne<Usuario>().WithMany().HasForeignKey("ViajeroId"),
                    j => j.HasOne<GrupoDeViaje>().WithMany().HasForeignKey("GrupoDeViajeId")
                );

            /* modelBuilder.Entity<Usuario>()
                 .HasMany(u => u.GruposComoCoordinador)
                 .WithOne(g => g.Coordinador)
                 .HasForeignKey(g => g.CoordinadorId)
                 .OnDelete(DeleteBehavior.Restrict);*/

            modelBuilder.Entity<GrupoDeViaje>()
                 .HasMany(g => g.PaisesDestino)
                 .WithMany()
                 .UsingEntity<Dictionary<string, object>>(
                     "GrupoDeViajePaisesDestino",
                     j => j.HasOne<Pais>().WithMany().HasForeignKey("PaisId"),
                     j => j.HasOne<GrupoDeViaje>().WithMany().HasForeignKey("GrupoDeViajeId")
                 );

            modelBuilder.Entity<GrupoDeViaje>()
                .HasMany(g => g.CiudadesDestino)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "GrupoDeViajeCiudadesDestino",
                    j => j.HasOne<Ciudad>().WithMany().HasForeignKey("CiudadId"),
                    j => j.HasOne<GrupoDeViaje>().WithMany().HasForeignKey("GrupoDeViajeId")
                );



            modelBuilder.Entity<Pais>()
                .HasMany(p => p.Ciudades)
                .WithOne(c => c.Pais)
                .HasForeignKey(c => c.PaisId)
                .OnDelete(DeleteBehavior.NoAction);  

            modelBuilder.Entity<Pais>()
                .HasMany(e => e.Hoteles)
                .WithOne(e => e.Pais)
                .HasForeignKey(c => c.PaisId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ciudad>()
                .HasMany(e => e.Hoteles)
                .WithOne(e => e.Ciudad)
                .HasForeignKey(c => c.CiudadId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Aeropuerto>()
                .HasOne(a => a.Pais)
                .WithMany(p => p.Aeropuertos)
                .HasForeignKey(a => a.PaisId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Aeropuerto>()
                .HasOne(a => a.Ciudad)
                .WithMany(c => c.Aeropuertos)
                .HasForeignKey(a => a.CiudadId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<EventoItinerario>()
            .HasOne(e => e.Aeropuerto)
            .WithMany()
            .HasForeignKey(e => e.AeropuertoId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EventoItinerario>()
                .HasOne(e => e.Actividad)
                .WithMany()
                .HasForeignKey(e => e.ActividadId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EventoItinerario>()
                .HasOne(e => e.Traslado)
                .WithMany()
                .HasForeignKey(e => e.TrasladoId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<EventoItinerario>()
                .HasOne(e => e.Vuelo)
                .WithMany()
                .HasForeignKey(e => e.VueloId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EventoItinerario>()
                .HasOne(e => e.Aerolinea)
                .WithMany()
                .HasForeignKey(e =>e.AerolineaId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
