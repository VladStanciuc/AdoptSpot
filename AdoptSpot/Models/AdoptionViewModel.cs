using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Models
{
    public class AdoptionViewModel
    {
        [Required]
        public string ApplicantName { get; set; }
        
        [Required]
        public string ApplicantEmail { get; set; }
        
        [Required]
        public string ApplicantPhoneNumber { get; set; }
        
        [Required]
        public string ApplicantAddress { get; set; }
        public int PetId { get; set; }

        // Adoption-specific information
        public string WhyDoYouWantToAdopt { get; set; }  // Why the applicant wants to adopt
        public string PetExperience { get; set; }  // Applicant's past experience with pets
        public bool HasOtherPets { get; set; }  // Does the applicant have other pets?
        public string OtherPetsInformation { get; set; }  // Information about the applicant's other pets
        public bool HasYoungChildren { get; set; }  // Does the applicant have young children?
        public string HomeEnvironmentDescription { get; set; }  // Description of the home environment
    }
}
