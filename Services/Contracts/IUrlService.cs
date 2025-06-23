using URLShortenerApp.Models.URL;

namespace URLShortenerApp.Services.Contracts
{
	public interface IUrlService
	{
		Task AddUrlAsync(string url);

		Task<bool> OriginalUrlExistsAsync(string url);

		Task<URLViewModel> GetUrlViewModelByOriginalUrlAsync(string url);
	}
}
