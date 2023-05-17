﻿using AdoptSpot.Data.Base;
using AdoptSpot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Data.Services
{
    public class MedicalTreatmentService : EntityBaseRepository<MedicalTreatment>, IMedicalTreatmentService
    {
        public MedicalTreatmentService(AppDbContext context) : base(context)
        {

        }
    }
}
