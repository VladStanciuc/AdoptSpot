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

    }
}
