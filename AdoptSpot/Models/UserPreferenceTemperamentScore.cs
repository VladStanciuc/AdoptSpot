using AdoptSpot.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Models
{
    public class UserPreferenceTemperamentScore
    {
        public int Id { get; set; }
        public Temperament Temperament { get; set; }
        [Range(1, 5, ErrorMessage = "Score must be between 1 and 5.")]
        public int Score { get; set; }
        public int UserPreferencesId { get; set; }
        public UserPreferences UserPreferences { get; set; }
    }
}
