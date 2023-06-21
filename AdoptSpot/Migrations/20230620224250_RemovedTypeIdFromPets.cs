using Microsoft.EntityFrameworkCore.Migrations;

namespace AdoptSpot.Migrations
{
    public partial class RemovedTypeIdFromPets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Pet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Pet",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
