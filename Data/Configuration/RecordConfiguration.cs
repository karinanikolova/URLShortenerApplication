using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using URLShortenerApp.Data.Models;

namespace URLShortenerApp.Data.Configuration
{
	public class RecordConfiguration : IEntityTypeConfiguration<Record>
	{
		public void Configure(EntityTypeBuilder<Record> builder)
		{
			// Configuring the Record entity's ralationship with URL and on delete behavior
			builder
				.HasOne(r => r.URL)
				.WithMany(u => u.Records)
				.HasForeignKey(r => r.URLId)
				.OnDelete(DeleteBehavior.Restrict);

			// Configuring a composite unique index constraint which ensures that no duplicate visit records for the same user on the same day can exist
			builder
				.HasIndex(r => new { r.URLId, r.AccessDate, r.UserIPAddress })
				.IsUnique();

			// Seeding initial data for Records
			builder.HasData(DataSeed.Records);
		}
	}
}
