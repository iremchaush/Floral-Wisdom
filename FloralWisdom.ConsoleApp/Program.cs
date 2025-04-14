using FloralWisdom.Services.Implementations;
using FloralWisdom.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using FloralWisdom.ConsoleApp.UI;
namespace FloralWisdom.ConsoleApp
{
	public class Program
	{
		static void Main(string[] args)
		{
			var services = new ServiceCollection();
			ConfigureServices(services);
			var serviceProvider = services.BuildServiceProvider();

			var plantMenu = serviceProvider.GetRequiredService<PlantsMenu>();
			plantMenu.Show();
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
