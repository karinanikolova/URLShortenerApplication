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
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			await app.RunAsync();
		}
	}
}
