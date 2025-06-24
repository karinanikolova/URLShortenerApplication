using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Text;
using URLShortenerApp.Data.Models;
using URLShortenerApp.Data.Utilities.Contracts;
using URLShortenerApp.Models.Statistics;
using URLShortenerApp.Models.URL;
using URLShortenerApp.Services.Contracts;

namespace URLShortenerApp.Services
{
	public class UrlService : IUrlService
	{
		private const string Base62Chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

		private readonly IRepository _repository;

		public UrlService(IRepository repository)
		{
			_repository = repository;
		}

		public async Task AddUrlAsync(string url)
		{
			var urlEntity = new URL()
			{
				OriginalUrl = url,
				ShortenedUrl = await GenerateShortenedUrlAsync(url),
				CreationDate = DateTime.UtcNow
			};

			await _repository.AddAsync<URL>(urlEntity);
			await _repository.SaveChangesAsync();
		}

		public async Task<Guid> GetUrlIdByShortenedUrlAsync(string shortenedUrl) =>
			await _repository.AllReadOnly<URL>()
				.Where(u => u.ShortenedUrl == shortenedUrl)
				.Select(u => u.Id)
				.FirstOrDefaultAsync();

		public async Task<UrlStatisticsViewModel> GetUrlStatisticsViewModelAsync(Guid urlId)
		{
			var recordViewModels = await _repository.AllReadOnly<Record>()
				.Where(r => r.URLId == urlId)
				.Select(r => new RecordViewModel()
				{
					UserIPAddress = r.UserIPAddress,
					AccessDate = r.AccessDate.ToString()
				})
				.OrderByDescending(r => r.AccessDate)
				.ToListAsync();

			var top10Users = recordViewModels
				.GroupBy(r => r.UserIPAddress)
				.Select(r => new IpVisitSummaryViewModel() {
					UserIPAddress = r.Key,
					VisitsCount = r.Count()
				})
				.OrderByDescending(x => x.VisitsCount)
				.Take(10)
				.ToList();

			return new UrlStatisticsViewModel()
			{
				RecordsPerUserPerDay = recordViewModels,
				Top10Users = top10Users
			};
		}

		public async Task<URLViewModel> GetUrlViewModelByOriginalUrlAsync(string url) =>
			await _repository.AllReadOnly<URL>()
				.Where(u => u.OriginalUrl == url)
				.Select(u => new URLViewModel()
				{
					OriginalUrl = u.OriginalUrl,
					ShortenedUrl = u.ShortenedUrl
				})
				.FirstOrDefaultAsync();

		public async Task<bool> HasUrlOpenBeenRecordedTodayAsync(Guid urlId, string ipAddress, DateTime dateTimeToday) =>
			await _repository.AllReadOnly<Record>()
				.AnyAsync(r => r.URLId == urlId &&
				r.AccessDate.Date == dateTimeToday.Date &&
				r.UserIPAddress == ipAddress);

		public async Task<bool> IsShortenedUrlValidAsync(string shortenedUrl) =>
			await _repository.AllReadOnly<URL>()
				.AnyAsync(u => u.ShortenedUrl == shortenedUrl);

		public async Task<bool> OriginalUrlExistsAsync(string url) =>
			await _repository.AllReadOnly<URL>()
				.AnyAsync(u => u.OriginalUrl == url);

		public async Task RecordUrlOpensAsync(Guid urlId, string ipAddress)
		{
			var record = new Record()
			{
				AccessDate = DateTime.UtcNow,
				UserIPAddress = ipAddress,
				URLId = urlId
			};

			await _repository.AddAsync<Record>(record);
			await _repository.SaveChangesAsync();
		}

		private async Task<string> GenerateShortenedUrlAsync(string longUrl, int length = 10)
		{
			var shortenedUrl = string.Empty;

			do
			{
				// Generating a unique identifier for the URL and converting it to a byte array
				byte[] guidBytes = Guid.NewGuid().ToByteArray();

				// Converting the byte array to a BigInteger
				var value = new BigInteger(guidBytes);

				// Making sure the value is positive
				if (value < 0)
				{
					value = -value;
				}

				// Generating the Base62 encoded string
				StringBuilder base62Url = new StringBuilder();

				while (value > 0)
				{
					value = BigInteger.DivRem(value, 62, out BigInteger remainder);

					base62Url.Insert(0, Base62Chars[(int)remainder]);
				}

				// Taking the first N characters for the shortened URL
				shortenedUrl = base62Url.ToString().Substring(0, length);

			} while (await ShortenedUrlExistsAsync(shortenedUrl));

			return shortenedUrl;
		}
		private async Task<bool> ShortenedUrlExistsAsync(string shortenedUrl) =>
			await _repository.AllReadOnly<URL>()
				.AnyAsync(u => u.ShortenedUrl == shortenedUrl);
	}
}
