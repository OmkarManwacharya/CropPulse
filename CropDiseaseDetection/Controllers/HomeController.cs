using CropDiseaseDetection.Models;
//using CropPulse.Models; // Use the new namespace
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CropPulse.Controllers
{
    public class HomeController : Controller
    {
        // MAIN LANDING PAGE
        public IActionResult Index() => View();

        // NEW ABOUT SECTION
        public IActionResult About() => View();

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}