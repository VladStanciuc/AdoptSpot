using Microsoft.EntityFrameworkCore.Migrations;

namespace AdoptSpot.Migrations
{
    public partial class AddedWeightProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "UserPreferenceTemperamentScore",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "UserPreferenceTemperamentScore");
        }
    }
}
