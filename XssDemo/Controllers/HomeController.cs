using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XssDemo.Models;

namespace XssDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(BlogPost blogpost)
        {
            return View("List", new List<BlogPost> { blogpost });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class BlogPost
    {
        public string Content { get; set; }
        public string Image { get; set; }
    }
}