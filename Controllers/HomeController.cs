using ASP121.Models;
using ASP121.Services.Hash;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP121.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHashService _hashService;

        public HomeController(ILogger<HomeController> logger, IHashService hashService)
        {
            _logger = logger;
            _hashService = hashService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Intro()
        {
            return View();
        }
        public IActionResult Basics()
        {
            Models.Home.BasicsModel model = new()
            {
                Moment = DateTime.Now
            };
            return View(model);
        }

        public IActionResult Razor()
        {
            Models.Home.RazorModel model = new()
            {
                IntValue = 10,
                BoolValue = true,
                ListValue = new() {"String 1", "String 2", "String 3", "String 4" }
            };
            return View(model);
        }

        public ViewResult Services()
        {
            ViewData["hash"] = _hashService.HashString("123");
            ViewData["obj"] = _hashService.GetHashCode();
            ViewData["ctr"] = this.GetHashCode();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}