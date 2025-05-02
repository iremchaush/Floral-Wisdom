using FloralWisdom.Services.Interfaces;
using FloralWisdom.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using FloralWisdom.Data;

namespace FloralWisdom.WebApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// DbContext 
			builder.Services.AddDbContext<FloralWisdomDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Server=localhost,1433;Database=FloralWisdom;User Id=sa;Password=#AniBonbon128;TrustServerCertificate=True;")));

			//Services
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped<IPlantService, PlantService>();
			builder.Services.AddScoped<ICareReminderService, CareReminderService>();
			builder.Services.AddScoped<IDiseaseReportService, DiseaseReportService>();
			builder.Services.AddScoped<IUserRequestService, UserRequestService>();

			builder.Services.AddControllersWithViews();

			var app = builder.Build();
			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseRouting();

			app.UseAuthorization();

			app.MapStaticAssets();
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}")
				.WithStaticAssets();

			app.Run();
		}
	}
}
