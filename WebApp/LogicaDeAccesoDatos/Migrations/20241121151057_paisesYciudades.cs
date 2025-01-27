using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDeAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class paisesYciudades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GruposDeViaje_Ciudades_CiudadDestinoId",
                table: "GruposDeViaje");

            migrationBuilder.DropForeignKey(
                name: "FK_GruposDeViaje_Paises_PaisDestinoId",
                table: "GruposDeViaje");

            migrationBuilder.DropIndex(
                name: "IX_GruposDeViaje_CiudadDestinoId",
                table: "GruposDeViaje");

            migrationBuilder.DropIndex(
                name: "IX_GruposDeViaje_PaisDestinoId",
                table: "GruposDeViaje");

            migrationBuilder.DropColumn(
                name: "CiudadDestinoId",
                table: "GruposDeViaje");

            migrationBuilder.DropColumn(
                name: "PaisDestinoId",
                table: "GruposDeViaje");

            migrationBuilder.AddColumn<string>(
                name: "CiudadesDestinoIds",
                table: "GruposDeViaje",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "PaisesDestinoIds",
                table: "GruposDeViaje",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.CreateTable(
                name: "GrupoDeViajeCiudadesDestino",
                columns: table => new
                {
                    CiudadesDestinoId = table.Column<int>(type: "int", nullable: false),
                    GrupoDeViajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoDeViajeCiudadesDestino", x => new { x.CiudadesDestinoId, x.GrupoDeViajeId });
                    table.ForeignKey(
                        name: "FK_GrupoDeViajeCiudadesDestino_Ciudades_CiudadesDestinoId",
                        column: x => x.CiudadesDestinoId,
                        principalTable: "Ciudades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupoDeViajeCiudadesDestino_GruposDeViaje_GrupoDeViajeId",
                        column: x => x.GrupoDeViajeId,
                        principalTable: "GruposDeViaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrupoDeViajePaisesDestino",
                columns: table => new
                {
                    GrupoDeViajeId = table.Column<int>(type: "int", nullable: false),
                    PaisesDestinoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoDeViajePaisesDestino", x => new { x.GrupoDeViajeId, x.PaisesDestinoId });
                    table.ForeignKey(
                        name: "FK_GrupoDeViajePaisesDestino_GruposDeViaje_GrupoDeViajeId",
                        column: x => x.GrupoDeViajeId,
                        principalTable: "GruposDeViaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupoDeViajePaisesDestino_Paises_PaisesDestinoId",
                        column: x => x.PaisesDestinoId,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrupoDeViajeCiudadesDestino_GrupoDeViajeId",
                table: "GrupoDeViajeCiudadesDestino",
                column: "GrupoDeViajeId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoDeViajePaisesDestino_PaisesDestinoId",
                table: "GrupoDeViajePaisesDestino",
                column: "PaisesDestinoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrupoDeViajeCiudadesDestino");

            migrationBuilder.DropTable(
                name: "GrupoDeViajePaisesDestino");

            migrationBuilder.DropColumn(
                name: "CiudadesDestinoIds",
                table: "GruposDeViaje");

            migrationBuilder.DropColumn(
                name: "PaisesDestinoIds",
                table: "GruposDeViaje");

            migrationBuilder.AddColumn<int>(
                name: "CiudadDestinoId",
                table: "GruposDeViaje",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaisDestinoId",
                table: "GruposDeViaje",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GruposDeViaje_CiudadDestinoId",
                table: "GruposDeViaje",
                column: "CiudadDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_GruposDeViaje_PaisDestinoId",
                table: "GruposDeViaje",
                column: "PaisDestinoId");

            migrationBuilder.AddForeignKey(
                name: "FK_GruposDeViaje_Ciudades_CiudadDestinoId",
                table: "GruposDeViaje",
                column: "CiudadDestinoId",
                principalTable: "Ciudades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GruposDeViaje_Paises_PaisDestinoId",
                table: "GruposDeViaje",
                column: "PaisDestinoId",
                principalTable: "Paises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
