using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerPlantMvcApplication.Migrations
{
    public partial class GenerationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stoppages");

            migrationBuilder.CreateTable(
                name: "Generations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PowerPlantId = table.Column<long>(nullable: true),
                    PowerPlantUnitId = table.Column<long>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Generations_PowerPlants_PowerPlantId",
                        column: x => x.PowerPlantId,
                        principalTable: "PowerPlants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Generations_PowerPlantUnits_PowerPlantUnitId",
                        column: x => x.PowerPlantUnitId,
                        principalTable: "PowerPlantUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Generations_PowerPlantId",
                table: "Generations",
                column: "PowerPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Generations_PowerPlantUnitId",
                table: "Generations",
                column: "PowerPlantUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Generations");

            migrationBuilder.CreateTable(
                name: "Stoppages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PowerPlantId = table.Column<long>(type: "bigint", nullable: true),
                    PowerPlantUnitId = table.Column<long>(type: "bigint", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stoppages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stoppages_PowerPlants_PowerPlantId",
                        column: x => x.PowerPlantId,
                        principalTable: "PowerPlants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stoppages_PowerPlantUnits_PowerPlantUnitId",
                        column: x => x.PowerPlantUnitId,
                        principalTable: "PowerPlantUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stoppages_PowerPlantId",
                table: "Stoppages",
                column: "PowerPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Stoppages_PowerPlantUnitId",
                table: "Stoppages",
                column: "PowerPlantUnitId");
        }
    }
}
