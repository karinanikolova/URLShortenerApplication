using System.Threading.Channels;

namespace URLShortenerApp.BackgroundServices
{
	/// <summary>
	/// A queue implementing IUrlAccessQueue for handling URL access log DTOs for asynchronous processing.
	/// </summary>
	public class UrlAccessQueue : IUrlAccessQueue
	{
		private readonly Channel<UrlAccessLogDto> _channel;

		public UrlAccessQueue()
		{
			// Initializing a channel with no limit of the number of items (UrlAccessLogDtos) it can hold.
			_channel = Channel.CreateUnbounded<UrlAccessLogDto>();
		}

		public ChannelReader<UrlAccessLogDto> Reader => _channel.Reader;

		public void Enqueue(UrlAccessLogDto logDto)
		{
			if (!_channel.Writer.TryWrite(logDto))
			{
				throw new InvalidOperationException("Could not enqueue log DTO.");
			}
		}
	}
}
