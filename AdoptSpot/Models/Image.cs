using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; }
    }
}
