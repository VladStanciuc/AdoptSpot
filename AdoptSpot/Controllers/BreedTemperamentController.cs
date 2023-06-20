using AdoptSpot.Data;
using AdoptSpot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Controllers
{
    public class BreedTemperamentController : Controller
    {
        private readonly AppDbContext _context;

        public BreedTemperamentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var breedTemperaments = _context.BreedTemperaments.ToList();
            return View(breedTemperaments);
        }

        public IActionResult Create()
        {
            ViewBag.BreedCharacteristics = new SelectList(_context.BreedCharacteristics, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(BreedTemperament breedTemperament)
        {
            if (ModelState.IsValid)
            {
                _context.BreedTemperaments.Add(breedTemperament);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BreedCharacteristics = new SelectList(_context.BreedCharacteristics, "Id", "Name", breedTemperament.BreedId);
            return View(breedTemperament);
        }
    }
}
