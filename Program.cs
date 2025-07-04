using Microsoft.EntityFrameworkCore;
using URLShortenerApp.Data;
using URLShortenerApp.Services.Contracts;

namespace URLShortenerApp
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAppDbContext(builder.Configuration);
			builder.Services.AddControllersWithViews();
			builder.Services.AddAppServices();
			builder.Services.AddRouteConfiguration();

			var app = builder.Build();

			var tldService = app.Services.GetRequiredService<ITldService>();
			await tldService.InitializeAsync();

			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "shortened",
				pattern: "{shortenedUrl}",
				defaults: new { controller = "Redirect", action = "RedirectTo" });

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			// Applying database migrations before starting the application.
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				try
				{
					var dbContext = services.GetRequiredService<AppDbContext>();

					if (dbContext.Database.IsRelational())
					{
						await dbContext.Database.MigrateAsync();
					}
				}
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred while applying database migrations.");
				}
			}

			await app.RunAsync();
		}
	}
}
