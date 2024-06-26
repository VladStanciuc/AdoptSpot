﻿using AdoptSpot.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Models
{

    [Table("MedicalTreatment")]
    public class MedicalTreatment : IEntityBase
    {
        public int Id { get; set; }
        public int MedicalRecordId { get; set; }
        public string TreatmentDescription { get; set; }
        public string PrescribingVeterinarian { get; set; }
        public float Cost { get; set; }
        public string Diagnosis { get; set; }
        public string Medication { get; set; }
        public string DosageAndUnit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        public bool IsPermanentTreatment()
        {
            return EndDate == DateTime.MaxValue;
        }

    }
}
