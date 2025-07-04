using URLShortenerApp.Data;
using URLShortenerApp.Data.Models;
using URLShortenerApp.Tests.Mocks;

namespace URLShortenerApp.Tests.UnitTests
{
	/// <summary>
	/// Base class for unit tests that sets up an in-memory database context and seeds it with test data.
	/// </summary>
	public class UnitTestsBase
	{
		protected AppDbContext _dbContext;

		protected UnitTestsBase()
		{

		}

		public List<URL> TestURLs { get; private set; }
		public List<Record> TestRecords { get; private set; }

		[OneTimeSetUp]
		public void SetUpDatabase()
		{
			_dbContext = DatabaseMock.Instance;
			_dbContext.Database.EnsureCreated();

			SeedDatabase();
		}

		[OneTimeTearDown]
		public void TearDownDatabase() => _dbContext.Dispose();

		private void SeedDatabase()
		{
			SeedURLs();
			SeedRecords();
		}

		private void SeedURLs()
		{
			TestURLs = new List<URL>()
			{
				new URL()
				{
					Id = Guid.Parse("8edd25fa-ce50-4e4d-a766-bf47dc5457fb"),
					OriginalUrl = "https://learn.microsoft.com/",
					ShortenedUrl = "b0fb55",
					SecretUrl = "3M4LLmm8wa",
					CreationDate = DateTime.UtcNow.AddDays(-15)
				},
				new URL()
				{
					Id = Guid.Parse("f21cbfc0-d7c2-4ea2-b6f5-6cfc62ab71bf"),
					OriginalUrl = "https://developer.mozilla.org/en-US/",
					ShortenedUrl = "4ab86f",
					SecretUrl = "3Nbu3MLVXe",
					CreationDate= DateTime.UtcNow.AddDays(-20)
				},
				new URL()
				{
					Id = Guid.Parse("aa6bea74-0160-4579-8abd-1fe2cfc75d7f"),
					OriginalUrl = "https://github.com/",
					ShortenedUrl = "c05ad8",
					SecretUrl = "7TkaTgMDdD",
					CreationDate= DateTime.UtcNow.AddDays(-10)
				}
			};

			_dbContext.URLs.AddRange(TestURLs);
			_dbContext.SaveChanges();
		}

		private void SeedRecords()
		{
			TestRecords = new List<Record>()
			{
				new Record()
				{
					Id = Guid.Parse("43e9e4f1-e8d8-4de7-a070-3c9d090ddde0"),
					AccessDate = DateTime.UtcNow,
					UserIPAddress = "173.67.45.12",
					URLId = Guid.Parse("8edd25fa-ce50-4e4d-a766-bf47dc5457fb")
				},
				new Record()
				{
					Id = Guid.Parse("81d2debd-b62a-4291-8bbf-74ed2720d6c1"),
					AccessDate = DateTime.UtcNow.AddDays(-3),
					UserIPAddress = "173.67.45.12",
					URLId = Guid.Parse("8edd25fa-ce50-4e4d-a766-bf47dc5457fb")
				},
				new Record()
				{
					Id = Guid.Parse("bb25cd81-3450-432d-9c5c-ace02ad1d090"),
					AccessDate = DateTime.UtcNow.AddDays(-1),
					UserIPAddress = "173.67.45.12",
					URLId = Guid.Parse("8edd25fa-ce50-4e4d-a766-bf47dc5457fb")
				},
				new Record()
				{
					Id = Guid.Parse("bad7734e-d135-440a-89fa-0d847316df39"),
					AccessDate = DateTime.UtcNow.AddDays(-7),
					UserIPAddress = "8.129.25.201",
					URLId = Guid.Parse("8edd25fa-ce50-4e4d-a766-bf47dc5457fb")
				},
				new Record()
				{
					Id = Guid.Parse("85cc8b32-016b-4e44-ab89-abe45190e0e5"),
					AccessDate = DateTime.UtcNow.AddDays(-2),
					UserIPAddress = "105.234.1.98",
					URLId = Guid.Parse("8edd25fa-ce50-4e4d-a766-bf47dc5457fb")
				},
				new Record()
				{
					Id = Guid.Parse("d9aaa388-9039-4ab3-84dc-b465773e6b94"),
					AccessDate = DateTime.UtcNow.AddDays(-1),
					UserIPAddress = "202.5.167.22",
					URLId = Guid.Parse("f21cbfc0-d7c2-4ea2-b6f5-6cfc62ab71bf")
				},
				new Record()
				{
					Id = Guid.Parse("0e50e929-d50a-4c52-b78a-8849b1fe64a8"),
					AccessDate = DateTime.UtcNow.AddDays(-9),
					UserIPAddress = "202.5.167.22",
					URLId = Guid.Parse("f21cbfc0-d7c2-4ea2-b6f5-6cfc62ab71bf")
				},
				new Record()
				{
					Id = Guid.Parse("a7b4f17f-d406-4644-95e3-1cd2a82401f6"),
					AccessDate = DateTime.UtcNow.AddDays(-10),
					UserIPAddress = "197.102.5.136",
					URLId = Guid.Parse("f21cbfc0-d7c2-4ea2-b6f5-6cfc62ab71bf")
				},
				new Record()
				{
					Id = Guid.Parse("5ead27e2-3ad0-40e0-a065-859d7f844850"),
					AccessDate = DateTime.UtcNow.AddDays(-1),
					UserIPAddress = "21.200.12.77",
					URLId = Guid.Parse("aa6bea74-0160-4579-8abd-1fe2cfc75d7f")
				},
				new Record()
				{
					Id = Guid.Parse("c75805f7-c70b-4561-a88a-7fddb3e9ba89"),
					AccessDate = DateTime.UtcNow.AddDays(-1),
					UserIPAddress = "23.9.231.188",
					URLId = Guid.Parse("aa6bea74-0160-4579-8abd-1fe2cfc75d7f")
				},
				new Record()
				{
					Id = Guid.Parse("d91ec354-8aba-4bb6-83c4-1e9608df5e70"),
					AccessDate = DateTime.UtcNow.AddDays(-5),
					UserIPAddress = "173.67.45.12",
					URLId = Guid.Parse("aa6bea74-0160-4579-8abd-1fe2cfc75d7f")
				}
			};

			_dbContext.Records.AddRange(TestRecords);
			_dbContext.SaveChanges();
		}
	}
}
