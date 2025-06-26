using Microsoft.EntityFrameworkCore;
using URLShortenerApp.BackgroundServices;
using URLShortenerApp.Data;
using URLShortenerApp.Data.Utilities;
using URLShortenerApp.Data.Utilities.Contracts;
using URLShortenerApp.Helpers;
using URLShortenerApp.Helpers.Contracts;
using URLShortenerApp.Services;
using URLShortenerApp.Services.Contracts;
using URLShortenerApp.Validation;

namespace Microsoft.Extensions.DependencyInjection
{
	/// <summary>
	/// Extension methods for IServiceCollection in order to keep the Program.cs file decluttered and encapsulate related logic.
	/// </summary>
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped<IRepository, Repository>();

			return services;
		}

		public static IServiceCollection AddAppServices(this IServiceCollection services)
		{
			// Adding custom services to the Inversion of Control container.
			services.AddScoped<IUrlService, UrlService>();

			services.AddSingleton<ITldService, TldService>();
			services.AddSingleton<IUrlAccessQueue, UrlAccessQueue>();

			services.AddHostedService<UrlAccessLoggingService>();
			services.AddHttpClient<ICustomUrlHelper, CustomUrlHelper>();

			return services;
		}

		public static IServiceCollection AddRouteConfiguration(this IServiceCollection services)
		{ 
			// Adding custom route constraints to the Inversion of Control container.
			services.Configure<RouteOptions>(options =>
			{
				options.ConstraintMap.Add("shortUrl", typeof(ShortUrlConstraint));
			});

			return services;
		}
	}
}
