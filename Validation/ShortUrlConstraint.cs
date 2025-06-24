using System.Text.RegularExpressions;

namespace URLShortenerApp.Validation
{
	/// <summary>
	/// A custom route constraint that validates a short URL.
	/// </summary>
	public class ShortUrlConstraint : IRouteConstraint
	{
		// If the short URL is made to be generated with less or more than 10 alphanumeric characters,the regex should be updated.
		private static readonly Regex _regex = new Regex("^[a-zA-Z0-9]{10}$", RegexOptions.Compiled); 
		public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
		{
			// Checks if the routeKey exists in the values dictionary.
			var value = values[routeKey]?.ToString();

			// Checks if the value is not null or empty and matches the regex pattern.
			return !string.IsNullOrEmpty(value) && _regex.IsMatch(value);
		}
	}
}
