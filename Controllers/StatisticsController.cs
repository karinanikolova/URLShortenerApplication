using Microsoft.AspNetCore.Mvc;
using URLShortenerApp.Helpers;
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

		[HttpGet("/{shortenedUrl:shortUrl}")] 
		public async Task<IActionResult> Stats(string shortenedUrl)
		{
			if (string.IsNullOrWhiteSpace(shortenedUrl) || !await _urlService.IsShortenedUrlValidAsync(shortenedUrl))
			{
				return NotFound();
			}

			var urlId = await _urlService.GetUrlIdByShortenedUrlAsync(shortenedUrl);

			if (urlId == Guid.Empty)
			{
				return NotFound();
			}

			var userIpAddress = IpAddressHelper.GetUserIpAddress(HttpContext);

			if (await _urlService.HasUrlOpenBeenRecordedTodayAsync(urlId, userIpAddress, DateTime.UtcNow) == false)
			{
				await _urlService.RecordUrlOpensAsync(urlId, userIpAddress);
			}

			var urlStatisticsModel = await _urlService.GetUrlStatisticsViewModelAsync(urlId);

			return View(urlStatisticsModel);
		}
	}
}
