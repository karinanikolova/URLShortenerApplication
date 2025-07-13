using URLShortenerApp.Helpers.Contracts;

namespace URLShortenerApp.Helpers
{
	/// <summary>
	/// CustomUrlHelper class provides methods to normalize URLs.
	/// </summary>
	public class CustomUrlHelper : ICustomUrlHelper
	{
		private readonly HttpClient _httpClient;

		public CustomUrlHelper(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		/// <summary>
		/// Normalizes the input URL by ensuring it starts with "http://" or "https://", checking its existence, and removing the trailing slash if present.
		/// </summary>
		/// <param name="inputUrl"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public async Task<string> Normalize(string inputUrl)
		{
			if (!inputUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
				!inputUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
			{
				inputUrl = "https://" + inputUrl;
			}

			var uri = new Uri(inputUrl);

			var httpsUrl = new UriBuilder(uri) { Scheme = uri.Scheme }.Uri.ToString();

			return NormalizeStructure(httpsUrl);
		}

		/// <summary>
		/// Normalizes the URL structure by removing the trailing slash.
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		private string NormalizeStructure(string url)
		{
			var uri = new Uri(url);

			var builder = new UriBuilder(uri)
			{
				Host = uri.Host.ToLowerInvariant(),
				Scheme = uri.Scheme,
				Path = uri.AbsolutePath
			};

			//Removing trailing slash
			if (builder.Path != "/" && builder.Path.EndsWith("/"))
			{
				builder.Path = builder.Path.TrimEnd('/');
			}

			return builder.Uri.ToString();
		}
	}
}
