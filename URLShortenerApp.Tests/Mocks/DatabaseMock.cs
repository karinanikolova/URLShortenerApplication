using Microsoft.EntityFrameworkCore;
using URLShortenerApp.Data;

namespace URLShortenerApp.Tests.Mocks
{
	/// <summary>
	/// Static class for database mock which provides Instance property for creating an in-memory database with a unique name without any seeded data.
	/// </summary>
	public static class DatabaseMock
	{
		public static AppDbContext Instance
		{
			get
			{
				var options = new DbContextOptionsBuilder<AppDbContext>()
					.UseInMemoryDatabase("AppInMemoryDb" + DateTime.Now.Ticks.ToString())
					.Options;

				return new AppDbContext(options, false);
			}
		}
	}
}
