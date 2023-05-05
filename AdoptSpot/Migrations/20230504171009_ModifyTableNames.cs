using Microsoft.EntityFrameworkCore.Migrations;

namespace AdoptSpot.Migrations
{
    public partial class ModifyTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vaccination_MedicalRecord_MedicalRecordId",
                table: "Vaccination");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vaccination",
                table: "Vaccination");

            migrationBuilder.RenameTable(
                name: "Vaccination",
                newName: "Vaccinations");

            migrationBuilder.RenameIndex(
                name: "IX_Vaccination_MedicalRecordId",
                table: "Vaccinations",
                newName: "IX_Vaccinations_MedicalRecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vaccinations",
                table: "Vaccinations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccinations_MedicalRecord_MedicalRecordId",
                table: "Vaccinations",
                column: "MedicalRecordId",
                principalTable: "MedicalRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vaccinations_MedicalRecord_MedicalRecordId",
                table: "Vaccinations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vaccinations",
                table: "Vaccinations");

            migrationBuilder.RenameTable(
                name: "Vaccinations",
                newName: "Vaccination");

            migrationBuilder.RenameIndex(
                name: "IX_Vaccinations_MedicalRecordId",
                table: "Vaccination",
                newName: "IX_Vaccination_MedicalRecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vaccination",
                table: "Vaccination",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccination_MedicalRecord_MedicalRecordId",
                table: "Vaccination",
                column: "MedicalRecordId",
                principalTable: "MedicalRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
