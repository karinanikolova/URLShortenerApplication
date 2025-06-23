using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (!model.Url.StartsWith("www.", StringComparison.OrdinalIgnoreCase) && !model.Url.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
			{
				model.Url = "www." + model.Url;
			}

			if (!model.Url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !model.Url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
			{
				model.Url = "https://" + model.Url;
			}

			if (!model.Url.EndsWith("/", StringComparison.OrdinalIgnoreCase))
			{
				model.Url = model.Url + "/";
			}

			if (!await _urlService.OriginalUrlExistsAsync(model.Url))
			{
				await _urlService.AddUrlAsync(model.Url);
			}

			var urlViewModel = await _urlService.GetUrlViewModelByOriginalUrlAsync(model.Url);

			return Json(urlViewModel);
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
