using AdoptSpot.Areas.Identity.Data;
using AdoptSpot.Data;
using AdoptSpot.Data.Services;
using AdoptSpot.Models;
using AdoptSpot.Models.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Controllers
{
    public class PetRecommendationController : Controller
    {
        private readonly IPetService _petService;
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public PetRecommendationController(IPetService petService, AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _petService = petService;
            _context = context;
            _userManager = userManager;
        }
        public  IActionResult Index()
        {
            return View();
        }

        [HttpPost("UserPreferences/PetRecommendation")]
        public async Task<IActionResult> PetRecommendation(UserPreferencesViewModel userPreferencesViewModel)
        {
            var allPets = await _petService.GetAllPetsWithImages();
            UserPreferences userPreferences = new UserPreferences();
            userPreferences.PrefferedLifeSpan = userPreferencesViewModel.PreferredLifeSpan;
            userPreferences.PrefferedSize = userPreferencesViewModel.PreferredSize;
            userPreferences.UserPreferenceTemperamentScores = userPreferencesViewModel.UserPreferenceTemperamentScores;
           
            // Filter Breed Characteristics based on user's preferred size and lifespan
            var suitableBreeds = _context.BreedCharacteristics
                .Where(b => b.Size == userPreferences.PrefferedSize
                            && b.LifeSpanInYears == userPreferences.PrefferedLifeSpan)
                .Select(b => b.Name)
                .ToList();

            // Filter pets based on the suitable breeds
            var recommendedPets = allPets
                .Select(p => new PetWithBreedDTO(p, ComputeMatchScore(p, userPreferences)))
                .OrderByDescending(ps => ps.Score)
                .Select(ps => ps.Pet)
                .ToList();

            return View("Index", recommendedPets);
        }

        private  double ComputeMatchScore(Pet pet, UserPreferences userPreferences)
        {
            double score = 0.0;
            var petBreedCharacteristics =  _context.BreedCharacteristics
                .FirstOrDefault(p => p.Name == pet.BreedName);
            var breedTemperaments = _context.BreedTemperaments
                .Where(t => t.BreedId == petBreedCharacteristics.Id)
                .ToList();

            petBreedCharacteristics.BreedTemperaments = breedTemperaments;
            // Add points based on pet's size
            if (petBreedCharacteristics.Size == userPreferences.PrefferedSize)
            {
                score += 1.0;
            }

            // Add points based on pet's lifespan
            if (petBreedCharacteristics.LifeSpanInYears == userPreferences.PrefferedLifeSpan)
            {
                score += 1.0;
            }

            // Add points based on each temperament's score and weight
            foreach (var temperamentScore in userPreferences.UserPreferenceTemperamentScores)
            {
                var breedTemperament = petBreedCharacteristics.BreedTemperaments
                    .FirstOrDefault(bt => bt.TemperamentType == temperamentScore.Temperament);

                if (breedTemperament != null)
                {
                    // Here, we subtract from the score the absolute difference between the user's score 
                    // and the breed's score, weighted by the user's weight for this temperament.
                    // This means that the score will be higher when the user's score and the breed's score match closely.
                    score -= Math.Abs(breedTemperament.Score - temperamentScore.Score) * temperamentScore.Weight;
                }
            }

            return score;
        }

    }
}
