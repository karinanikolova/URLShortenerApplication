using NUnit.Framework.Internal;
using URLShortenerApp.Data.Utilities;
using URLShortenerApp.Data.Utilities.Contracts;
using URLShortenerApp.Services;
using URLShortenerApp.Services.Contracts;

namespace URLShortenerApp.Tests.UnitTests
{
	/// <summary>
	/// Unit tests for the UrlService class.
	/// </summary>

	[TestFixture]
	public class UrlServiceTests : UnitTestsBase
	{
		private IUrlService _urlService;
		private IRepository _testRepository;

		[OneTimeSetUp]
		public void Setup()
		{
			_testRepository = new Repository(_dbContext);
			_urlService = new UrlService(_testRepository);
		}

		[Test]
		public async Task AddUrlAsync_ShouldAddUrlSuccessfully()
		{
			var url = "https://example.com";
			var creationDate = DateTime.UtcNow;

			await _urlService.AddUrlAsync(url, creationDate);
			
			var addedUrl = await _urlService.GetUrlViewModelByUrlAndCreationDateAsync(url, creationDate);

			Assert.That(addedUrl, Is.Not.Null, "The task of adding a new URL was not successful and the returned value is null.");
			Assert.That(addedUrl.OriginalUrl, Is.EqualTo(url), "The evaluated URL values are not equal.");

			Assert.Multiple(() =>
			{
				Assert.That(addedUrl.OriginalUrl, Is.EqualTo(url), "The evaluated URL values are not equal.");
				Assert.That(addedUrl.ShortenedUrl, Is.Not.Null, "The shortened URL is null.");
				Assert.That(addedUrl.SecretUrl, Is.Not.Null, "The secret URL is null.");
			});
		}

		[Test]
		public async Task GetOriginalUrlByShortenedUrlAsync_ShouldReturnOriginalUrl()
		{
			var url = TestURLs.First();

			var result = await _urlService.GetOriginalUrlByShortenedUrlAsync(url.ShortenedUrl);

			Assert.That(result, Is.EqualTo(url.OriginalUrl));
		}

		[Test]
		public async Task GetUrlIdBySecretUrlAsync_ShouldReturnCorrectGuid()
		{
			var url = TestURLs.Skip(1).Take(1).First();

			var id = await _urlService.GetUrlIdBySecretUrlAsync(url.SecretUrl);

			Assert.That(id, Is.Not.EqualTo(Guid.Empty), "The evaluated id is equal to zero.");
			Assert.That(id, Is.EqualTo(url.Id), "The evaluated ids are not the same.");
		}

		[Test]
		public async Task GetUrlStatisticsViewModelAsync_ShouldReturnCorrectStats()
		{
			var url = TestURLs.Last();

			var stats = await _urlService.GetUrlStatisticsViewModelAsync(url.Id);

			Assert.That(stats, Is.Not.Null);
			Assert.That(stats.UniqueVisitsPerDay.Count,Is.EqualTo(3), "The URL unique visits per day are not the right count..");
			Assert.That(stats.Top10Users.Count, Is.GreaterThanOrEqualTo(3), "The URL top 10 users are not the right count.");
		}
	}
}
