
using URLShortenerApp.Services.Contracts;

namespace URLShortenerApp.BackgroundServices
{
	/// <summary>
	/// Background service that processes URL access logs from a queue and records them in the database.
	/// </summary>
	public class UrlAccessLoggingService : BackgroundService
	{
		private readonly IUrlAccessQueue _queue;
		private readonly IServiceProvider _serviceProvider;

		public UrlAccessLoggingService(IUrlAccessQueue queue, IServiceProvider serviceProvider)
		{
			_queue = queue;
			_serviceProvider = serviceProvider;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			await foreach (var logDto in _queue.Reader.ReadAllAsync(stoppingToken))
			{
				try
				{
					using var scope = _serviceProvider.CreateScope();
					var urlService = scope.ServiceProvider.GetRequiredService<IUrlService>();
					await urlService.RecordUrlOpensAsync(logDto.URLId, logDto.UserIPAddress, logDto.AccessDate);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine($"Error processing log item: {ex.Message}");
				}
			}
		}
	}
}
