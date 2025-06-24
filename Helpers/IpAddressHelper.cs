namespace URLShortenerApp.Helpers
{
	public static class IpAddressHelper
	{
		/// <summary>
		/// Gets the IP address of the current request.
		/// </summary>
		/// <param name="context">The HTTP context.</param>
		/// <returns>The IP address as a string.</returns>
		public static string GetUserIpAddress(HttpContext context)
		{
			// Tries to get the user IP from the X-Forwarded-For (used by proxies), if available.
			var forwardedHeader = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();

			if (!string.IsNullOrEmpty(forwardedHeader))
			{
				return forwardedHeader.Split(',').First().Trim();
			}

			// If the X-Forwarded-For header is not present, falls back to the RemoteIpAddress.
			return context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
		}
	}
}
