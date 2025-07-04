using Microsoft.EntityFrameworkCore;
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

			Assert.That(id, Is.Not.EqualTo(Guid.Empty), "The evaluated ID is equal to zero.");
			Assert.That(id, Is.EqualTo(url.Id), "The evaluated IDs are not the same.");
		}

		[Test]
		public async Task GetUrlStatisticsViewModelAsync_ShouldReturnCorrectStats()
		{
			var url = TestURLs.Last();

			var stats = await _urlService.GetUrlStatisticsViewModelAsync(url.Id);

			Assert.That(stats, Is.Not.Null);
			Assert.That(stats.UniqueVisitsPerDay.Count, Is.EqualTo(3), "The URL unique visits per day are not the right count..");
			Assert.That(stats.Top10Users.Count, Is.GreaterThanOrEqualTo(3), "The URL top 10 users are not the right count.");
		}

		[Test]
		public async Task HasUrlOpenBeenRecordedTodayAsync_ShouldReturnTrue_WhenRecordExists()
		{
			var url = TestURLs.First();
			var ip = "173.67.45.12";
			var today = DateTime.UtcNow;

			var result = await _urlService.HasUrlOpenBeenRecordedTodayAsync(url.Id, ip, today);

			Assert.That(result, Is.True, "Expected a record to exist for today but none was found.");
		}

		[Test]
		public async Task HasUrlOpenBeenRecordedTodayAsync_ShouldReturnFalse_WhenNoRecordExists()
		{
			var url = TestURLs.First();
			var ip = "0.0.0.0";
			var today = DateTime.UtcNow;

			var result = await _urlService.HasUrlOpenBeenRecordedTodayAsync(url.Id, ip, today);

			Assert.That(result, Is.False, "Expected no record to exist, but one was found.");
		}

		[Test]
		public async Task IsSecretUrlValidAsync_ShouldReturnTrue_WhenSecretUrlExists()
		{
			var secretUrl = TestURLs.First().SecretUrl;

			var result = await _urlService.IsSecretUrlValidAsync(secretUrl);

			Assert.That(result, Is.True, "Expected the secret URL to be valid.");
		}

		[Test]
		public async Task IsSecretUrlValidAsync_ShouldReturnFalse_WhenSecretUrlDoesNotExist()
		{
			var result = await _urlService.IsSecretUrlValidAsync("secretUrl");

			Assert.That(result, Is.False, "Expected the secret URL to be invalid.");
		}

		[Test]
		public async Task OriginalUrlExistsAsync_ShouldReturnTrue_WhenUrlExists()
		{
			var url = TestURLs.First().OriginalUrl;

			var result = await _urlService.OriginalUrlExistsAsync(url);

			Assert.That(result, Is.True, "Expected the original URL to exist.");
		}

		[Test]
		public async Task OriginalUrlExistsAsync_ShouldReturnFalse_WhenUrlDoesNotExist()
		{
			var result = await _urlService.OriginalUrlExistsAsync("https://pestebin.com");

			Assert.That(result, Is.False, "Expected the original URL to not exist.");
		}

		[Test]
		public async Task RecordUrlOpensAsync_ShouldAddNewRecord()
		{
			var url = TestURLs.Last();
			var ip = "111.111.111.111";
			var date = DateTime.UtcNow;

			await _urlService.RecordUrlOpensAsync(url.Id, ip, date);

			var exists = await _dbContext.Records.AnyAsync(r =>
				r.URLId == url.Id && r.UserIPAddress == ip && r.AccessDate.Date == date.Date);

			Assert.That(exists, Is.True, "Expected the record to be added to the database.");
		}

		[Test]
		public async Task GetUrlIdByShortenedUrlAsync_ShouldReturnCorrectId()
		{
			var url = TestURLs.First();

			var id = await _urlService.GetUrlIdByShortenedUrlAsync(url.ShortenedUrl);

			Assert.That(id, Is.EqualTo(url.Id), "The returned ID does not match the expected URL ID.");
		}

		[Test]
		public async Task GetUrlIdByShortenedUrlAsync_ShouldReturnEmptyGuid_WhenNotFound()
		{
			var id = await _urlService.GetUrlIdByShortenedUrlAsync("shortenedUrl");

			Assert.That(id, Is.EqualTo(Guid.Empty), "Expected an empty Guid for a nonexistent shortened URL.");
		}
	}
}
