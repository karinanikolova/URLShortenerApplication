namespace URLShortenerApp.Helpers.Contracts
{
	/// <summary>
	/// ICustomUrlHelper interface defines methods for normalizing URLs.
	/// </summary>
	public interface ICustomUrlHelper
	{
		Task<string> Normalize(string inputUrl);
	}
}
