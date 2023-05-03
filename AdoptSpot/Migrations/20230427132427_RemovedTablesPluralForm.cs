using Microsoft.EntityFrameworkCore.Migrations;

namespace AdoptSpot.Migrations
{
    public partial class RemovedTablesPluralForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adoptions_Pets_PetId",
                table: "Adoptions");
         

            migrationBuilder.DropForeignKey(
                name: "FK_Adoptions_Users_UserId",
                table: "Adoptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Pets_PetId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistories_Pets_PetId",
                table: "MedicalRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalTreatments_MedicalRecord_MedicalRecordId",
                table: "MedicalTreatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccination_MedicalRecord_MedicalRecordId",
                table: "Vaccination");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pets",
                table: "Pets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalTreatments",
                table: "MedicalTreatments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalHistories",
                table: "MedicalRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adoptions",
                table: "Adoptions");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Pets",
                newName: "Pet");

            migrationBuilder.RenameTable(
                name: "MedicalTreatments",
                newName: "MedicalTreatment");

            migrationBuilder.RenameTable(
                name: "MedicalRecord",
                newName: "MedicalRecord");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image");

            migrationBuilder.RenameTable(
                name: "Adoptions",
                newName: "Adoption");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalTreatments_MedicalRecordId",
                table: "MedicalTreatment",
                newName: "IX_MedicalTreatment_MedicalRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalHistories_PetId",
                table: "MedicalRecord",
                newName: "IX_MedicalRecord_PetId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_PetId",
                table: "Image",
                newName: "IX_Image_PetId");

            migrationBuilder.RenameIndex(
                name: "IX_Adoptions_UserId",
                table: "Adoption",
                newName: "IX_Adoption_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Adoptions_PetId",
                table: "Adoption",
                newName: "IX_Adoption_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pet",
                table: "Pet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalTreatment",
                table: "MedicalTreatment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalRecord",
                table: "MedicalRecord",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adoption",
                table: "Adoption",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adoption_Pet_PetId",
                table: "Adoption",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Adoption_User_UserId",
                table: "Adoption",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Pet_PetId",
                table: "Image",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Vaccination_MedicalRecord_MedicalRecordId",
                table: "Vaccination",
                column: "MedicalRecordId",
                principalTable: "MedicalRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adoption_Pet_PetId",
                table: "Adoption");

            migrationBuilder.DropForeignKey(
                name: "FK_Adoption_User_UserId",
                table: "Adoption");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Pet_PetId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecord_Pet_PetId",
                table: "MedicalRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalTreatment_MedicalRecord_MedicalRecordId",
                table: "MedicalTreatment");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccination_MedicalRecord_MedicalRecordId",
                table: "Vaccination");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pet",
                table: "Pet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalTreatment",
                table: "MedicalTreatment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalHistories",
                table: "MedicalRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adoption",
                table: "Adoption");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Pet",
                newName: "Pets");

            migrationBuilder.RenameTable(
                name: "MedicalTreatment",
                newName: "MedicalTreatments");

            migrationBuilder.RenameTable(
                name: "MedicalRecord",
                newName: "MedicalRecord");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images");

            migrationBuilder.RenameTable(
                name: "Adoption",
                newName: "Adoptions");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalTreatment_MedicalRecordId",
                table: "MedicalTreatments",
                newName: "IX_MedicalTreatments_MedicalRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalHistories_PetId",
                table: "MedicalRecord",
                newName: "IX_MedicalRecord_PetId");

            migrationBuilder.RenameIndex(
                name: "IX_Image_PetId",
                table: "Images",
                newName: "IX_Images_PetId");

            migrationBuilder.RenameIndex(
                name: "IX_Adoption_UserId",
                table: "Adoptions",
                newName: "IX_Adoptions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Adoption_PetId",
                table: "Adoptions",
                newName: "IX_Adoptions_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pets",
                table: "Pets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalTreatments",
                table: "MedicalTreatments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalRecord",
                table: "MedicalRecord",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adoptions",
                table: "Adoptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adoptions_Pets_PetId",
                table: "Adoptions",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Adoptions_Users_UserId",
                table: "Adoptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Pets_PetId",
                table: "Images",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecord_Pets_PetId",
                table: "MedicalRecord",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalTreatments_MedicalRecord_MedicalRecordId",
                table: "MedicalTreatments",
                column: "MedicalRecordId",
                principalTable: "MedicalRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
