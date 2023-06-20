using AdoptSpot.Areas.Identity.Data;
using AdoptSpot.Data;
using AdoptSpot.Data.Enums;
using AdoptSpot.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace AdoptSpot.Controllers
{
    public class UserPreferencesController : Controller
    {
        private readonly AppDbContext _context;
        // private readonly UserManager<ApplicationUser> _userManager;

        public UserPreferencesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserPreferences/Create
        public IActionResult Create()
        {
            var viewModel = new UserPreferencesViewModel
            {
                UserPreferenceTemperamentScores = Enum.GetValues(typeof(Temperament))
                                               .Cast<Temperament>()
                                               .Select(t => new UserPreferenceTemperamentScore { Temperament = t })
                                               .ToList()
            };

            return View(viewModel);
        }

        // POST: UserPreferences/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserPreferencesViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var userPreferences = new UserPreferences
                {
                    PrefferedSize = viewModel.PreferredSize,
                    PrefferedLifeSpan = viewModel.PreferredLifeSpan,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                _context.Add(userPreferences);
                await _context.SaveChangesAsync();

                foreach (var score in viewModel.UserPreferenceTemperamentScores)
                {
                    score.UserPreferencesId = userPreferences.Id;
                    _context.Add(score);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

    }
}
