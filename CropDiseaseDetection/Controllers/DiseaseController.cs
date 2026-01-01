using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CropDiseaseDetection.Models; // This allows access to the Disease class
using Microsoft.AspNetCore.Mvc;
//using CropPulse.Models;
using Microsoft.EntityFrameworkCore;

namespace CropPulse.Controllers
{
    public class DiseaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiseaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Upload() => View();

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile imageFile, string crop)
        {
            if (imageFile == null || imageFile.Length == 0) 
            {
                ModelState.AddModelError("", "Please select a valid crop image.");
                return View();
            }

            // 1. Save the Image to wwwroot/uploads
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // 2. Analyze (Lookup database based on the crop type selected)
            var diseaseInfo = await _context.Diseases
                .FirstOrDefaultAsync(d => d.Crop == crop);

            if (diseaseInfo != null)
            {
                ViewBag.ImageUrl = "/uploads/" + fileName;
                return View("Result", diseaseInfo);
            }

            return RedirectToAction("Upload");
        }
    }
}