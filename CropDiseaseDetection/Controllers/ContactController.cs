using CropDiseaseDetection.Models;
//using CropPulse.Data;
using CropPulse.Models; // Essential reference
using Microsoft.AspNetCore.Mvc;

namespace CropPulse.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(Contact model)
        {
            if (ModelState.IsValid)
            {
                _context.Contacts.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("Index", model);
        }
    }
}