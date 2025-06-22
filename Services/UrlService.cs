using URLShortenerApp.Data.Models;
using URLShortenerApp.Data.Utilities.Contracts;
using URLShortenerApp.Services.Contracts;

namespace URLShortenerApp.Services
{
	public class UrlService : IUrlService
	{
		private readonly IRepository _repository;

		public UrlService(IRepository repository)
		{
			_repository = repository;
		}

		public async Task AddUrl(string url)
		{
			var urlEntity = new URL()
			{
				OriginalUrl = url,
				ShortenedUrl = GenerateShortenedUrl(url),
				CreationDate = DateTime.UtcNow
			};

			await _repository.AddAsync<URL>(urlEntity);
			await _repository.SaveChangesAsync();
		}

		private string GenerateShortenedUrl(string longUrl)
		{
			//To be implemented

			return "";
		}
	}
}
