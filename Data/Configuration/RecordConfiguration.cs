using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using URLShortenerApp.Data.Models;

namespace URLShortenerApp.Data.Configuration
{
	public class RecordConfiguration : IEntityTypeConfiguration<Record>
	{
		public void Configure(EntityTypeBuilder<Record> builder)
		{
			builder
				.HasOne(r => r.URL)
				.WithMany(u => u.Records)
				.HasForeignKey(r => r.URLId)
				.OnDelete(DeleteBehavior.Restrict);

			// Seeding initial data for Records
			builder.HasData(DataSeed.Records);
		}
	}
}
