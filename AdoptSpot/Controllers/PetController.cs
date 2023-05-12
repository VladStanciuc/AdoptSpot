﻿using AdoptSpot.Data.Services;
using AdoptSpot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
            var data = await _service.GetAllAsync(p => p.Images);
            
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
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("Name,TypeId,Species,Age,PetGender,Color,Breed,Description,CreatedAt,Adoptions,MedicalRecord,Images")] Pet pet, List<IFormFile> images)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    // Check if the pet.Images property is null
                    if (pet.Images == null)
                    {
                        pet.Images = new List<Image>();
                    }

                    // Process each image in the list
                    foreach (var image in images)
                    {
                        if (image != null && image.Length > 0)
                        {
                            using var memoryStream = new MemoryStream();
                            await image.CopyToAsync(memoryStream);

                            var img = new Image
                            {
                                FileName = image.FileName,
                                ContentType = image.ContentType,
                                Data = memoryStream.ToArray()
                            };
                            pet.Images.Add(img);

                        }

                    }

                    // Save the Pet instance with the associated images to the database
                    await _service.AddAsync(pet);

                    return RedirectToAction(nameof(Index));
                }

            }
            return View(pet);
        }

        //Get:Pet/Edit/1

        //Get:Pet/Edit/id
        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var petDetails = await _service.GetByIdAsync(id, include: p => p.Include(p => p.Images).Include(p => p.MedicalRecord).ThenInclude(p => p.Vaccinations));


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
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TypeId,Species,Age,PetGender,Color,Breed,Description,CreatedAt,Adoptions,MedicalRecord,Images")] Pet pet, 
            List<IFormFile> newImages, [Bind(Prefix = "MedicalRecord")] MedicalRecord updatedMedicalRecord, 
            [Bind(Prefix = "MedicalRecord.MedicalTreatments")] ICollection<MedicalTreatment> updatedMedicalTreatments, 
            [Bind(Prefix = "MedicalRecord.Vaccines")] ICollection<Vaccination> updatedVaccinations)
           
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

           
            var petToUpdate = await _service.GetByIdAsync(id, include: p => p.Include(p => p.Images)
                                                                             .Include(p =>p.MedicalRecord)
                                                                             .ThenInclude(mr => mr.Vaccinations));
            if (petToUpdate == null)
            {
                return NotFound();
            }
           await  _service.UploadImages(petToUpdate, newImages);
            
            foreach (var updatedVaccination in updatedVaccinations)
            {
                var existingVaccination = petToUpdate.MedicalRecord.Vaccinations.FirstOrDefault(v => v.Id == updatedVaccination.Id);

                if (existingVaccination != null)
                {
                    existingVaccination.Disease = updatedVaccination.Disease;
                    existingVaccination.DateAdministered = updatedVaccination.DateAdministered;
                    existingVaccination.VeterinarianName = updatedVaccination.VeterinarianName;
                    existingVaccination.ExpirationDate = updatedVaccination.ExpirationDate;
                    existingVaccination.BatchNumber = updatedVaccination.BatchNumber;
                    existingVaccination.Notes = updatedVaccination.Notes;
                }
            }

            await _service.UpdateAsync(id, petToUpdate);
            return RedirectToAction(nameof(Index));
        }
     
        [HttpDelete]
        [Route("EditVaccinationDelete/{vaccineId}")]
        public async Task<IActionResult> DeleteVaccination( int vaccineId)
        {
            var result = await _service.DeleteVaccinationAsync(vaccineId);
            if (result)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
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
       
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var pet = await _service.GetByIdAsync(id, include: p => p.Include(p => p.Images));

            if (pet == null)
            {
                return View("NotFound");
            }

            return View(pet);
        }
       
        [HttpPost]
        [Route("EditVaccination/{petId}")]
        public async Task<IActionResult> AddVaccinationAsync(int petId, [FromBody] Vaccination vaccination)
        {
            try
            {
                var petToUpdate = await _service.GetByIdAsync(petId, include: p => p.Include(p => p.MedicalRecord));
                if (petToUpdate == null)
                {
                    return NotFound();
                }

                await _service.AddVaccinationAsync(petToUpdate, vaccination);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception here, e.g., using a logger or writing to a file.
                return StatusCode(500, "An error occurred while adding the vaccination.");
            }
        }
        [HttpGet]
        [Route("Pets/AddVaccinationAsync")]
        public IActionResult _AddVaccination(int index)
        {
            ViewData["Index"] = index;
            return PartialView("_AddVaccination", new Vaccination());
        }



    }
}
