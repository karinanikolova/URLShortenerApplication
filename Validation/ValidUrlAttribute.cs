using System.ComponentModel.DataAnnotations;

namespace URLShortenerApp.Validation
{
	public class ValidUrlAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
			{
				return ValidationResult.Success;
			}

			var input = value.ToString().Trim();

			if (!input.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !input.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
			{
				input = "http://" + input;
			}

			if (Uri.TryCreate(input, UriKind.Absolute, out var uriResult) &&
				(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
			{
				return ValidationResult.Success;
			}

			return new ValidationResult("Please enter a valid URL.");
		}
	}
}
