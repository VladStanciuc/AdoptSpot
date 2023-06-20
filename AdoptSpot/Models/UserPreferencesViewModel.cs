using AdoptSpot.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Models
{
    public class UserPreferencesViewModel
    {
        public UserPreferencesViewModel()
        {
            UserPreferenceTemperamentScores = new List<UserPreferenceTemperamentScore>();
        }
        public Size PreferredSize { get; set; }
        public LifeSpanInYears PreferredLifeSpan { get; set; }

        public List<UserPreferenceTemperamentScore> UserPreferenceTemperamentScores { get; set; }
        public IEnumerable<int> Scores => Enumerable.Range(1, 5);
    }
}
