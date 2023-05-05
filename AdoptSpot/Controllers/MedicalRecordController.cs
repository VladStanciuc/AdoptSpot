using AdoptSpot.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Controllers
{
    public class MedicalRecordController: Controller
    {
        private readonly IMedicalRecordService _service;
        public MedicalRecordController(IMedicalRecordService service)
        {
            _service = service;
        }
    }
}
