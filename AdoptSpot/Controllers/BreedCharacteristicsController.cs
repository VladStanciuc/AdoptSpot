using AdoptSpot.Data;
using AdoptSpot.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Controllers
{
    public class BreedCharacteristicsController : Controller
    {
        private readonly AppDbContext _context;

        public BreedCharacteristicsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var breedCharacteristics = _context.BreedCharacteristics.ToList();
            return View(breedCharacteristics);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BreedCharacteristics breedCharacteristics)
        {
            if (ModelState.IsValid)
            {
                _context.BreedCharacteristics.Add(breedCharacteristics);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(breedCharacteristics);
        }
    }
}
