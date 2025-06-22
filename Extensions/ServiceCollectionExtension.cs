using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using URLShortenerApp.Data;
using URLShortenerApp.Data.Utilities;
using URLShortenerApp.Data.Utilities.Contracts;
using URLShortenerApp.Services;
using URLShortenerApp.Services.Contracts;

namespace Microsoft.Extensions.DependencyInjection
{
	/// <summary>
	/// Extension methods for IServiceCollection in order to keep the Program.cs file decluttered.
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

			return services;
		}
	}
}
