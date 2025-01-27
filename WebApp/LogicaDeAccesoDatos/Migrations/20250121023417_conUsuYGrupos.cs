using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDeAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class conUsuYGrupos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrupoDeViajeUsuarios");

            migrationBuilder.CreateTable(
                name: "GrupoDeViajeViajero",
                columns: table => new
                {
                    GrupoDeViajeId = table.Column<int>(type: "int", nullable: false),
                    ViajeroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoDeViajeViajero", x => new { x.GrupoDeViajeId, x.ViajeroId });
                    table.ForeignKey(
                        name: "FK_GrupoDeViajeViajero_GruposDeViaje_GrupoDeViajeId",
                        column: x => x.GrupoDeViajeId,
                        principalTable: "GruposDeViaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupoDeViajeViajero_Usuarios_ViajeroId",
                        column: x => x.ViajeroId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrupoDeViajeViajero_ViajeroId",
                table: "GrupoDeViajeViajero",
                column: "ViajeroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrupoDeViajeViajero");

            migrationBuilder.CreateTable(
                name: "GrupoDeViajeUsuarios",
                columns: table => new
                {
                    GruposComoViajeroId = table.Column<int>(type: "int", nullable: false),
                    ViajerosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoDeViajeUsuarios", x => new { x.GruposComoViajeroId, x.ViajerosId });
                    table.ForeignKey(
                        name: "FK_GrupoDeViajeUsuarios_GruposDeViaje_GruposComoViajeroId",
                        column: x => x.GruposComoViajeroId,
                        principalTable: "GruposDeViaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupoDeViajeUsuarios_Usuarios_ViajerosId",
                        column: x => x.ViajerosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrupoDeViajeUsuarios_ViajerosId",
                table: "GrupoDeViajeUsuarios",
                column: "ViajerosId");
        }
    }
}
