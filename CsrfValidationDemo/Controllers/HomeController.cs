using CsrfValidationDemo.Models;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CsrfValidationDemo.Controllers
{
    public class Model
    {
        public string Data1 { get; set; }
        public string Data2 { get; set; }
    }

    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Process()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Process(Model model)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}