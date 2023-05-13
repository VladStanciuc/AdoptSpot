
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
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;





namespace AdoptSpot.Data.Services
{
    public class PetService : EntityBaseRepository<Pet>, IPetService
    {
        private readonly IVaccinationService _vaccinationService;
        private readonly AppDbContext _context;

        public PetService(AppDbContext context, IVaccinationService vaccinationService) : base(context)
        {
            _vaccinationService = vaccinationService;
            _context = context;
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
        public async Task<bool> DeleteVaccinationAsync(int vaccineId)
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

        public async Task UpdateExistingVaccinationsAsync(Pet petToUpdate, [Bind(Prefix = "MedicalRecord.Vaccines")] ICollection<Vaccination> updatedVaccinations)
        {
            if (petToUpdate == null)
            {
                throw new ArgumentException("Invalid pet");
            }

            foreach (var updatedVaccination in updatedVaccinations)
            {
                var existingVaccination = petToUpdate.MedicalRecord.Vaccinations.FirstOrDefault(v => v.Id == updatedVaccination.Id);

                if (existingVaccination != null)
                {
                    existingVaccination.Disease = updatedVaccination.Disease;
                    existingVaccination.DateAdministered = updatedVaccination.DateAdministered;
                    existingVaccination.VeterinarianName = updatedVaccination.VeterinarianName;
                    existingVaccination.ExpirationDate = updatedVaccination.ExpirationDate;
                    existingVaccination.BatchNumber = updatedVaccination.BatchNumber;
                    existingVaccination.Notes = updatedVaccination.Notes;
                    
                }
            }
            await _context.SaveChangesAsync();

        }

        public async Task UploadImages(Pet petToUpdate, List<IFormFile> newImages)
        {
            foreach (var image in newImages)
            {
                if (image != null && image.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await image.CopyToAsync(memoryStream);
                    using var imageSharp = Image.Load(memoryStream.ToArray());
                    imageSharp.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max,
                        Size = new Size(500, 500)
                    }));
                    using var resultStream = new MemoryStream();
                    await imageSharp.SaveAsJpegAsync(resultStream);
                    var resizedImageBytes = resultStream.ToArray();
                    var img = new ImageModel
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
        public async Task DeleteImagesAsync(Pet petToUpdate, [FromBody] int[] imageIds)
        {
            var imagesToDelete = petToUpdate.Images.Where(i => imageIds.Contains(i.Id)).ToList();

            if (imagesToDelete.Count > 0)
            {
                foreach (var image in imagesToDelete)
                {
                    petToUpdate.Images.Remove(image);
                }

                // Assuming _context is your DbContext
                _context.Pets.Update(petToUpdate);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No images found with the provided ids");
            }
        }


    }
}
