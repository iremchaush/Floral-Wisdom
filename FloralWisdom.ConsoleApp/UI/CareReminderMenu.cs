using FloralWisdom.Data;
using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Interfaces;
using FloralWisdom.Services.ViewModels;

namespace FloralWisdom.ConsoleApp.UI
{
	public class CareReminderMenu(
		ICareReminderService careReminderService,
		IPlantService plantService)
	{
		public async Task ShowMenuAsync()
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("=== Plant Care Reminder Menu ===");
				Console.WriteLine("1. Show all care reminders");
				Console.WriteLine("2. Add care reminder");
				Console.WriteLine("3. Delete care reminder");
				Console.WriteLine("0. Back to main menu (or press ESC)");
				Console.Write("Choice: ");

				var key = Console.ReadKey(true);

				switch (key.Key)
				{
					case ConsoleKey.D1:
					case ConsoleKey.NumPad1:
						await ShowAllAsync();
						break;
					case ConsoleKey.D2:
					case ConsoleKey.NumPad2:
						await AddAsync();
						break;
					case ConsoleKey.D3:
					case ConsoleKey.NumPad3:
						await DeleteAsync();
						break;
					case ConsoleKey.D0:
					case ConsoleKey.NumPad0:
					case ConsoleKey.Escape:
						return;
					default:
						Console.WriteLine("\nInvalid choice. Press 1–3, 0 or ESC.");
						break;
				}

				Console.WriteLine("\nPress Enter to continue...");
				Console.ReadLine();
			}
		}

		private async Task ShowAllAsync()
		{
			var reminders = await careReminderService.GetAllAsync();
			Console.WriteLine("\n--- All Care Reminders ---");

			foreach (var r in reminders)
			{
				Console.WriteLine($"Id: {r.Id} | {r.Remindertype} due on {r.NextDueDate:dd MMM yyyy} | Scientific name/: {r.Plant?.ScientificName}");
			}
		}

		private async Task AddAsync()
		{
			Console.Clear();
			Console.WriteLine("=== Add New Care Reminder ===");

			var plants = await plantService.GetAllAsync();
			foreach (var p in plants)
			{
				Console.WriteLine($"{p.Id}. {p.Name} ({p.ScientificName})");
			}

			string plantId = ReadString("Enter Plant ID: ");
			string type = ReadString("Enter Reminder Type (e.g., Watering, Pruning): ");
			DateTime date = ReadDate("Enter Next Due Date (yyyy-MM-dd): ");

			var reminder = new CareReminderViewModel
			{
				Id = Guid.NewGuid().ToString(),
				Remindertype = type,
				NextDueDate = date,
				PlantId = plantId
			};

			await careReminderService.CreateCareReminderAsync(reminder);
			Console.WriteLine("Care reminder added successfully.");
			
		}

		private async Task DeleteAsync()
		{
			await ShowAllAsync();

			string id = ReadString("\nEnter Reminder ID to delete: ");
			await careReminderService.DeleteCareReminderAsync(id);
			Console.WriteLine("Care reminder deleted.");
		}

		// ========== Helper Methods ==========

		private static string ReadString(string prompt)
		{
			string input;
			while (true)
			{
				Console.Write(prompt);
				input = Console.ReadLine();
				if (!string.IsNullOrEmpty(input))
					break;
				Console.WriteLine("Invalid input. Please try again.");
			}
			return input;
		}

		private static DateTime ReadDate(string prompt)
		{
			DateTime date;
			while (true)
			{
				Console.Write(prompt);
				var input = Console.ReadLine();
				if (DateTime.TryParse(input, out date))
					break;
				Console.WriteLine("Invalid date. Use format yyyy-MM-dd.");
			}
			return date;
		}
	}
}
