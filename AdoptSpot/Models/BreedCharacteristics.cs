using AdoptSpot.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Models
{
    public class BreedCharacteristics
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }


        public LifeSpanInYears LifeSpanInYears { get; set; }


        [Required]
        [StringLength(50)]
        public Size Size { get; set; }
    
        public string CommonHealthIssues { get; set; }

        public string OtherDetails { get; set; }

        public List<BreedTemperament> BreedTemperaments { get; set; }
    }
}
