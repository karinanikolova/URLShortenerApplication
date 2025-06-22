using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using URLShortenerApp.Models;
using URLShortenerApp.Models.URL;
using URLShortenerApp.Services.Contracts;

namespace URLShortenerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUrlService _urlService;

		public HomeController(ILogger<HomeController> logger, IUrlService urlService)
        {
            _logger = logger;
			_urlService = urlService;
		}

        [HttpGet]
		public async Task<IActionResult> Index()
        {
            return View(new URLAddModel());
        }

		[HttpPost]
		public async Task<IActionResult> Index(URLAddModel model)
		{
			// To be implemented: Check if the URL already exists in the database
			if (!ModelState.IsValid)
			{
				return View(model);
			}

            await _urlService.AddUrl(model.Url);

			return Ok();
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
