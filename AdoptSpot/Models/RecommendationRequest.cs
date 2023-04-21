using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Models
{
    public class RecommendationRequest
    {
        public int Size { get; set; }
        public int MonthlyCost { get; set; }
        public string Personality { get; set; }
        public ICollection<string> Vaccinations { get; set; }
        public ICollection<string> MedicalTreatments { get; set; }
    }
}
