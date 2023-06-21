using AdoptSpot.Data.Base;
using AdoptSpot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Data.Services
{
    public interface IPetService : IEntityBaseRepository<Pet>
    {
        Task UploadImages(Pet petToUpdate, List<IFormFile> newImages);
        Task AddVaccinationAsync(Pet petToUpdate, Vaccination vaccination);
        Task<bool> DeleteVaccinationAsync( int vaccineId);
        Task UpdateExistingVaccinationsAsync(Pet petToUpdate, [Bind(Prefix = "MedicalRecord.Vaccines")] ICollection<Vaccination> updatedVaccinations);
        Task DeleteImagesAsync(Pet petToUpdate, [FromBody] int[] imageIds);
        Task UpdateExistingMedicalTreatments(Pet petToUpdate, MedicalTreatment updatedMedicalTreatments);
        Task DeleteMedicalTreatmentAsync(Pet petToUpdate,int medicalTreatmentId);
        Task AddMedicalTreatmentAsync(Pet petToUpdate, MedicalTreatment newMedicalTreatment);
        Task<ICollection<MedicalTreatment>> GetMedicalTreatmentsAsync(int petId);
        Task <Pet>  GetByIdWithImages(int petId);
        Task<List<Pet>> GetAllPetsWithImages();
    }
}
