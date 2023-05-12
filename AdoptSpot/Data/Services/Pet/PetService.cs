
using AdoptSpot.Data.Base;
using AdoptSpot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Data.Services
{
    public class PetService : EntityBaseRepository<Pet>, IPetService
    {
        private readonly IVaccinationService _vaccinationService;
       
        public PetService(AppDbContext context, IVaccinationService vaccinationService) : base(context)
        {
            _vaccinationService = vaccinationService;
        }

       public async Task AddVaccinationAsync(Pet petToUpdate, Vaccination vaccination)
        {
            if (petToUpdate?.MedicalRecord == null)
            {
                throw new ArgumentException("Invalid pet or medical record.");
            }
            bool isDuplicate = petToUpdate.MedicalRecord.Vaccinations.Any(v =>
        v.Disease == vaccination.Disease &&
        v.DateAdministered == vaccination.DateAdministered &&
        v.VeterinarianName == vaccination.VeterinarianName &&
        v.ExpirationDate == vaccination.ExpirationDate &&
        v.BatchNumber == vaccination.BatchNumber &&
        v.Notes == vaccination.Notes
    );
            if (!isDuplicate)
            {
                petToUpdate.MedicalRecord.Vaccinations.Add(vaccination);
                await this.UpdateAsync(petToUpdate.Id, petToUpdate);
            }
        }
        public async Task<bool> DeleteVaccinationAsync( int vaccineId)
        {

            var vaccineToDelete = await _vaccinationService.GetByIdAsync(vaccineId);
          
            if (vaccineToDelete != null)
            {
                await _vaccinationService.DeleteAsync(vaccineToDelete.Id);
                return true;
            }
            else
            {
                return false;
            }
        }


        public async  Task UploadImages(Pet petToUpdate, List<IFormFile> newImages)
        {
            foreach (var image in newImages)
            {
                if (image != null && image.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await image.CopyToAsync(memoryStream);

                    var img = new Image
                    {
                        FileName = image.FileName,
                        ContentType = image.ContentType,
                        Data = memoryStream.ToArray(),
                        PetId = petToUpdate.Id // Set the PetId for the new image
                    };

                    petToUpdate.Images.Add(img);
                }
            }
        }

       
    }
}
