using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDeAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class tokenSinId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TokensRevocados",
                table: "TokensRevocados");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TokensRevocados");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "TokensRevocados",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TokensRevocados",
                table: "TokensRevocados",
                column: "Token");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TokensRevocados",
                table: "TokensRevocados");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "TokensRevocados",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TokensRevocados",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TokensRevocados",
                table: "TokensRevocados",
                column: "Id");
        }
    }
}
