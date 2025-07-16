using URLShortenerApp.Services.Contracts;

namespace URLShortenerApp.Services
{
	/// <summary>
	/// Provides functionality to retrieve the list of top-level domains (TLDs) from the IANA (Internet Assigned Numbers
	/// Authority) data source.
	/// </summary>
	/// <remarks>This class fetches the TLD data from the official IANA TLD list, which is hosted at  <see
	/// href="https://data.iana.org/TLD/tlds-alpha-by-domain.txt"/>. The data is returned as a plain text string, with
	/// each TLD listed on a separate line. The caller is responsible for parsing or processing the data as
	/// needed.</remarks>
	public class IanaTldProvider : ITldProvider
	{
		private const string IanaTldUrl = "https://data.iana.org/TLD/tlds-alpha-by-domain.txt";

		public async Task<string> GetTldsAsync()
		{
			using var httpClient = new HttpClient();

			var tlds = await httpClient.GetStringAsync(IanaTldUrl);

			return tlds;
		}
	}
}
