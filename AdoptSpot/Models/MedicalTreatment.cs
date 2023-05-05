using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Models
{

    [Table("MedicalTreatment")]
    public class MedicalTreatment
    {
        public int Id { get; set; }
        public int MedicalRecordId { get; set; }
        public DateTime TreatmentDate { get; set; }
        public string TreatmentDescription { get; set; }
        public string PrescribingVeterinarian { get; set; }
        public float Cost { get; set; }
        public string Diagnosis { get; set; }
        public string Medication { get; set; }
        public int Dosage { get; set; }
        public string DosageUnit { get; set; }
        public int Frequency { get; set; }
        public string FrequencyUnit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
        public MedicalRecord MedicalRecord { get; set; }

    }
}
