using AdoptSpot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.ViewModels
{
    public class UserAdoptionViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AdoptionStatus { get; set; }
        public List<Pet> AdoptedPets { get; set; }
    }
}
