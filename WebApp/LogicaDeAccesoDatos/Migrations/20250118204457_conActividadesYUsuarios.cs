using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDeAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class conActividadesYUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Traslados_TiposDeTraslado_TipoDeTrasladoId",
                table: "Traslados");

            migrationBuilder.DropTable(
                name: "TiposDeTraslado");

            migrationBuilder.DropIndex(
                name: "IX_Traslados_TipoDeTrasladoId",
                table: "Traslados");

            migrationBuilder.RenameColumn(
                name: "TipoDeTrasladoId",
                table: "Traslados",
                newName: "TipoDeTraslado");

            migrationBuilder.CreateTable(
                name: "ActividadUsuario",
                columns: table => new
                {
                    ActividadesId = table.Column<int>(type: "int", nullable: false),
                    UsuariosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActividadUsuario", x => new { x.ActividadesId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_ActividadUsuario_Actividades_ActividadesId",
                        column: x => x.ActividadesId,
                        principalTable: "Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActividadUsuario_Usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActividadUsuario_UsuariosId",
                table: "ActividadUsuario",
                column: "UsuariosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActividadUsuario");

            migrationBuilder.RenameColumn(
                name: "TipoDeTraslado",
                table: "Traslados",
                newName: "TipoDeTrasladoId");

            migrationBuilder.CreateTable(
                name: "TiposDeTraslado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDeTraslado", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Traslados_TipoDeTrasladoId",
                table: "Traslados",
                column: "TipoDeTrasladoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Traslados_TiposDeTraslado_TipoDeTrasladoId",
                table: "Traslados",
                column: "TipoDeTrasladoId",
                principalTable: "TiposDeTraslado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
