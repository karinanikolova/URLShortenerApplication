namespace URLShortenerApp.Services.Contracts
{
	/// <summary>
	/// Interface for the TLD (Top-Level Domain) service.
	/// </summary>
	public interface ITldService
	{
		Task InitializeAsync();

		bool IsValidTld(string tld);
	}
}
