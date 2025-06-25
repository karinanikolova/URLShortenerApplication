namespace URLShortenerApp.Models.Statistics
{
	public class UrlStatisticsViewModel
	{
		public IEnumerable<RecordViewModel> UniqueVisitsPerDay { get; set; } = new List<RecordViewModel>();

		public IEnumerable<IpVisitSummaryViewModel> Top10Users { get; set; } = new List<IpVisitSummaryViewModel>();
	}
}
