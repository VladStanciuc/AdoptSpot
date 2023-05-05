using AdoptSpot.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Models
{
    public class MedicalRecord:IEntityBase
    {
        public int Id { get; set; }
        public int VaccinationId { get; set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        public ICollection<MedicalTreatment> MedicalTreatments { get; set; }
        public ICollection<Vaccination> Vaccinations { get; set; }
 
       
        public MedicalRecord()
        {
            MedicalTreatments = new List<MedicalTreatment>();
            Vaccinations = new List<Vaccination>();
        }
       
    }
}
