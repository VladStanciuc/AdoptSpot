﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AdoptSpot.Data.Base;
using AdoptSpot.Data.Enums;
namespace AdoptSpot.Models
{
    public class Pet : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public  PetGender PetGender { get; set; }
        public string Color { get; set; }
        public string Breed { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        // public DateTime UpdatedAt { get; set; }
        public ICollection<Adoption> Adoptions { get; set; }
        public ICollection<MedicalHistory> MedicalHistories { get; set; }
        public ICollection<Image> Images { get; set; }

        //Relationships
        public Pet()
        {
            Images = new List<Image>();
        }
    }
}