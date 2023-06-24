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

        public ViewResult Azure()
        {
            return View();
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
        public ViewResult API()
        {
            return View();
        }
        public ViewResult Services()
        {
            ViewData["hash"] = _hashService.HashString("123");
            ViewData["obj"] = _hashService.GetHashCode();
            ViewData["ctr"] = this.GetHashCode();
            return View();
        }
        public ViewResult Sessions(String? userstring)
        {
            if(userstring != null)  // є дані від форми
            {
                HttpContext.Session.SetString("StoredString", userstring);
            }

            if(HttpContext.Session.Keys.Contains("StoredString"))
            {
                ViewData["StoredString"] = HttpContext.Session.GetString("StoredString");
            }
            else
            {
                ViewData["StoredString"] = "У сесії немає збережених даних";
            }


            if (HttpContext.Session.Keys.Contains("Form2String"))
            {
                ViewData["Form2String"] = HttpContext.Session.GetString("Form2String");
            }
            else
            {
                ViewData["Form2String"] = "У сесії немає збережених даних";
            }
            return View();
        }

        public RedirectToActionResult SessionsForm(String? userstring)
        {
            // цей метод приймає дані від другої форми і надсилає Redirect
            // Але для того щоб дані "userstring" були доступні після перезапиту,
            // їх необхідно зберегти у сесії
            HttpContext.Session.SetString("Form2String", userstring!);
            return RedirectToAction("Sessions");
            /* Sessions       userstring
             *  Form1 -----------------------> Sessions -> HTML (/sessions?userstring=123)
             *  
             *  
             *                userstring
             *  Form2 -----------------------> SessionsForm -> 302 (Redirect)
             *           redirect to Sessions
             *        <-----------------------      Сесія зберігає дані між запитами
             *  Browser     -(немає даних)-
             *        -----------------------> Sessions -> HTML (/sessions)
             */

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