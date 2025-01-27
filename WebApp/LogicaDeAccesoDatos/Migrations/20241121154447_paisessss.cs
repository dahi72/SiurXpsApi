using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDeAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class paisessss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CiudadesDestinoIds",
                table: "GruposDeViaje");

            migrationBuilder.DropColumn(
                name: "PaisesDestinoIds",
                table: "GruposDeViaje");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CiudadesDestinoIds",
                table: "GruposDeViaje",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaisesDestinoIds",
                table: "GruposDeViaje",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
