using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdoptSpot.Migrations
{
    public partial class ModifiedMedicalTreatmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dosage",
                table: "MedicalTreatment");

            migrationBuilder.DropColumn(
                name: "DosageUnit",
                table: "MedicalTreatment");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "MedicalTreatment");

            migrationBuilder.DropColumn(
                name: "FrequencyUnit",
                table: "MedicalTreatment");

            migrationBuilder.DropColumn(
                name: "TreatmentDate",
                table: "MedicalTreatment");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "MedicalTreatment",
                newName: "DosageAndUnit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DosageAndUnit",
                table: "MedicalTreatment",
                newName: "Notes");

            migrationBuilder.AddColumn<int>(
                name: "Dosage",
                table: "MedicalTreatment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DosageUnit",
                table: "MedicalTreatment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Frequency",
                table: "MedicalTreatment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FrequencyUnit",
                table: "MedicalTreatment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TreatmentDate",
                table: "MedicalTreatment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
