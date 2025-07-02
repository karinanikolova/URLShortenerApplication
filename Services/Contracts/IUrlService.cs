using URLShortenerApp.Models.Statistics;
using URLShortenerApp.Models.URL;

namespace URLShortenerApp.Services.Contracts
{
	public interface IUrlService
	{
		Task AddUrlAsync(string url, DateTime creationDate);

		Task<bool> OriginalUrlExistsAsync(string url);

		Task<URLViewModel> GetUrlViewModelByUrlAndCreationDateAsync(string url, DateTime creationDate);

		Task<bool> IsSecretUrlValidAsync(string secretUrl);

		Task RecordUrlOpensAsync(Guid urlId, string ipAddress, DateTime accessDate);

		Task<Guid> GetUrlIdBySecretUrlAsync(string secretUrl);

		Task<Guid> GetUrlIdByShortenedUrlAsync(string shortenedUrl);

		Task<bool> HasUrlOpenBeenRecordedTodayAsync(Guid urlId, string ipAddress, DateTime dateTimeToday);

		Task<UrlStatisticsViewModel> GetUrlStatisticsViewModelAsync(Guid urlId);

		Task<string> GetOriginalUrlByShortenedUrlAsync(string shortenedUrl);
	}
}
