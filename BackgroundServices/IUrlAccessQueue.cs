using System.Threading.Channels;

namespace URLShortenerApp.BackgroundServices
{
	/// <summary>
	/// Interface for a queue that handles URL access log DTOs.
	/// </summary>
	public interface IUrlAccessQueue
	{
		ChannelReader<UrlAccessLogDto> Reader { get; }

		void Enqueue(UrlAccessLogDto logDto);
	}
}
