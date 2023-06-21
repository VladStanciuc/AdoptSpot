using AdoptSpot.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Models
{
    public class BreedCharacteristicsViewModel
    {
        public BreedCharacteristicsViewModel()
        {
            BreedTemperament = new List<BreedTemperament>();
        }
        public string Name { get; set; }
        public string CommonHealthIssues { get; set; }
        public string OtherDetails { get; set; }

        public Size PreferredSize { get; set; }
        public LifeSpanInYears PreferredLifeSpan { get; set; }

        public List<BreedTemperament> BreedTemperament { get; set; }
        public IEnumerable<int> Scores => Enumerable.Range(1, 5);
    }
}
