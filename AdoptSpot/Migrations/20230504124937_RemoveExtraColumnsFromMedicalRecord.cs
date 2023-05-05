using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AdoptSpot.Migrations
{
    public partial class RemoveExtraColumnsFromMedicalRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
        name: "Vaccination",
        table: "MedicalRecord");

            migrationBuilder.DropColumn(
                name: "VaccinationDate",
                table: "MedicalRecord");

            migrationBuilder.DropColumn(
                name: "MedicalTreatment",
                table: "MedicalRecord");

            migrationBuilder.DropColumn(
                name: "TreatmentDate",
                table: "MedicalRecord");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
       name: "Vaccination",
       table: "MedicalRecord",
       type: "nvarchar(max)",
       nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VaccinationDate",
                table: "MedicalRecord",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicalTreatment",
                table: "MedicalRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TreatmentDate",
                table: "MedicalRecord",
                type: "datetime2",
                nullable: true);
        }
    }
}
