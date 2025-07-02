using Microsoft.AspNetCore.Mvc;
using URLShortenerApp.Services.Contracts;

namespace URLShortenerApp.Controllers
{
	public class StatisticsController : Controller
	{
		private readonly IUrlService _urlService;

		public StatisticsController(IUrlService urlService)
		{
			_urlService = urlService;
		}

		[HttpGet("/{secretUrl:secretUrl}")]
		public async Task<IActionResult> Stats(string secretUrl)
		{
			if (string.IsNullOrWhiteSpace(secretUrl) || !await _urlService.IsSecretUrlValidAsync(secretUrl))
			{
				return NotFound();
			}

			var urlId = await _urlService.GetUrlIdBySecretUrlAsync(secretUrl);

			if (urlId == Guid.Empty)
			{
				return NotFound();
			}

			var urlStatisticsModel = await _urlService.GetUrlStatisticsViewModelAsync(urlId);

			return View(urlStatisticsModel);
		}
	}
}