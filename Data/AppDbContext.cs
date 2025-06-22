using Microsoft.EntityFrameworkCore;
using URLShortenerApp.Data.Configuration;
using URLShortenerApp.Data.Models;

namespace URLShortenerApp.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<URL> URLs { get; set; }
		public DbSet<Record> Records { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyConfiguration<URL>(new URLConfiguration());
			builder.ApplyConfiguration<Record>(new RecordConfiguration());
		}
	}
}
