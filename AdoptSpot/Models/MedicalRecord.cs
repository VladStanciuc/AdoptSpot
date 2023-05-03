using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int VaccinationId { get; set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        public ICollection<MedicalTreatment> MedicalTreatments { get; set; }
        public ICollection<Vaccination> Vaccines { get; set; }
 
       
        public MedicalRecord()
        {
            MedicalTreatments = new List<MedicalTreatment>();
            Vaccines = new List<Vaccination>();
        }
       
    }
}
