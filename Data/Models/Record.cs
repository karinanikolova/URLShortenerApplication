using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static URLShortenerApp.Constants.DataConstants.Record;

namespace URLShortenerApp.Data.Models
{
	public class Record
	{
		[Key]
		[Comment("Record identifier")]
		public Guid Id { get; set; }

		[Required]
		[Comment("URL access date")]
		public DateTime AccessDate { get; set; }

		[Required]
		[MaxLength(UserIPAddressMaxLength)]
		[Comment("User IP adress")]
		public string UserIPAddress { get; set; }

		[Required]
		[Comment("URL record identifier")]
		public Guid URLId { get; set; }

		[ForeignKey(nameof(URLId))]
		public URL URL { get; set; }
	}
}
