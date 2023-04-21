using AdoptSpot.Data;
using AdoptSpot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Controllers
{
    [Route("[controller]s")]
    public class PetController : Controller
    {
        private readonly AppDbContext _context;
        public PetController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Pet> pets = _context.Pets.Include(p => p.Images).ToList(); // Include related Images; assuming you have a DbContext named _context
            return View(pets);
        }

        public ActionResult carousel()
        {
            return View();
        }
    }
}
