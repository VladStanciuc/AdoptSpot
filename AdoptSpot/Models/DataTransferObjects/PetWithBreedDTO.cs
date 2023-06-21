using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Models.DataTransferObjects
{
    public class PetWithBreedDTO
    {
        public Pet Pet { get; set; }
        public double Score { get; set; }
        public PetWithBreedDTO(Pet pet, double score)
        {
            Pet = pet;
            Score = score;
        }
    }
}
