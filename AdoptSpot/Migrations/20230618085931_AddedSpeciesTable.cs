using Microsoft.EntityFrameworkCore.Migrations;

namespace AdoptSpot.Migrations
{
    public partial class AddedSpeciesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Species",
                table: "Pet");

            migrationBuilder.AddColumn<int>(
                name: "SpeciesId",
                table: "Pet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    SpeciesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    SpeciesType = table.Column<int>(type: "int", nullable: false),
                    SpeciesDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AverageLifespan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Behavior = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Habitat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AverageWeight = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.SpeciesId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pet_SpeciesId",
                table: "Pet",
                column: "SpeciesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Species_SpeciesId",
                table: "Pet",
                column: "SpeciesId",
                principalTable: "Species",
                principalColumn: "SpeciesId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Species_SpeciesId",
                table: "Pet");

            migrationBuilder.DropTable(
                name: "Species");

            migrationBuilder.DropIndex(
                name: "IX_Pet_SpeciesId",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "SpeciesId",
                table: "Pet");

            migrationBuilder.AddColumn<string>(
                name: "Species",
                table: "Pet",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
