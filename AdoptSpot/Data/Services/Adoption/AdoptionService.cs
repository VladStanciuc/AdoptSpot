using AdoptSpot.Data.Base;
using AdoptSpot.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace AdoptSpot.Data.Services
{
    public class AdoptionService: EntityBaseRepository<Adoption>, IAdoptionService
    {
        private readonly IPetService _petService;
        private readonly AppDbContext _context;
        public AdoptionService(AppDbContext context, IPetService petService) : base(context)
        {
            _petService = petService;
            _context = context;
        }

        public async Task AddAdoptionRequestAsync( Adoption adoptionForm)
        {

                await this.AddAsync(adoptionForm);
            
        }

        public async Task<IEnumerable<int>> GetAdoptionRequests(string userId)
        {
            var adoptions = await this.GetAllAsync();
            var userAdoptions= adoptions.Where(a => a.AdopterUserId == userId);

            var petIds = userAdoptions.Select(a => a.PetId);

            return petIds;
        }
    }
}
