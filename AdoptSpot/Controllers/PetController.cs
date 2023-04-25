using AdoptSpot.Data;
using AdoptSpot.Data.Services;
using AdoptSpot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Controllers
{
    [Route("Pets")]
    public class PetController : Controller
    {
        private readonly IPetService _service;
        public PetController(IPetService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("")] // Empty route for the Index action
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }
        //Get:Pet/Create
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("Name,TypeId,Species,Age,PetGender,Color,Breed,Description,CreatedAt,Adoptions,MedicalHistories,Images")] Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return View(pet);
            }
            await _service.AddAsync(pet);

            return RedirectToAction(nameof(Index));
        }

        //Get:Pet/Edit/1

        //Get:Pet/Edit/id
        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(actorDetails);
            }

        }
        [HttpPost]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TypeId,Species,Age,PetGender,Color,Breed,Description,CreatedAt,Adoptions,MedicalHistories,Images")] Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return View(pet);
            }
            await _service.UpdateAsync(id, pet);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("Delete/{id}")]  
        public async Task<IActionResult> Delete(int id)
        {
            var petDetails = await _service.GetByIdAsync(id);

            if (petDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(petDetails);
            }

        }
        [HttpPost]
        [Route("Delete/{id}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var petDetails = await _service.GetByIdAsync(id);

            if (petDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                await _service.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }

        }

    }
}
