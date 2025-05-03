using Microsoft.Extensions.DependencyInjection;
using FloralWisdom.ConsoleApp.UI;
using FloralWisdom.Services.Interfaces;
using FloralWisdom.Services.Implementations;
using FloralWisdom.Services.ViewModels;
using FloralWisdom.Data;
using Microsoft.EntityFrameworkCore;
using FloralWisdom.Data.Repositories;
using FloralWisdom.Models.Entities;
namespace FloralWisdom.ConsoleApp
{
	public class Program
	{
		static async Task Main(string[] args)
		{


			var services = new ServiceCollection();
			services.AddDbContext<FloralWisdomDbContext>(options =>
				options.UseSqlServer("Server = localhost, 1433; Database = FloralWisdom; User Id = sa; Password =#AniBonbon128;TrustServerCertificate=True;"));


			services.AddTransient<UserMenu>();
			services.AddTransient<PlantsMenu>();
			services.AddTransient<CareReminderMenu>();
			services.AddTransient<UserRequestMenu>();
			services.AddTransient<DiseaseReportMenu>();

			ConfigureServices(services);
			RegisterRepositories(services);

			var serviceProvider = services.BuildServiceProvider();

			await ShowMainMenuAsync(serviceProvider);

		}

		private static void ConfigureServices(ServiceCollection services)
		{
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IPlantService, PlantService>();
			services.AddScoped<ICareReminderService, CareReminderService>();
			services.AddScoped<IDiseaseReportService, DiseaseReportService>();
			services.AddScoped<IUserRequestService, UserRequestService>();
		}
		public static void RegisterRepositories(ServiceCollection services)
		{
			services.AddScoped<IRepository<Plant, string>, ProfRepository<Plant, string>>();
			services.AddScoped<IRepository<CareReminder, string>, ProfRepository<CareReminder, string>>();
			services.AddScoped<IRepository<DiseaseReport, string>, ProfRepository<DiseaseReport, string>>();
			services.AddScoped<IRepository<User, string>, ProfRepository<User, string>>();
			services.AddScoped<IRepository<UserPlant, object>, ProfRepository<UserPlant, object>>();
			services.AddScoped<IRepository<UserRequest, string>, ProfRepository<UserRequest, string >>();
		}
		private static async Task ShowMainMenuAsync(ServiceProvider serviceProvider)
		{
			bool exitRequested = false;

			while (!exitRequested)
			{
				Console.Clear();
				Console.WriteLine("=== Floral Wisdom Main Menu ===");
				Console.WriteLine("1. User Management");
				Console.WriteLine("2. Plant Management");
				Console.WriteLine("3. Care Reminders");
				Console.WriteLine("4. User Requests");
				Console.WriteLine("5. Disease Reports");
				Console.WriteLine("0. Exit");
				Console.Write("Select an option: ");

				string input = Console.ReadLine();

				switch (input)
				{
					case "1":
						var userMenu = serviceProvider.GetRequiredService<UserMenu>();
						await userMenu.ShowMenuAsync();
						break;
					case "2":
						var plantMenu = serviceProvider.GetRequiredService<PlantsMenu>();
						await plantMenu.ShowMenuAsync();
						break;
					case "3":
						var careReminderMenu = serviceProvider.GetRequiredService<CareReminderMenu>();
						await careReminderMenu.ShowMenuAsync();
						break;
					case "4":
						var userRequestMenu = serviceProvider.GetRequiredService<UserRequestMenu>();
						await userRequestMenu.ShowMenuAsync();
						break;
					case "5":
						var diseaseReportMenu = serviceProvider.GetRequiredService<DiseaseReportMenu>();
						await diseaseReportMenu.ShowMenuAsync();
						break;
					case "0":
						exitRequested = true;
						Console.WriteLine("Exiting application. Goodbye!");
						break;
					default:
						Console.WriteLine("Invalid selection. Please try again.");
						break;
				}

				if (!exitRequested)
				{
					Console.WriteLine("\nPress any key to return to the main menu...");
					Console.ReadKey();
				}
			}
		}
	}
}
