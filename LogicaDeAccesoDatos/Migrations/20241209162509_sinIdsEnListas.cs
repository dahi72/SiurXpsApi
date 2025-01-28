using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDeAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class sinIdsEnListas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_Traslados_Dia_DiaId",
                table: "Traslados");

            migrationBuilder.DropForeignKey(
                name: "FK_Vuelos_Dia_DiaId",
                table: "Vuelos");

            migrationBuilder.DropIndex(
                name: "IX_Vuelos_DiaId",
                table: "Vuelos");

            migrationBuilder.DropIndex(
                name: "IX_Traslados_DiaId",
                table: "Traslados");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DebeCambiarContrasena",
                table: "Usuarios",
                newName: "DebeCambiarContraseña");

            migrationBuilder.AddColumn<int>(
                name: "DiaId",
                table: "Vuelos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiaId",
                table: "Traslados",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Vuelos_DiaId",
                table: "Vuelos",
                column: "DiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Traslados_DiaId",
                table: "Traslados",
                column: "DiaId");

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
    }
}
