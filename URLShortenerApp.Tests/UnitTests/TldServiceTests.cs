using Moq;
using URLShortenerApp.Services;
using URLShortenerApp.Services.Contracts;

namespace URLShortenerApp.Tests.UnitTests
{
	/// <summary>
	/// Unit tests for the UrlService class.
	/// </summary>
	[TestFixture]
	public class TldServiceTests : UnitTestsBase
	{
		private TldService _tldService;
		private Mock<ITldProvider> _mockProvider;

		[OneTimeSetUp]
		public void Setup()
		{
			_mockProvider = new Mock<ITldProvider>();
			_tldService = new TldService(_mockProvider.Object);
		}

		[Test]
		public async Task InitializeAsync_ShouldPopulateValidTlds()
		{
			string tlds = @"
				# Version 2025071600
				COM
				NET
				ORG
				";

			_mockProvider
				.Setup(p => p.GetTldsAsync())
				.ReturnsAsync(tlds);

			await _tldService.InitializeAsync();
			Assert.Multiple(() =>
			{
				Assert.That(_tldService.ValidTlds, Contains.Item("com"), "The evaluated TLD collection does not contain 'com'.");
				Assert.That(_tldService.ValidTlds, Contains.Item("net"), "The evaluated TLD collection does not contain 'net'.");
				Assert.That(_tldService.ValidTlds, Contains.Item("org"), "The evaluated TLD collection does not contain 'org'.");
				Assert.That(_tldService.ValidTlds, Does.Not.Contain("gov"), "The evaluated TLD collection contains 'gov' but it should not.");
			});
		}

		[Test]
		public async Task IsValidTld_ShouldReturnTrue_WhenTldExists()
		{
			_mockProvider
				.Setup(p => p.GetTldsAsync()).
				ReturnsAsync("COM\nNET");

			await _tldService.InitializeAsync();

			Assert.Multiple(() =>
			{
				Assert.That(_tldService.IsValidTld("com"), Is.True, "The evaluated service method returned 'false' for 'com' as a valid TLD.");
				Assert.That(_tldService.IsValidTld("Com"), Is.True, "The evaluated service method returned 'false' for 'Com' as a valid TLD.");
			});
		}

		[Test]
		public async Task IsValidTld_ShouldReturnFalse_WhenTldDoesNotExist()
		{
			_mockProvider
				.Setup(p => p.GetTldsAsync()).
				ReturnsAsync("COM\nBG");

			await _tldService.InitializeAsync();

			Assert.Multiple(() =>
			{
				Assert.That(_tldService.IsValidTld("123"), Is.False, "The evaluated service method returned 'false' for '123' as a valid TLD.");
			});
		}
	}
}
