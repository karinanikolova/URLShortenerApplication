using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using URLShortenerApp.Data.Models;

namespace URLShortenerApp.Data.Configuration
{
	public class URLConfiguration : IEntityTypeConfiguration<URL>
	{
		public void Configure(EntityTypeBuilder<URL> builder)
		{
			
		}
	}
}
