using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Models
{
    public class MedicalHistory
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        public string Vaccination { get; set; }
        public DateTime VaccinationDate { get; set; }
        public string MedicalTreatment { get; set; }
        public DateTime TreatmentDate { get; set; }
    }
}
