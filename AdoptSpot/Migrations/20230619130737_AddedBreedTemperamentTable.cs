using Microsoft.EntityFrameworkCore.Migrations;

namespace AdoptSpot.Migrations
{
    public partial class AddedBreedTemperamentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BreedCharacteristics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LifeSpanInYears = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    CommonHealthIssues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreedCharacteristics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BreedTemperaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BreedId = table.Column<int>(type: "int", nullable: false),
                    TemperamentType = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreedTemperaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreedTemperaments_BreedCharacteristics_BreedId",
                        column: x => x.BreedId,
                        principalTable: "BreedCharacteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BreedTemperaments_BreedId",
                table: "BreedTemperaments",
                column: "BreedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreedTemperaments");

            migrationBuilder.DropTable(
                name: "BreedCharacteristics");
        }
    }
}
