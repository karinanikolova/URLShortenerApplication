using Microsoft.EntityFrameworkCore;
using URLShortenerApp.Data.Configuration;
using URLShortenerApp.Data.Models;

namespace URLShortenerApp.Data
{
	public class AppDbContext : DbContext
	{
		private bool _shouldSeedDb;

		public AppDbContext(DbContextOptions<AppDbContext> options, bool shouldSeedDb = true) : base(options)
		{
			_shouldSeedDb = shouldSeedDb;
		}

		public DbSet<URL> URLs { get; set; }
		public DbSet<Record> Records { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			if (_shouldSeedDb)
			{
				// Applying configurations for URL and Record entities
				builder.ApplyConfiguration<URL>(new URLConfiguration());
				builder.ApplyConfiguration<Record>(new RecordConfiguration());
			}
		}
	}
}
