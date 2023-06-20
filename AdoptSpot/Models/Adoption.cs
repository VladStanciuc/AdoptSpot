using AdoptSpot.Areas.Identity.Data;
using AdoptSpot.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Models
{
    [Table("Adoption")]
    public class Adoption : IEntityBase
    {
        [Key]
        public int Id { get; set; }
     
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        public DateTime AdoptionDate { get; set; }
        public string AdoptionStatus { get; set; }
        public string AdopterUserId { get; set; }
        public ApplicationUser AdopterUser { get; set; }

    }
}
