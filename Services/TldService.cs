using URLShortenerApp.Services.Contracts;

namespace URLShortenerApp.Services
{
	/// <summary>
	/// Service implementing the ITldService interface for fetching and storing the Top-Level Domains (TLDs) at application startup.
	/// </summary>
	public class TldService : ITldService
	{
		private const string ianaTldUrl = "https://data.iana.org/TLD/tlds-alpha-by-domain.txt";

		private readonly HashSet<string> _validTlds = new();

		public IReadOnlyCollection<string> ValidTlds => _validTlds;

		public async Task InitializeAsync()
		{
			using var httpClient = new HttpClient();

			string content = await httpClient.GetStringAsync(ianaTldUrl);

			var tlds = content
				.Split('\n')
				.Where(line => !string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
				.Select(line => line.Trim().ToLower());

			_validTlds.Clear();

			foreach (var tld in tlds)
			{
				_validTlds.Add(tld);
			}
		}

		public bool IsValidTld(string tld) => _validTlds.Contains(tld.ToLower());
	}
}
