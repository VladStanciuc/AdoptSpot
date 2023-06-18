using AdoptSpot.Data.Base;
using AdoptSpot.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Models
{
    public class Species : IEntityBase
    {

        public int Id { get; set; }
        public int PetId { get; set; }
        public ICollection<Pet> Pets { get; set; }

        public SpeciesType SpeciesType { get; set; }

        public string SpeciesDescription { get; set; }

        public string AverageLifespan { get; set; }

        public string Behavior { get; set; }

        public string Habitat { get; set; }

        public string Size { get; set; }

        public string AverageWeight { get; set; }
        
    }
}
