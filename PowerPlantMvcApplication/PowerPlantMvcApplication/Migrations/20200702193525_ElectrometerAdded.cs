using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerPlantMvcApplication.Migrations
{
    public partial class ElectrometerAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Electrometers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PowerPlantId = table.Column<long>(nullable: true),
                    Value = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Electrometers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Electrometers_PowerPlants_PowerPlantId",
                        column: x => x.PowerPlantId,
                        principalTable: "PowerPlants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Electrometers_PowerPlantId",
                table: "Electrometers",
                column: "PowerPlantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Electrometers");
        }
    }
}
