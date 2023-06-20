using AdoptSpot.Areas.Identity.Data;
using AdoptSpot.Data;
using AdoptSpot.Data.Services;
using AdoptSpot.Models;
using AdoptSpot.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdoptSpot.Controllers
{
    public class AdoptionController : Controller
    {
        private readonly IAdoptionService _service;
        private readonly AppDbContext _context;
        private readonly IPetService _petService;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdoptionController(IAdoptionService service, AppDbContext context, IPetService petService, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _context = context;
            _petService = petService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create(int petId)
        {
            var viewModel = new AdoptionViewModel { PetId = petId };
            return View(viewModel);
        }
        [HttpPost]
        public async Task <IActionResult> Create(AdoptionViewModel viewModel)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Get the currently logged-in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Create a new Adoption object
            var adoption = new Adoption
            {
                AdopterUserId = userId,
                PetId = viewModel.PetId,
                AdoptionStatus = "Pending",
                AdoptionDate = DateTime.Now

            };

            // Save the adoption object to the database
           await  _service.AddAdoptionRequestAsync(adoption);


            // Redirect the user to a confirmation page
            return RedirectToAction("Index", "Pets");

        }
        [HttpGet]
        public async Task<IActionResult> UserAdoptions()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var user = await _userManager.GetUserAsync(User);
            if (userId == null)
            {
                return NotFound();
            }

            var petIds = await _service.GetAdoptionRequests(userId);

            List<Pet> adoptedPets = new List<Pet>();
            foreach (var petId in petIds)
            {
                var pet = await _petService.GetByIdWithImages(petId);
                if (pet != null)
                {
                    adoptedPets.Add(pet);
                }
            }

            var viewModel = new UserAdoptionViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber,
               
                AdoptedPets = adoptedPets
            };
            return View(viewModel);
        }

    }
    
}
