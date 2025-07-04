using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static URLShortenerApp.Constants.DataConstants.URL;

namespace URLShortenerApp.Data.Models
{
	public class URL
	{
		[Key]
		[Comment("URL identifier")]
		public Guid Id { get; set; }

		[Required]
		[MaxLength(OriginalUrlMaxLength)]
		[Comment("Original URL")]
		public string OriginalUrl { get; set; }

		[Required]
		[MaxLength(ShortenedUrlMaxLength)]
		[Comment("Shortened URL")]
		public string ShortenedUrl { get; set; }

		[Required]
		[MaxLength(SecretUrlMaxLength)]
		[Comment("Secret URL")]
		public string SecretUrl { get; set; }

		[Required]
		[Comment("URL creation date")]
		public DateTime CreationDate { get; set; }

		public ICollection<Record> Records { get; set; } = new List<Record>();
	}
}
