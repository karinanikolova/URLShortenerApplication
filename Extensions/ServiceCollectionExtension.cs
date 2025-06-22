using Microsoft.EntityFrameworkCore;
using URLShortenerApp.Data;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			//services.AddScoped<IRepository, Repository>();

			return services;
		}
	}
}
