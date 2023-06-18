using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AdoptSpot.Data.Base;
using AdoptSpot.Data.Enums;
namespace AdoptSpot.Models
{
    [Table("Pet")]
    public class Pet : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public SpeciesType SpeciesType { get; set; }
        public int Age { get; set; }
        public  PetGender PetGender { get; set; }
        public string Color { get; set; }
        public string Breed { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public ICollection<Adoption> Adoptions { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        public ICollection<ImageModel> Images { get; set; }

        //Relationships
        public Pet()
        {
            Images = new List<ImageModel>();
            MedicalRecord = new MedicalRecord();
        }
       
    }
}
