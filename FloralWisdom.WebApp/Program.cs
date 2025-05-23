using FloralWisdom.Services.Interfaces;
using FloralWisdom.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using FloralWisdom.Data;
using FloralWisdom.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FloralWisdom.WebApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            // DbContext 
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<FloralWisdomDbContext>(options =>
                options.UseSqlServer(connectionString));

			//Services
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped<IPlantService, PlantService>();
			builder.Services.AddScoped<ICareReminderService, CareReminderService>();
			builder.Services.AddScoped<IDiseaseReportService, DiseaseReportService>();
			builder.Services.AddScoped<IUserRequestService, UserRequestService>();

			builder.Services.AddControllersWithViews();
            builder.Services.AddScoped(typeof(IRepository<,>), typeof(ProfRepository<,>));

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

	//		builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	//.AddCookie(options =>
	//{
	//	options.LoginPath = "/Account/Login";
	//	options.LogoutPath = "/Account/Logout";
	//});

			app.UseAuthentication();
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
