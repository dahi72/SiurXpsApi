using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDeAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class dias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "Horario",
                table: "Vuelos",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "ActividadDia",
                columns: table => new
                {
                    ActividadesId = table.Column<int>(type: "int", nullable: false),
                    DiasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActividadDia", x => new { x.ActividadesId, x.DiasId });
                    table.ForeignKey(
                        name: "FK_ActividadDia_Actividades_ActividadesId",
                        column: x => x.ActividadesId,
                        principalTable: "Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActividadDia_Dia_DiasId",
                        column: x => x.DiasId,
                        principalTable: "Dia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AerolineaDia",
                columns: table => new
                {
                    AerolinesId = table.Column<int>(type: "int", nullable: false),
                    DiasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AerolineaDia", x => new { x.AerolinesId, x.DiasId });
                    table.ForeignKey(
                        name: "FK_AerolineaDia_Aerolineas_AerolinesId",
                        column: x => x.AerolinesId,
                        principalTable: "Aerolineas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AerolineaDia_Dia_DiasId",
                        column: x => x.DiasId,
                        principalTable: "Dia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AeropuertoDia",
                columns: table => new
                {
                    AeropuertosId = table.Column<int>(type: "int", nullable: false),
                    DiasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AeropuertoDia", x => new { x.AeropuertosId, x.DiasId });
                    table.ForeignKey(
                        name: "FK_AeropuertoDia_Aeropuertos_AeropuertosId",
                        column: x => x.AeropuertosId,
                        principalTable: "Aeropuertos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AeropuertoDia_Dia_DiasId",
                        column: x => x.DiasId,
                        principalTable: "Dia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiaHotel",
                columns: table => new
                {
                    DiasId = table.Column<int>(type: "int", nullable: false),
                    HotelesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaHotel", x => new { x.DiasId, x.HotelesId });
                    table.ForeignKey(
                        name: "FK_DiaHotel_Dia_DiasId",
                        column: x => x.DiasId,
                        principalTable: "Dia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiaHotel_Hoteles_HotelesId",
                        column: x => x.HotelesId,
                        principalTable: "Hoteles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiaTraslado",
                columns: table => new
                {
                    DiasId = table.Column<int>(type: "int", nullable: false),
                    TrasladosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaTraslado", x => new { x.DiasId, x.TrasladosId });
                    table.ForeignKey(
                        name: "FK_DiaTraslado_Dia_DiasId",
                        column: x => x.DiasId,
                        principalTable: "Dia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiaTraslado_Traslados_TrasladosId",
                        column: x => x.TrasladosId,
                        principalTable: "Traslados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiaVuelo",
                columns: table => new
                {
                    DiasId = table.Column<int>(type: "int", nullable: false),
                    VuelosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaVuelo", x => new { x.DiasId, x.VuelosId });
                    table.ForeignKey(
                        name: "FK_DiaVuelo_Dia_DiasId",
                        column: x => x.DiasId,
                        principalTable: "Dia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiaVuelo_Vuelos_VuelosId",
                        column: x => x.VuelosId,
                        principalTable: "Vuelos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActividadDia_DiasId",
                table: "ActividadDia",
                column: "DiasId");

            migrationBuilder.CreateIndex(
                name: "IX_AerolineaDia_DiasId",
                table: "AerolineaDia",
                column: "DiasId");

            migrationBuilder.CreateIndex(
                name: "IX_AeropuertoDia_DiasId",
                table: "AeropuertoDia",
                column: "DiasId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaHotel_HotelesId",
                table: "DiaHotel",
                column: "HotelesId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaTraslado_TrasladosId",
                table: "DiaTraslado",
                column: "TrasladosId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaVuelo_VuelosId",
                table: "DiaVuelo",
                column: "VuelosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActividadDia");

            migrationBuilder.DropTable(
                name: "AerolineaDia");

            migrationBuilder.DropTable(
                name: "AeropuertoDia");

            migrationBuilder.DropTable(
                name: "DiaHotel");

            migrationBuilder.DropTable(
                name: "DiaTraslado");

            migrationBuilder.DropTable(
                name: "DiaVuelo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Horario",
                table: "Vuelos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");
        }
    }
}
