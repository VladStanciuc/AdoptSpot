using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdoptSpot.Areas.Identity.Data;
using AdoptSpot.Data.Enums;

namespace AdoptSpot.Models
{
    public class UserPreferences
    {
        public int Id { get; set; }
        public Size PrefferedSize { get; set; }
        public LifeSpanInYears PrefferedLifeSpan { get; set; }
        public List<UserPreferenceTemperamentScore> UserPreferenceTemperamentScores { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
