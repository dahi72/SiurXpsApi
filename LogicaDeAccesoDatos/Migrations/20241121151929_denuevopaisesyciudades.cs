using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDeAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class denuevopaisesyciudades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrupoDeViajeCiudadesDestino_Ciudades_CiudadesDestinoId",
                table: "GrupoDeViajeCiudadesDestino");

            migrationBuilder.DropForeignKey(
                name: "FK_GrupoDeViajePaisesDestino_Paises_PaisesDestinoId",
                table: "GrupoDeViajePaisesDestino");

            migrationBuilder.RenameColumn(
                name: "PaisesDestinoId",
                table: "GrupoDeViajePaisesDestino",
                newName: "PaisId");

            migrationBuilder.RenameIndex(
                name: "IX_GrupoDeViajePaisesDestino_PaisesDestinoId",
                table: "GrupoDeViajePaisesDestino",
                newName: "IX_GrupoDeViajePaisesDestino_PaisId");

            migrationBuilder.RenameColumn(
                name: "CiudadesDestinoId",
                table: "GrupoDeViajeCiudadesDestino",
                newName: "CiudadId");

            migrationBuilder.AddForeignKey(
                name: "FK_GrupoDeViajeCiudadesDestino_Ciudades_CiudadId",
                table: "GrupoDeViajeCiudadesDestino",
                column: "CiudadId",
                principalTable: "Ciudades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GrupoDeViajePaisesDestino_Paises_PaisId",
                table: "GrupoDeViajePaisesDestino",
                column: "PaisId",
                principalTable: "Paises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrupoDeViajeCiudadesDestino_Ciudades_CiudadId",
                table: "GrupoDeViajeCiudadesDestino");

            migrationBuilder.DropForeignKey(
                name: "FK_GrupoDeViajePaisesDestino_Paises_PaisId",
                table: "GrupoDeViajePaisesDestino");

            migrationBuilder.RenameColumn(
                name: "PaisId",
                table: "GrupoDeViajePaisesDestino",
                newName: "PaisesDestinoId");

            migrationBuilder.RenameIndex(
                name: "IX_GrupoDeViajePaisesDestino_PaisId",
                table: "GrupoDeViajePaisesDestino",
                newName: "IX_GrupoDeViajePaisesDestino_PaisesDestinoId");

            migrationBuilder.RenameColumn(
                name: "CiudadId",
                table: "GrupoDeViajeCiudadesDestino",
                newName: "CiudadesDestinoId");

            migrationBuilder.AddForeignKey(
                name: "FK_GrupoDeViajeCiudadesDestino_Ciudades_CiudadesDestinoId",
                table: "GrupoDeViajeCiudadesDestino",
                column: "CiudadesDestinoId",
                principalTable: "Ciudades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GrupoDeViajePaisesDestino_Paises_PaisesDestinoId",
                table: "GrupoDeViajePaisesDestino",
                column: "PaisesDestinoId",
                principalTable: "Paises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
