using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdoptSpot.Migrations
{
    public partial class ModifyImageModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Images",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Images");
        }
    }
}
