using Microsoft.AspNetCore.Mvc;
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
				var firstError = ModelState.Values.SelectMany(v => v.Errors)
										  .Select(e => e.ErrorMessage)
										  .FirstOrDefault();

				return BadRequest(new { error = firstError ?? "Invalid input." });
			}

			// Should rewrite logic, because when you add a URL starting with "https", "www" will be added and so on.
			if (!model.Url.StartsWith("www.", StringComparison.OrdinalIgnoreCase) && !model.Url.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
			{
				model.Url = "www." + model.Url;
			}

			if (!model.Url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !model.Url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
			{
				model.Url = "https://" + model.Url;
			}
			//

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

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error(int statusCode)
		{
			if (statusCode == 404)
			{
				ViewData["StatusCode"] = statusCode;
				return View("Error404");
			}

			return View();
		}
	}
}
