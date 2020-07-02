using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerPlantMvcApplication.Migrations
{
    public partial class PowerPlantUnitAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PowerPlantUnits",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PowerPlantId = table.Column<long>(nullable: true),
                    Capacity = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerPlantUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerPlantUnits_PowerPlants_PowerPlantId",
                        column: x => x.PowerPlantId,
                        principalTable: "PowerPlants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PowerPlantUnits_PowerPlantId",
                table: "PowerPlantUnits",
                column: "PowerPlantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PowerPlantUnits");
        }
    }
}
