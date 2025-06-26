using System.Net;
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
				if (!inputUrl.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
				{
					inputUrl = "https://www." + inputUrl;
				}
				else
				{
					inputUrl = "https://" + inputUrl;
				}
			}
			else
			{
				if (!inputUrl.Contains("www.", StringComparison.OrdinalIgnoreCase))
				{
					if (inputUrl.Contains("http://", StringComparison.OrdinalIgnoreCase))
					{
						inputUrl = inputUrl.Replace("http://", "https://www.");

					}
					else
					{
						inputUrl = inputUrl.Replace("https://", "https://www.");
					}
				}
			}

			var uri = new Uri(inputUrl);

			var httpsUrl = new UriBuilder(uri) { Scheme = Uri.UriSchemeHttps }.Uri.ToString();
			var httpUrl = new UriBuilder(uri) { Scheme = Uri.UriSchemeHttp }.Uri.ToString();

			if (await UrlExistsAsync(httpsUrl))
			{
				return NormalizeStructure(httpsUrl);
			}

			if (await UrlExistsAsync(httpUrl))
			{
				return NormalizeStructure(httpUrl);
			}

			throw new Exception("URL is not reachable over HTTP or HTTPS.");
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

		/// <summary>
		/// Checks if the URL exists by sending a HEAD request.
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		private async Task<bool> UrlExistsAsync(string url)
		{
			try
			{
				// Sending a lightweight HEAD request to a remote server.
				var request = new HttpRequestMessage(HttpMethod.Head, url);
				// A response from the web server that hosts the requested domain.
				var response = await _httpClient.SendAsync(request);

				if (!response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.MethodNotAllowed)
				{
					request = new HttpRequestMessage(HttpMethod.Get, url);
					response = await _httpClient.SendAsync(request);
				}

				return response.IsSuccessStatusCode;
			}
			catch
			{
				return false;
			}
		}
	}
}
