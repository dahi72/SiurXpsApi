using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDeAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class conCatalogos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "Aeropuertos");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Aeropuertos");

            migrationBuilder.AddColumn<int>(
                name: "CiudadId",
                table: "Aeropuertos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaisId",
                table: "Aeropuertos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Aeropuertos_CiudadId",
                table: "Aeropuertos",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_Aeropuertos_PaisId",
                table: "Aeropuertos",
                column: "PaisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aeropuertos_Ciudades_CiudadId",
                table: "Aeropuertos",
                column: "CiudadId",
                principalTable: "Ciudades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Aeropuertos_Paises_PaisId",
                table: "Aeropuertos",
                column: "PaisId",
                principalTable: "Paises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aeropuertos_Ciudades_CiudadId",
                table: "Aeropuertos");

            migrationBuilder.DropForeignKey(
                name: "FK_Aeropuertos_Paises_PaisId",
                table: "Aeropuertos");

            migrationBuilder.DropIndex(
                name: "IX_Aeropuertos_CiudadId",
                table: "Aeropuertos");

            migrationBuilder.DropIndex(
                name: "IX_Aeropuertos_PaisId",
                table: "Aeropuertos");

            migrationBuilder.DropColumn(
                name: "CiudadId",
                table: "Aeropuertos");

            migrationBuilder.DropColumn(
                name: "PaisId",
                table: "Aeropuertos");

            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "Aeropuertos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Aeropuertos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
