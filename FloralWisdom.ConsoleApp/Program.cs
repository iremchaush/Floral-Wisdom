using FloralWisdom.Services.Implementations;
using FloralWisdom.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using FloralWisdom.ConsoleApp.UI;
namespace FloralWisdom.ConsoleApp
{
	public class Program
	{
		static async Task Main(string[] args)
		{

			var services = new ServiceCollection();

			services.AddTransient<UserMenu>();
			services.AddTransient<PlantsMenu>();
			services.AddTransient<CareReminderMenu>();
			services.AddTransient<UserRequestMenu>();
			services.AddTransient<DiseaseReportMenu>();

			ConfigureServices(services);

			var serviceProvider = services.BuildServiceProvider();

			var userMenu = serviceProvider.GetRequiredService<UserMenu>();
			 await userMenu.ShowMenuAsync();

			var plantMenu = serviceProvider.GetRequiredService<PlantsMenu>();
			 await plantMenu.ShowMenuAsync();

			var careReminderMenu = serviceProvider.GetRequiredService<CareReminderMenu>();
			 await careReminderMenu.ShowMenuAsync();

			var userRequestMenu = serviceProvider.GetRequiredService<UserRequestMenu>();
			await userRequestMenu.ShowMenuAsync();

			var diseaseReportMenu = serviceProvider.GetRequiredService<DiseaseReportMenu>();
			await diseaseReportMenu.ShowMenuAsync();


		}

		private static void ConfigureServices(ServiceCollection services)
		{
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IPlantService, PlantService>();
			services.AddScoped<ICareReminderService, CareReminderService>();
			services.AddScoped<IDiseaseReportService, DiseaseReportService>();
			services.AddScoped<IUserRequestService, UserRequestService>();
		}
	}
}
