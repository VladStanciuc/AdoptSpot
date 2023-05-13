using AdoptSpot.Data;
using AdoptSpot.Data.Services;
using AdoptSpot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AdoptSpot.Controllers
{
    [Route("Image")]
    public class ImageController : Controller
    {
        private readonly IImageService _service;

        public ImageController(IImageService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var images = await _service.GetAllAsync();
            return View(images);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(IFormFile image)
        {
            if (ModelState.IsValid && image != null && image.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);

                var img = new ImageModel
                {
                    FileName = image.FileName,
                    ContentType = image.ContentType,
                    Data = memoryStream.ToArray()
                };

                await _service.AddAsync(img);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var imageDetails = await _service.GetByIdAsync(id);

            if (imageDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(imageDetails);
            }
        }

        [HttpPost]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (image != null && image.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);

                var img = new ImageModel
                {
                    Id = id,
                    FileName = image.FileName,
                    ContentType = image.ContentType,
                    Data = memoryStream.ToArray()
                };

                await _service.UpdateAsync(id, img);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var imageDetails = await _service.GetByIdAsync(id);

            if (imageDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(imageDetails);
            }
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imageDetails = await _service.GetByIdAsync(id);

            if (imageDetails == null)
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
        [Route("GetImage/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var image = await _service.GetByIdAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            return File(image.Data, image.ContentType);
        }
    }
}
