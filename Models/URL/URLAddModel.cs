using System.ComponentModel.DataAnnotations;
using URLShortenerApp.Validation;
using static URLShortenerApp.Constants.DataConstants.URL;
using static URLShortenerApp.Constants.MessageConstants;

namespace URLShortenerApp.Models.URL
{
	public class URLAddModel
	{
		[Required(ErrorMessage = RequiredFieldMessage)]
		[StringLength(OriginalUrlMaxLength, MinimumLength = OriginalUrlMinLength, ErrorMessage = FieldLengthMessage)]
		[ValidUrl]
		public string Url { get; set; }

		public DateTime CreationDate { get; set; } = DateTime.UtcNow.Date;
	}
}
