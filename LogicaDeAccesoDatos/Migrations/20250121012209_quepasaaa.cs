using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDeAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class quepasaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrupoDeViajeUsuarios_GruposDeViaje_GrupoDeViajeId",
                table: "GrupoDeViajeUsuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_GrupoDeViajeUsuarios_Usuarios_UsuarioId",
                table: "GrupoDeViajeUsuarios");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "GrupoDeViajeUsuarios",
                newName: "ViajerosId");

            migrationBuilder.RenameColumn(
                name: "GrupoDeViajeId",
                table: "GrupoDeViajeUsuarios",
                newName: "GruposComoViajeroId");

            migrationBuilder.RenameIndex(
                name: "IX_GrupoDeViajeUsuarios_UsuarioId",
                table: "GrupoDeViajeUsuarios",
                newName: "IX_GrupoDeViajeUsuarios_ViajerosId");

            migrationBuilder.AddForeignKey(
                name: "FK_GrupoDeViajeUsuarios_GruposDeViaje_GruposComoViajeroId",
                table: "GrupoDeViajeUsuarios",
                column: "GruposComoViajeroId",
                principalTable: "GruposDeViaje",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GrupoDeViajeUsuarios_Usuarios_ViajerosId",
                table: "GrupoDeViajeUsuarios",
                column: "ViajerosId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrupoDeViajeUsuarios_GruposDeViaje_GruposComoViajeroId",
                table: "GrupoDeViajeUsuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_GrupoDeViajeUsuarios_Usuarios_ViajerosId",
                table: "GrupoDeViajeUsuarios");

            migrationBuilder.RenameColumn(
                name: "ViajerosId",
                table: "GrupoDeViajeUsuarios",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "GruposComoViajeroId",
                table: "GrupoDeViajeUsuarios",
                newName: "GrupoDeViajeId");

            migrationBuilder.RenameIndex(
                name: "IX_GrupoDeViajeUsuarios_ViajerosId",
                table: "GrupoDeViajeUsuarios",
                newName: "IX_GrupoDeViajeUsuarios_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_GrupoDeViajeUsuarios_GruposDeViaje_GrupoDeViajeId",
                table: "GrupoDeViajeUsuarios",
                column: "GrupoDeViajeId",
                principalTable: "GruposDeViaje",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GrupoDeViajeUsuarios_Usuarios_UsuarioId",
                table: "GrupoDeViajeUsuarios",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
