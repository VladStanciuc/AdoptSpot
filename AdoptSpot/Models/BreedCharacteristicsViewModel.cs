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
        public Size PreferredSize { get; set; }
        public LifeSpanInYears PreferredLifeSpan { get; set; }

        public List<BreedTemperament> BreedTemperament { get; set; }
        public IEnumerable<int> Scores => Enumerable.Range(1, 5);
    }
}
