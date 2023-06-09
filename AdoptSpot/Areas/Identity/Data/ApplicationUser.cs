using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AdoptSpot.Data.Enums;
using AdoptSpot.Models;
using Microsoft.AspNetCore.Identity;

namespace AdoptSpot.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
       
        public string Password { get; set; }
        [PersonalData]
        [Column(TypeName ="nvarchar(100")]
        public string FirstName { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100")]
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public UserRole UserRole { get; set; }

        public ICollection<Adoption> Adoptions { get; set; }
    }
}
