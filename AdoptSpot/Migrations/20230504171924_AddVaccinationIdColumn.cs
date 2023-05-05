using Microsoft.EntityFrameworkCore.Migrations;

namespace AdoptSpot.Migrations
{
    public partial class AddVaccinationIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecord_Pet_PetId",
                table: "MedicalRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalTreatment_MedicalRecord_MedicalRecordId",
                table: "MedicalTreatment");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccinations_MedicalRecord_MedicalRecordId",
                table: "Vaccinations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalRecord",
                table: "MedicalRecord");

            migrationBuilder.RenameTable(
                name: "MedicalRecord",
                newName: "MedicalRecord");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalRecord_PetId",
                table: "MedicalRecord",
                newName: "IX_MedicalRecord_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalRecord",
                table: "MedicalRecord",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecord_Pet_PetId",
                table: "MedicalRecord",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalTreatment_MedicalRecord_MedicalRecordId",
                table: "MedicalTreatment",
                column: "MedicalRecordId",
                principalTable: "MedicalRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccinations_MedicalRecord_MedicalRecordId",
                table: "Vaccinations",
                column: "MedicalRecordId",
                principalTable: "MedicalRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddColumn<int>(
            name: "VaccinationId",
            table: "MedicalRecord",
            nullable: false,
            defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecord_Pet_PetId",
                table: "MedicalRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalTreatment_MedicalRecord_MedicalRecordId",
                table: "MedicalTreatment");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccinations_MedicalRecord_MedicalRecordId",
                table: "Vaccinations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalRecord",
                table: "MedicalRecord");

            migrationBuilder.RenameTable(
                name: "MedicalRecord",
                newName: "MedicalRecord");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalRecord_PetId",
                table: "MedicalRecord",
                newName: "IX_MedicalRecord_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalRecord",
                table: "MedicalRecord",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecord_Pet_PetId",
                table: "MedicalRecord",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalTreatment_MedicalRecord_MedicalRecordId",
                table: "MedicalTreatment",
                column: "MedicalRecordId",
                principalTable: "MedicalRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccinations_MedicalRecord_MedicalRecordId",
                table: "Vaccinations",
                column: "MedicalRecordId",
                principalTable: "MedicalRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropColumn(
            name: "VaccinationId",
            table: "MedicalRecord");
        }
    }
}
