namespace URLShortenerApp.Services.Contracts
{
	public interface IUrlService
	{
		Task AddUrl(string url);
	}
}
