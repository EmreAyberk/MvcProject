using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerPlantMvcApplication.Migrations
{
    public partial class ElectrometerFKChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Electrometers_PowerPlants_PowerPlantId",
                table: "Electrometers");

            migrationBuilder.DropIndex(
                name: "IX_Electrometers_PowerPlantId",
                table: "Electrometers");

            migrationBuilder.DropColumn(
                name: "PowerPlantId",
                table: "Electrometers");

            migrationBuilder.AddColumn<long>(
                name: "PowerPlantUnitId",
                table: "Electrometers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Stoppages",
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
                name: "IX_Electrometers_PowerPlantUnitId",
                table: "Electrometers",
                column: "PowerPlantUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Stoppages_PowerPlantId",
                table: "Stoppages",
                column: "PowerPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Stoppages_PowerPlantUnitId",
                table: "Stoppages",
                column: "PowerPlantUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Electrometers_PowerPlantUnits_PowerPlantUnitId",
                table: "Electrometers",
                column: "PowerPlantUnitId",
                principalTable: "PowerPlantUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Electrometers_PowerPlantUnits_PowerPlantUnitId",
                table: "Electrometers");

            migrationBuilder.DropTable(
                name: "Stoppages");

            migrationBuilder.DropIndex(
                name: "IX_Electrometers_PowerPlantUnitId",
                table: "Electrometers");

            migrationBuilder.DropColumn(
                name: "PowerPlantUnitId",
                table: "Electrometers");

            migrationBuilder.AddColumn<long>(
                name: "PowerPlantId",
                table: "Electrometers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Electrometers_PowerPlantId",
                table: "Electrometers",
                column: "PowerPlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Electrometers_PowerPlants_PowerPlantId",
                table: "Electrometers",
                column: "PowerPlantId",
                principalTable: "PowerPlants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
