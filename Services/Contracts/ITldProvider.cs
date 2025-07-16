namespace URLShortenerApp.Services.Contracts
{
	/// <summary>
	/// Interaface for a provider that fetches Top-Level Domains (TLDs).
	/// </summary>
	public interface ITldProvider
	{
		Task<string> GetTldsAsync();
	}
}
