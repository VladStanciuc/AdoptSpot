using AdoptSpot.Data;
using AdoptSpot.Data.Enums;
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
        // private readonly UserManager<ApplicationUser> _userManager;

        public BreedCharacteristicsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: BreedCharacteristics/Create
        public IActionResult Create()
        {
            var viewModel = new BreedCharacteristicsViewModel
            {
                BreedTemperament = Enum.GetValues(typeof(Temperament))
                                               .Cast<Temperament>()
                                               .Select(t => new BreedTemperament { TemperamentType = t })
                                               .ToList()
            };

            return View(viewModel);
        }

        // POST: UserPreferences/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BreedCharacteristicsViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var breedCharacteristics = new BreedCharacteristics
                {
                    Size = viewModel.PreferredSize,
                    LifeSpanInYears = viewModel.PreferredLifeSpan,
                    Name = viewModel.Name,
                    CommonHealthIssues = viewModel.CommonHealthIssues,
                    OtherDetails = viewModel.OtherDetails
                };

                _context.Add(breedCharacteristics);
                await _context.SaveChangesAsync();

                foreach (var score in viewModel.BreedTemperament)
                {
                    score.BreedId = breedCharacteristics.Id;
                    _context.Add(score);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }
    }
}
