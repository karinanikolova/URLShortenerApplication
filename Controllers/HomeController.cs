using Microsoft.AspNetCore.Mvc;
using URLShortenerApp.Helpers.Contracts;
using URLShortenerApp.Models.URL;
using URLShortenerApp.Services.Contracts;

namespace URLShortenerApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUrlService _urlService;
		private readonly ICustomUrlHelper _customUrlHelper;

		public HomeController(ILogger<HomeController> logger, IUrlService urlService, ICustomUrlHelper customUrlHelper)
		{
			_logger = logger;
			_urlService = urlService;
			_customUrlHelper = customUrlHelper;
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

			string normalizedUrl;

			try
			{
				normalizedUrl = await _customUrlHelper.Normalize(model.Url);
			}
			catch (Exception ex)
			{
				return BadRequest(new { error = ex.Message });
			}

			if (!await _urlService.OriginalUrlExistsAsync(normalizedUrl))
			{
				await _urlService.AddUrlAsync(normalizedUrl);
			}

			var urlViewModel = await _urlService.GetUrlViewModelByOriginalUrlAsync(normalizedUrl);

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
