using URLShortenerApp.Services.Contracts;

namespace URLShortenerApp.Services
{
	/// <summary>
	/// Service implementing the ITldService interface for fetching and storing the Top-Level Domains (TLDs) at application startup.
	/// </summary>
	public class TldService : ITldService
	{
		private readonly ITldProvider _tldProvider;
		private readonly HashSet<string> _validTlds = new();

		public TldService(ITldProvider tldProvider)
		{
			_tldProvider = tldProvider;
		}

		public IReadOnlyCollection<string> ValidTlds => _validTlds;

		public async Task InitializeAsync()
		{
			var tldsContent = await _tldProvider.GetTldsAsync();

			var tlds = tldsContent
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
