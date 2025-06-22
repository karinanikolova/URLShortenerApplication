using URLShortenerApp.Data.Models;

namespace URLShortenerApp.Data.Configuration
{
	/// <summary>
	/// Static class to seed initial data for URLs and records.
	/// </summary>
	public static class DataSeed
	{
		// URL identifiers
		private static readonly Guid FirstURLId = Guid.Parse("e244bc69-5e82-4cdc-aabf-69a7c620d282");
		private static readonly Guid SecondURLId = Guid.Parse("f2fd801f-6493-4a93-a9fa-1829c8ac21fd");
		private static readonly Guid ThirdURLId = Guid.Parse("19cbaea2-e65e-4931-8c25-685708d7f4c8");

		// Record identifiers
		private static readonly Guid FirstRecordId = Guid.Parse("86739286-18d5-4f31-85b6-f6d824108650");
		private static readonly Guid SecondRecordId = Guid.Parse("e2723ab2-6c56-43e4-b594-a59f9a3b9006");
		private static readonly Guid ThirdRecordId = Guid.Parse("8b91667d-031a-4c26-8817-a4c05fbca351");
		private static readonly Guid FourthRecordId = Guid.Parse("149c2f2c-14ec-4169-bb66-7e8c014c78ac");
		private static readonly Guid FifthRecordId = Guid.Parse("37898beb-b7b7-4398-ab01-96596bedee10");
		private static readonly Guid SixthRecordId = Guid.Parse("8f55796e-b0dd-451e-a22e-8335af736b6d");
		private static readonly Guid SeventhRecordId = Guid.Parse("1f034183-ec79-4ea2-a0c1-8bd47967efff");
		private static readonly Guid EighthRecordId = Guid.Parse("d1c80305-ec15-4a89-b96a-b8a4dd259959");
		private static readonly Guid NinethRecordId = Guid.Parse("8f602fd7-869a-484a-9cf2-6f41d4dacbad");
		private static readonly Guid TenthRecordId = Guid.Parse("e13642b1-c894-41d3-acc9-8211e8a2ba8b");
		private static readonly Guid EleventhRecordId = Guid.Parse("fad2151d-5e15-4b6c-9db3-fd9149217e7e");
		private static readonly Guid TwelvethRecordId = Guid.Parse("1eaa56a1-99c6-4c5c-8351-58f03a381c47");
		private static readonly Guid ThirteenthRecordId = Guid.Parse("6051d816-2a0c-439f-90ac-cba5b7d913df");

		// Collections to hold the data
		public static readonly List<URL> URLs = new();
		public static readonly List<Record> Records = new();

		// Static constructor to initialize the data
		static DataSeed()
		{
			InitializeURLs();
			InitializeRecords();
		}

		// Implementing the URLs initialization method
		private static void InitializeURLs()
		{
			URLs.AddRange(new List<URL>()
			{
				new()
				{
					Id = FirstURLId,
					OriginalUrl = "https://www.progress.com/",
					ShortenedUrl = "NlptN4Bw1P",
					CreationDate = DateTime.UtcNow.AddDays(-25)
				},
				new()
				{
					Id = SecondURLId,
					OriginalUrl = "https://dev.bg/company/progress/",
					ShortenedUrl = "BtGWzHZ96t",
					CreationDate = DateTime.UtcNow.AddDays(-30)
				},
				new()
				{
					Id = ThirdURLId,
					OriginalUrl = "https://www.telerik.com/",
					ShortenedUrl = "X9k2v3Q8yF",
					CreationDate = DateTime.UtcNow.AddDays(-35)
				}
			});
		}

		// Implementing the Records initialization method
		private static void InitializeRecords()
		{
			Records.AddRange(new List<Record>()
			{
				new()
				{
					Id = FirstRecordId,
					AccessDate = DateTime.UtcNow,
					UserIPAddress = "165.122.32.209",
					URLId = FirstURLId
				},
				new()
				{
					Id = SecondRecordId,
					AccessDate = DateTime.UtcNow.AddDays(-10),
					UserIPAddress = "165.122.32.209",
					URLId = FirstURLId
				},
				new()
				{
					Id = ThirdRecordId,
					AccessDate = DateTime.UtcNow.AddDays(-3),
					UserIPAddress = "119.240.93.167",
					URLId = FirstURLId
				},
				new()
				{
					Id = FourthRecordId,
					AccessDate = DateTime.UtcNow.AddDays(-1),
					UserIPAddress = "165.122.32.209",
					URLId = FirstURLId
				},
				new()
				{
					Id = FifthRecordId,
					AccessDate = DateTime.UtcNow.AddDays(-9),
					UserIPAddress = "1.225.63.20",
					URLId = FirstURLId
				},
				new()
				{
					Id = SixthRecordId,
					AccessDate = DateTime.UtcNow.AddDays(-15),
					UserIPAddress = "215.148.253.91",
					URLId = SecondURLId
				},
				new()
				{
					Id = SeventhRecordId,
					AccessDate = DateTime.UtcNow.AddDays(-7),
					UserIPAddress = "165.122.32.209",
					URLId = SecondURLId
				},new()
				{
					Id = EighthRecordId,
					AccessDate = DateTime.UtcNow.AddDays(-2),
					UserIPAddress = "165.218.50.241",
					URLId = SecondURLId
				},
				new()
				{
					Id = NinethRecordId,
					AccessDate = DateTime.UtcNow.AddDays(-1),
					UserIPAddress = "215.148.253.91",
					URLId = SecondURLId
				},
				new()
				{
					Id = TenthRecordId,
					AccessDate = DateTime.UtcNow.AddDays(-20),
					UserIPAddress = "119.240.93.167",
					URLId = ThirdURLId
				},
				new()
				{
					Id = EleventhRecordId,
					AccessDate = DateTime.UtcNow.AddDays(-25),
					UserIPAddress = "165.218.50.241",
					URLId = ThirdURLId
				},
				new()
				{
					Id = TwelvethRecordId,
					AccessDate = DateTime.UtcNow.AddDays(-5),
					UserIPAddress = "128.181.26.173",
					URLId = ThirdURLId
				},
				new()
				{
					Id = ThirteenthRecordId,
					AccessDate = DateTime.UtcNow.AddDays(-1),
					UserIPAddress = "165.122.32.209",
					URLId = ThirdURLId
				}
			});
		}
	}
}
