using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using URLShortenerApp.Data.Models;

namespace URLShortenerApp.Data.Configuration
{
	public class URLConfiguration : IEntityTypeConfiguration<URL>
	{
		public void Configure(EntityTypeBuilder<URL> builder)
		{
			// Indexing the ShortenedUrl for faster lookups
			builder
				.HasIndex(u => u.ShortenedUrl)
				.IsUnique();

			// Seeding initial data for URLs
			builder.HasData(DataSeed.URLs);
		}
	}
}
