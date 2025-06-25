namespace URLShortenerApp.BackgroundServices
{
	/// <summary>
	/// Data Transfer Object for storing URL access information temporarily.
	/// </summary>
	public class UrlAccessLogDto
	{
		public Guid URLId { get; set; }

		public string UserIPAddress { get; set; }

		public DateTime AccessDate { get; set; }
	}
}
