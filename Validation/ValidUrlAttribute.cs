using System.ComponentModel.DataAnnotations;
using URLShortenerApp.Services.Contracts;

namespace URLShortenerApp.Validation
{
	/// <summary>
	/// ValidUrlAttribute is a custom validation attribute that checks if a given URL is valid.
	/// </summary>
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
				input = "https://" + input;
			}

			if (Uri.TryCreate(input, UriKind.Absolute, out var uriResult) &&
				(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
			{
				var tld = uriResult.Host.ToLowerInvariant().Split('.').LastOrDefault();
				var tldService = (ITldService)validationContext.GetService(typeof(ITldService));

				if (tld != null && tldService?.IsValidTld(tld) == true)
				{
					return ValidationResult.Success;
				}
			}

			return new ValidationResult("Please enter a valid URL with a recognized top-level domain.");
		}
	}
}
