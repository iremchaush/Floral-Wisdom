using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloralWisdom.ConsoleApp.UI
{
	public class DiseaseReportMenu(
		IDiseaseReportService diseaseReportService,
		IPlantService plantService)
	{
		public async Task ShowMenuAsync()
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("=== Disease Report Menu ===");
				Console.WriteLine("1. Show all reports");
				Console.WriteLine("2. Add new report");
				Console.WriteLine("3. Delete report");
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
			var reports = await diseaseReportService.GetAllAsync();
			Console.WriteLine("\n--- All Disease Reports ---");

			foreach (var r in reports)
			{
				Console.WriteLine($"{r.Id}. Diagnosis: {r.Diagnosis} | Treatment: {r.RecommendedTreatment} | Plant name: {r.Plant?.Name}");
			}
		}

		private async Task AddAsync()
		{
			Console.Clear();
			Console.WriteLine("=== Add New Disease Report ===");

			var plants = await plantService.GetAllAsync();
			foreach (var p in plants)
			{
				Console.WriteLine($"{p.Id}. {p.Name} ({p.ScientificName})");
			}

			string plantId = ReadString("Enter Plant ID: ");
			string diagnosis = ReadString("Enter Diagnosis: ");
			string treatment = ReadString("Enter Recommended Treatment: ");

			var report = new DiseaseReport
			{
				Id = Guid.NewGuid().ToString(),
				Diagnosis = diagnosis,
				RecommendedTreatment = treatment,
				PlantId = plantId
			};

			await diseaseReportService.AddAsync(report);
			Console.WriteLine("Disease report added successfully.");
		}

		private async Task DeleteAsync()
		{
			await ShowAllAsync();

			string id = ReadString("\nEnter Report ID to delete: ");
			await diseaseReportService.DeleteAsync(id);
			Console.WriteLine("Report deleted.");
		}

		// ===== Helper Methods =====

		private static string ReadString(string prompt)
		{
			string input;
			while (true)
			{
				Console.Write(prompt);
				input = Console.ReadLine();
				if (!string.IsNullOrWhiteSpace(input))
					break;
				Console.WriteLine("Invalid input. Please try again.");
			}
			return input;
		}
	}
}


