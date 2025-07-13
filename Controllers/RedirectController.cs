using Microsoft.AspNetCore.Mvc;
using URLShortenerApp.BackgroundServices;
using URLShortenerApp.Helpers;
using URLShortenerApp.Services.Contracts;

namespace URLShortenerApp.Controllers
{
	public class RedirectController : Controller
	{
		private readonly IUrlService _urlService;
		private readonly IUrlAccessQueue _accessQueue;

		public RedirectController(IUrlService urlService, IUrlAccessQueue accessQueue)
		{
			_urlService = urlService;
			_accessQueue = accessQueue;
		}

		[HttpGet("/{shortenedUrl}")]
		public async Task<IActionResult> RedirectTo(string shortenedUrl)
		{
			var originalUrl = await _urlService.GetOriginalUrlByShortenedUrlAsync(shortenedUrl);

			if (originalUrl == null)
			{
				return NotFound("Short URL not found.");
			}

			var urlId = await _urlService.GetUrlIdByShortenedUrlAsync(shortenedUrl);

			if (urlId == Guid.Empty)
			{
				return NotFound();
			}

			var userIpAddress = IpAddressHelper.GetUserIpAddress(HttpContext);

			if (await _urlService.HasUrlOpenBeenRecordedTodayAsync(urlId, userIpAddress, DateTime.UtcNow.Date) == false)
			{
				_accessQueue.Enqueue(new UrlAccessLogDto()
				{
					URLId = urlId,
					UserIPAddress = userIpAddress,
					AccessDate = DateTime.UtcNow.Date
				});
			}

			return Redirect(originalUrl);
		}
	}
}
