using AdoptSpot.Data.Services;
using AdoptSpot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
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
                        pet.Images = new List<ImageModel>();
                    }

                    // Process each image in the list
                    foreach (var image in images)
                    {
                        if (image != null && image.Length > 0)
                        {
                            using var memoryStream = new MemoryStream();
                            await image.CopyToAsync(memoryStream);

                            var img = new ImageModel
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
            var petDetails = await _service.GetByIdAsync(id, include: p => p.Include(p => p.Images)
                                                                   .Include(p => p.MedicalRecord)
                                                                      .ThenInclude(mr => mr.Vaccinations)
                                                                   .Include(p => p.MedicalRecord)
                                                                      .ThenInclude(mr => mr.MedicalTreatments));


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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TypeId,SpeciesType,Age,PetGender,Color,Breed,Description,CreatedAt,Adoptions,MedicalRecord,Images")] Pet pet, 
            List<IFormFile> newImages, [Bind(Prefix = "MedicalRecord")] MedicalRecord updatedMedicalRecord, 
            [Bind(Prefix = "MedicalRecord.MedicalTreatments")] ICollection<MedicalTreatment> updatedMedicalTreatments,
             [Bind(Prefix = "MedicalRecord.Vaccines")] ICollection<Vaccination> updatedVaccinations)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            var petToUpdate = await _service.GetByIdAsync(id, include: p => p.Include(p => p.Images)
                                                                   .Include(p => p.MedicalRecord)
                                                                      .ThenInclude(mr => mr.Vaccinations)
                                                                   .Include(p => p.MedicalRecord)
                                                                      .ThenInclude(mr => mr.MedicalTreatments));

            if (petToUpdate == null)
            {
                return NotFound();
            }
            petToUpdate.Name = pet.Name;
            petToUpdate.SpeciesType = pet.SpeciesType;
            petToUpdate.Age = pet.Age;
            petToUpdate.PetGender = pet.PetGender;
            petToUpdate.Color = pet.Color;
            petToUpdate.BreedName = pet.BreedName;
            petToUpdate.Description = pet.Description;
            petToUpdate.CreatedAt = pet.CreatedAt;
            await  _service.UploadImages(petToUpdate, newImages);
           
            await _service.UpdateExistingVaccinationsAsync(petToUpdate, updatedVaccinations);
          
            await _service.UpdateAsync(id, petToUpdate);

            return RedirectToAction("Index");
        }
      

        [HttpPost]
        [Route("Edit/AddMedicalTreatment/{petId}")]
        public async Task<IActionResult> AddMedicalTreatment(int petId, [FromBody] MedicalTreatment medicalTreatment)
        {
            var petToUpdate = await _service.GetByIdAsync(petId, include: p => p.Include(p => p.MedicalRecord).ThenInclude(mt => mt.MedicalTreatments));
            if (petToUpdate == null)
            {
                return NotFound();
            }

            try
            {
                await _service.AddMedicalTreatmentAsync(petToUpdate, medicalTreatment);
                return Ok(new { status = "success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }
        [HttpPost]
        [Route("Edit/UpdateMedicalTreatment/{petId}")]
        public async Task<IActionResult> UpdateMedicalTreatment(int petId, [FromBody] MedicalTreatment updatedMedicalTreatment)
        {
          
            var petToUpdate = await _service.GetByIdAsync(petId, include: p => p.Include(p => p.MedicalRecord).ThenInclude(mt => mt.MedicalTreatments));
            if (petToUpdate == null)
            {
                return NotFound();
            }
           
            await _service.UpdateExistingMedicalTreatments(petToUpdate, updatedMedicalTreatment);
            return Ok();
        }
        [HttpDelete]
        [Route("Edit/DeleteMedicalTreatment/{petId}/{medicalTreatmentId}")]
        public async Task<IActionResult> DeleteMedicalTreatment(int petId, int medicalTreatmentId)
        {
            var petToUpdate = await _service.GetByIdAsync(petId, include: p => p.Include(p => p.MedicalRecord).ThenInclude(mt => mt.MedicalTreatments));
            try
            {
                await _service.DeleteMedicalTreatmentAsync(petToUpdate, medicalTreatmentId);
                return Json(new { status = "success" });
            }
            catch (Exception)
            {
                return Json(new { status = "error" });
            }
        }

        [HttpDelete]
        [Route("DeleteImages/{petId}")]
        public async Task<IActionResult> DeleteImages(int petId, [FromBody] int[] imageIds)
        {
            var pet = await _service.GetByIdAsync(petId, include: p => p.Include(p => p.Images));

            if (pet == null)
            {
                return NotFound();
            }

            try
            {
                await _service.DeleteImagesAsync(pet, imageIds);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                // You could return a more specific status code depending on the exception
                return NotFound();
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
            var pet = await _service.GetByIdAsync(id, include: p => p.Include(p => p.Images)
                                                                   .Include(p => p.MedicalRecord)
                                                                      .ThenInclude(mr => mr.Vaccinations)
                                                                   .Include(p => p.MedicalRecord)
                                                                      .ThenInclude(mr => mr.MedicalTreatments));
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
                var petToUpdate = await _service.GetByIdAsync(petId, include: p => p.Include(p => p.MedicalRecord));
                if (petToUpdate == null)
                {
                    return NotFound();
                }

                await _service.AddVaccinationAsync(petToUpdate, vaccination);
                return Ok();
        }

        [HttpDelete]
        [Route("EditVaccinationDelete/{vaccineId}")]
        public async Task<IActionResult> DeleteVaccination(int vaccineId)
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
        [Route("Pets/AddVaccinationAsync")]
        public IActionResult _AddVaccination(int index)
        {
            ViewData["Index"] = index;
            return PartialView("_AddVaccination", new Vaccination());
        }


    


    }
}
