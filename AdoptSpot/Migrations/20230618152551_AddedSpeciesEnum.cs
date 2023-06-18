using Microsoft.EntityFrameworkCore.Migrations;

namespace AdoptSpot.Migrations
{
    public partial class AddedSpeciesEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Species_SpeciesId",
                table: "Pet");

            migrationBuilder.DropTable(
                name: "Species");

            migrationBuilder.DropIndex(
                name: "IX_Pet_SpeciesId",
                table: "Pet");

            migrationBuilder.RenameColumn(
                name: "SpeciesId",
                table: "Pet",
                newName: "SpeciesType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpeciesType",
                table: "Pet",
                newName: "SpeciesId");

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AverageLifespan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AverageWeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Behavior = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Habitat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpeciesDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpeciesType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
