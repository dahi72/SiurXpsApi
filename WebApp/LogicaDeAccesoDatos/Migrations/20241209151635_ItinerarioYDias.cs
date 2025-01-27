using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDeAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class ItinerarioYDias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaInicio",
                table: "Actividades");

            migrationBuilder.RenameColumn(
                name: "DebeCambiarContrasena",
                table: "Usuarios",
                newName: "DebeCambiarContraseña");

            migrationBuilder.AddColumn<int>(
                name: "DiaId",
                table: "Vuelos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Horario",
                table: "Traslados",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "DiaId",
                table: "Traslados",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFin",
                table: "Itinerarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicio",
                table: "Itinerarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GrupoDeViajeId",
                table: "Itinerarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiaId",
                table: "Hoteles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiaId",
                table: "Aeropuertos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiaId",
                table: "Aerolineas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiaId",
                table: "Actividades",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Dia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ItinerarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dia_Itinerarios_ItinerarioId",
                        column: x => x.ItinerarioId,
                        principalTable: "Itinerarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vuelos_DiaId",
                table: "Vuelos",
                column: "DiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Traslados_DiaId",
                table: "Traslados",
                column: "DiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Itinerarios_GrupoDeViajeId",
                table: "Itinerarios",
                column: "GrupoDeViajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hoteles_DiaId",
                table: "Hoteles",
                column: "DiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Aeropuertos_DiaId",
                table: "Aeropuertos",
                column: "DiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Aerolineas_DiaId",
                table: "Aerolineas",
                column: "DiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_DiaId",
                table: "Actividades",
                column: "DiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Dia_ItinerarioId",
                table: "Dia",
                column: "ItinerarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actividades_Dia_DiaId",
                table: "Actividades",
                column: "DiaId",
                principalTable: "Dia",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aerolineas_Dia_DiaId",
                table: "Aerolineas",
                column: "DiaId",
                principalTable: "Dia",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aeropuertos_Dia_DiaId",
                table: "Aeropuertos",
                column: "DiaId",
                principalTable: "Dia",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hoteles_Dia_DiaId",
                table: "Hoteles",
                column: "DiaId",
                principalTable: "Dia",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Itinerarios_GruposDeViaje_GrupoDeViajeId",
                table: "Itinerarios",
                column: "GrupoDeViajeId",
                principalTable: "GruposDeViaje",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Traslados_Dia_DiaId",
                table: "Traslados",
                column: "DiaId",
                principalTable: "Dia",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vuelos_Dia_DiaId",
                table: "Vuelos",
                column: "DiaId",
                principalTable: "Dia",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividades_Dia_DiaId",
                table: "Actividades");

            migrationBuilder.DropForeignKey(
                name: "FK_Aerolineas_Dia_DiaId",
                table: "Aerolineas");

            migrationBuilder.DropForeignKey(
                name: "FK_Aeropuertos_Dia_DiaId",
                table: "Aeropuertos");

            migrationBuilder.DropForeignKey(
                name: "FK_Hoteles_Dia_DiaId",
                table: "Hoteles");

            migrationBuilder.DropForeignKey(
                name: "FK_Itinerarios_GruposDeViaje_GrupoDeViajeId",
                table: "Itinerarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Traslados_Dia_DiaId",
                table: "Traslados");

            migrationBuilder.DropForeignKey(
                name: "FK_Vuelos_Dia_DiaId",
                table: "Vuelos");

            migrationBuilder.DropTable(
                name: "Dia");

            migrationBuilder.DropIndex(
                name: "IX_Vuelos_DiaId",
                table: "Vuelos");

            migrationBuilder.DropIndex(
                name: "IX_Traslados_DiaId",
                table: "Traslados");

            migrationBuilder.DropIndex(
                name: "IX_Itinerarios_GrupoDeViajeId",
                table: "Itinerarios");

            migrationBuilder.DropIndex(
                name: "IX_Hoteles_DiaId",
                table: "Hoteles");

            migrationBuilder.DropIndex(
                name: "IX_Aeropuertos_DiaId",
                table: "Aeropuertos");

            migrationBuilder.DropIndex(
                name: "IX_Aerolineas_DiaId",
                table: "Aerolineas");

            migrationBuilder.DropIndex(
                name: "IX_Actividades_DiaId",
                table: "Actividades");

            migrationBuilder.DropColumn(
                name: "DiaId",
                table: "Vuelos");

            migrationBuilder.DropColumn(
                name: "DiaId",
                table: "Traslados");

            migrationBuilder.DropColumn(
                name: "FechaFin",
                table: "Itinerarios");

            migrationBuilder.DropColumn(
                name: "FechaInicio",
                table: "Itinerarios");

            migrationBuilder.DropColumn(
                name: "GrupoDeViajeId",
                table: "Itinerarios");

            migrationBuilder.DropColumn(
                name: "DiaId",
                table: "Hoteles");

            migrationBuilder.DropColumn(
                name: "DiaId",
                table: "Aeropuertos");

            migrationBuilder.DropColumn(
                name: "DiaId",
                table: "Aerolineas");

            migrationBuilder.DropColumn(
                name: "DiaId",
                table: "Actividades");

            migrationBuilder.RenameColumn(
                name: "DebeCambiarContraseña",
                table: "Usuarios",
                newName: "DebeCambiarContrasena");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Horario",
                table: "Traslados",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicio",
                table: "Actividades",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
