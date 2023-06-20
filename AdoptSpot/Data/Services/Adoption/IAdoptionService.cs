using AdoptSpot.Data.Base;
using AdoptSpot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Data.Services
{
    public interface IAdoptionService : IEntityBaseRepository<Adoption>
    {
        Task AddAdoptionRequestAsync( Adoption adoptionForm);
        Task<IEnumerable<int>> GetAdoptionRequests(string userId);

    }
}
