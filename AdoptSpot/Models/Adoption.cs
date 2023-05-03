using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Models
{
    [Table("Adoption")]
    public class Adoption
    {
        [Key]
        public int Id { get; set; }
     
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        public DateTime AdoptionDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
