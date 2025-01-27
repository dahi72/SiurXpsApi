using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDeAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class porLasdudas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Eventos_ActividadId",
                table: "Eventos",
                column: "ActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_AerolineaId",
                table: "Eventos",
                column: "AerolineaId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_AeropuertoId",
                table: "Eventos",
                column: "AeropuertoId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_HotelId",
                table: "Eventos",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_TrasladoId",
                table: "Eventos",
                column: "TrasladoId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_VueloId",
                table: "Eventos",
                column: "VueloId");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Actividades_ActividadId",
                table: "Eventos",
                column: "ActividadId",
                principalTable: "Actividades",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Aerolineas_AerolineaId",
                table: "Eventos",
                column: "AerolineaId",
                principalTable: "Aerolineas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Aeropuertos_AeropuertoId",
                table: "Eventos",
                column: "AeropuertoId",
                principalTable: "Aeropuertos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Hoteles_HotelId",
                table: "Eventos",
                column: "HotelId",
                principalTable: "Hoteles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Traslados_TrasladoId",
                table: "Eventos",
                column: "TrasladoId",
                principalTable: "Traslados",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Vuelos_VueloId",
                table: "Eventos",
                column: "VueloId",
                principalTable: "Vuelos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Actividades_ActividadId",
                table: "Eventos");

            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Aerolineas_AerolineaId",
                table: "Eventos");

            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Aeropuertos_AeropuertoId",
                table: "Eventos");

            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Hoteles_HotelId",
                table: "Eventos");

            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Traslados_TrasladoId",
                table: "Eventos");

            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Vuelos_VueloId",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_ActividadId",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_AerolineaId",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_AeropuertoId",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_HotelId",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_TrasladoId",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_VueloId",
                table: "Eventos");
        }
    }
}
