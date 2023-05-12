using AdoptSpot.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Models
{
    public class Vaccination : IEntityBase
    {
        public int Id { get; set; }
        public string Disease { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateAdministered { get; set; }
        public string VeterinarianName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }
        public string BatchNumber { get; set; }
        public string Notes { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        public int MedicalRecordId { get; set; }
    }
}