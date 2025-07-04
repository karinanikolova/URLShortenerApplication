namespace URLShortenerApp.Constants
{
	public static class DataConstants
	{
		public static class URL
		{
			// Different for each browser, but generally accepted maximum length for URLs
			public const int OriginalUrlMaxLength = 2048;
			public const int OriginalUrlMinLength = 5;

			public const int ShortenedUrlMaxLength = 6;

			public const int SecretUrlMaxLength = 10;
		}

		public static class  Record
		{
			// Maximum length for IPv6 address
			public const int UserIPAddressMaxLength = 45; 
		}
	}
}
