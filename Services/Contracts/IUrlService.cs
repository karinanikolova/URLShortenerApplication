using URLShortenerApp.Models.Statistics;
using URLShortenerApp.Models.URL;

namespace URLShortenerApp.Services.Contracts
{
	public interface IUrlService
	{
		Task AddUrlAsync(string url);

		Task<bool> OriginalUrlExistsAsync(string url);

		Task<URLViewModel> GetUrlViewModelByOriginalUrlAsync(string url);

		Task<bool> IsShortenedUrlValidAsync(string shortenedUrl);

		Task RecordUrlOpensAsync(Guid urlId, string ipAddress, DateTime accessDate);

		Task<Guid> GetUrlIdByShortenedUrlAsync(string shortenedUrl);

		Task<bool> HasUrlOpenBeenRecordedTodayAsync(Guid urlId, string ipAddress, DateTime dateTimeToday);

		Task<UrlStatisticsViewModel> GetUrlStatisticsViewModelAsync(Guid urlId);
	}
}
