﻿// <auto-generated />
using System;
using LogicaDeAccesoDatos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LogicaDeAccesoDatos.Migrations
{
    [DbContext(typeof(EContext))]
    [Migration("20250121023417_conUsuYGrupos")]
    partial class conUsuYGrupos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ActividadUsuario", b =>
                {
                    b.Property<int>("ActividadesId")
                        .HasColumnType("int");

                    b.Property<int>("UsuariosId")
                        .HasColumnType("int");

                    b.HasKey("ActividadesId", "UsuariosId");

                    b.HasIndex("UsuariosId");

                    b.ToTable("ActividadUsuario");
                });

            modelBuilder.Entity("GrupoDeViajeCiudadesDestino", b =>
                {
                    b.Property<int>("CiudadId")
                        .HasColumnType("int");

                    b.Property<int>("GrupoDeViajeId")
                        .HasColumnType("int");

                    b.HasKey("CiudadId", "GrupoDeViajeId");

                    b.HasIndex("GrupoDeViajeId");

                    b.ToTable("GrupoDeViajeCiudadesDestino");
                });

            modelBuilder.Entity("GrupoDeViajePaisesDestino", b =>
                {
                    b.Property<int>("GrupoDeViajeId")
                        .HasColumnType("int");

                    b.Property<int>("PaisId")
                        .HasColumnType("int");

                    b.HasKey("GrupoDeViajeId", "PaisId");

                    b.HasIndex("PaisId");

                    b.ToTable("GrupoDeViajePaisesDestino");
                });

            modelBuilder.Entity("GrupoDeViajeViajero", b =>
                {
                    b.Property<int>("GrupoDeViajeId")
                        .HasColumnType("int");

                    b.Property<int>("ViajeroId")
                        .HasColumnType("int");

                    b.HasKey("GrupoDeViajeId", "ViajeroId");

                    b.HasIndex("ViajeroId");

                    b.ToTable("GrupoDeViajeViajero");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Actividad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duracion")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Opcional")
                        .HasColumnType("bit");

                    b.Property<string>("Ubicacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Actividades");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Aerolinea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaginaWeb")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Aerolineas");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Aeropuerto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CiudadId")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaginaWeb")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaisId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CiudadId");

                    b.HasIndex("PaisId");

                    b.ToTable("Aeropuertos");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Ciudad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaisCodigoIso")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaisId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaisId");

                    b.ToTable("Ciudades");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.EventoItinerario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ActividadId")
                        .HasColumnType("int");

                    b.Property<int?>("AerolineaId")
                        .HasColumnType("int");

                    b.Property<int?>("AeropuertoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaYHora")
                        .HasColumnType("datetime2");

                    b.Property<int?>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("ItinerarioId")
                        .HasColumnType("int");

                    b.Property<int?>("TrasladoId")
                        .HasColumnType("int");

                    b.Property<int?>("VueloId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActividadId");

                    b.HasIndex("AerolineaId");

                    b.HasIndex("AeropuertoId");

                    b.HasIndex("HotelId");

                    b.HasIndex("ItinerarioId");

                    b.HasIndex("TrasladoId");

                    b.HasIndex("VueloId");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.GrupoDeViaje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CoordinadorId")
                        .HasColumnType("int");

                    b.Property<int>("EstadoGrupo")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CoordinadorId");

                    b.ToTable("GruposDeViaje");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("CheckIn")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("CheckOut")
                        .HasColumnType("time");

                    b.Property<int>("CiudadId")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PaginaWeb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaisId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CiudadId");

                    b.HasIndex("PaisId");

                    b.ToTable("Hoteles");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Itinerario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("GrupoDeViajeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GrupoDeViajeId");

                    b.ToTable("Itinerarios");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Pais", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodigoIso")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Paises");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Traslado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeOnly>("Horario")
                        .HasColumnType("time");

                    b.Property<string>("LugarDeEncuentro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoDeTraslado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Traslados");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("DebeCambiarContrasena")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaNac")
                        .HasColumnType("datetime2");

                    b.Property<string>("Pasaporte")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasaporteDocumentoRuta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PrimerApellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PrimerNombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SegundoApellido")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SegundoNombre")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SeguroDeViajeDocumentoRuta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VacunasDocumentoRuta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VisaDocumentoRuta")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Vuelo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeOnly>("Horario")
                        .HasColumnType("time");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vuelos");
                });

            modelBuilder.Entity("LogicaNegocio.TokenRevocado", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Expiracion")
                        .HasColumnType("datetime2");

                    b.HasKey("Token");

                    b.ToTable("TokensRevocados");
                });

            modelBuilder.Entity("ActividadUsuario", b =>
                {
                    b.HasOne("LogicaNegocio.Dominio.Actividad", null)
                        .WithMany()
                        .HasForeignKey("ActividadesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Dominio.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuariosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GrupoDeViajeCiudadesDestino", b =>
                {
                    b.HasOne("LogicaNegocio.Dominio.Ciudad", null)
                        .WithMany()
                        .HasForeignKey("CiudadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Dominio.GrupoDeViaje", null)
                        .WithMany()
                        .HasForeignKey("GrupoDeViajeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GrupoDeViajePaisesDestino", b =>
                {
                    b.HasOne("LogicaNegocio.Dominio.GrupoDeViaje", null)
                        .WithMany()
                        .HasForeignKey("GrupoDeViajeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Dominio.Pais", null)
                        .WithMany()
                        .HasForeignKey("PaisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GrupoDeViajeViajero", b =>
                {
                    b.HasOne("LogicaNegocio.Dominio.GrupoDeViaje", null)
                        .WithMany()
                        .HasForeignKey("GrupoDeViajeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Dominio.Usuario", null)
                        .WithMany()
                        .HasForeignKey("ViajeroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Aeropuerto", b =>
                {
                    b.HasOne("LogicaNegocio.Dominio.Ciudad", "Ciudad")
                        .WithMany("Aeropuertos")
                        .HasForeignKey("CiudadId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Dominio.Pais", "Pais")
                        .WithMany("Aeropuertos")
                        .HasForeignKey("PaisId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ciudad");

                    b.Navigation("Pais");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Ciudad", b =>
                {
                    b.HasOne("LogicaNegocio.Dominio.Pais", "Pais")
                        .WithMany("Ciudades")
                        .HasForeignKey("PaisId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Pais");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.EventoItinerario", b =>
                {
                    b.HasOne("LogicaNegocio.Dominio.Actividad", "Actividad")
                        .WithMany()
                        .HasForeignKey("ActividadId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("LogicaNegocio.Dominio.Aerolinea", "Aerolinea")
                        .WithMany()
                        .HasForeignKey("AerolineaId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("LogicaNegocio.Dominio.Aeropuerto", "Aeropuerto")
                        .WithMany()
                        .HasForeignKey("AeropuertoId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("LogicaNegocio.Dominio.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("HotelId");

                    b.HasOne("LogicaNegocio.Dominio.Itinerario", "Iti")
                        .WithMany("Eventos")
                        .HasForeignKey("ItinerarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Dominio.Traslado", "Traslado")
                        .WithMany()
                        .HasForeignKey("TrasladoId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("LogicaNegocio.Dominio.Vuelo", "Vuelo")
                        .WithMany()
                        .HasForeignKey("VueloId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Actividad");

                    b.Navigation("Aerolinea");

                    b.Navigation("Aeropuerto");

                    b.Navigation("Hotel");

                    b.Navigation("Iti");

                    b.Navigation("Traslado");

                    b.Navigation("Vuelo");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.GrupoDeViaje", b =>
                {
                    b.HasOne("LogicaNegocio.Dominio.Usuario", "Coordinador")
                        .WithMany("GruposComoCoordinador")
                        .HasForeignKey("CoordinadorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Coordinador");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Hotel", b =>
                {
                    b.HasOne("LogicaNegocio.Dominio.Ciudad", "Ciudad")
                        .WithMany("Hoteles")
                        .HasForeignKey("CiudadId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Dominio.Pais", "Pais")
                        .WithMany("Hoteles")
                        .HasForeignKey("PaisId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Ciudad");

                    b.Navigation("Pais");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Itinerario", b =>
                {
                    b.HasOne("LogicaNegocio.Dominio.GrupoDeViaje", "Grupo")
                        .WithMany()
                        .HasForeignKey("GrupoDeViajeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grupo");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Ciudad", b =>
                {
                    b.Navigation("Aeropuertos");

                    b.Navigation("Hoteles");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Itinerario", b =>
                {
                    b.Navigation("Eventos");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Pais", b =>
                {
                    b.Navigation("Aeropuertos");

                    b.Navigation("Ciudades");

                    b.Navigation("Hoteles");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Usuario", b =>
                {
                    b.Navigation("GruposComoCoordinador");
                });
#pragma warning restore 612, 618
        }
    }
}
