using KinadecWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KinadecWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


		public IActionResult About()
		{
			return View(); // returns Views/Home/About.cshtml
		}

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Mission()
        {
            return View();
        }

		public IActionResult Gallery()
		{
			return View();
		}

		public IActionResult Vision()
		{
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
