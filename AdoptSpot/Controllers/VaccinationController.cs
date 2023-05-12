using AdoptSpot.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Controllers
{
    public class VaccinationController : Controller
    {
        private readonly IVaccinationService _service;
        public VaccinationController(IVaccinationService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
