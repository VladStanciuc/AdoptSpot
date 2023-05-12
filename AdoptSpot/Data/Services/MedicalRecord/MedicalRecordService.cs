using AdoptSpot.Data.Base;
using AdoptSpot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Data.Services
{
    public class MedicalRecordService:EntityBaseRepository<MedicalRecord>, IMedicalRecordService
    {
        public MedicalRecordService(AppDbContext context) : base(context)
        {

        }

    }
}

