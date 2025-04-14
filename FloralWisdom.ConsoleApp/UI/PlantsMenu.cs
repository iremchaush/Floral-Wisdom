using FloralWisdom.Models;
using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Implementations;
using FloralWisdom.Services.Interfaces;

namespace FloralWisdom.ConsoleApp.UI
{
	public class PlantsMenu(IPlantService plantService)
	{
		public void Show()
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Plant Menu:");
				Console.WriteLine("1. Add Plant");
				Console.WriteLine("2. View All Plants");
				Console.WriteLine("3. Remove Plant");
				Console.WriteLine("0. Exit");
				Console.Write("Choose option: ");
				var choice = Console.ReadLine();
				switch (choice)
				{
					case "1":
						AddPlant();
						break;
					case "2":
						ViewPlants();
						break;
					case "3":
						RemovePlant();
						break;
					case "0":
						return;
					default:
						Console.WriteLine("Invalid choice. Press Enter to continue...");
						Console.ReadLine();
						break;
				}
			}
		}
		private void AddPlant()
		{
			Console.Write("Name: ");
			string name = Console.ReadLine()!;
			//Console.Write("Scientific Name: ");
			//string scientificName = Console.ReadLine()!;
			Console.Write("Description: ");
			string description = Console.ReadLine()!;
			Console.Write("Watering Frequency: ");
			int wateringFrequency = int.Parse(Console.ReadLine())!;
			Console.Write("Sunlight Requirement: ");
			string sunlightRequirement = Console.ReadLine()!;

			var plant = new Plant
			{
				Name = name,
				//ScientificName = scientificName,
				Description = description,
				WateringFrequency = wateringFrequency,
				SunlightRequirement = sunlightRequirement
			};
			plantService.AddAsync(plant);
			Console.WriteLine("Plant added! Press Enter to continue...");
			Console.ReadLine();
		}
		private void ViewPlants()
		{
			var plants = plantService.GetAllAsync();
			Console.WriteLine("\nAll Plants:");
			foreach (var plant in plants)
			{
				Console.WriteLine($"ID: {plant.Id}, Name: {plant.Name}, Scientific Name: {plant.ScientificName} Description: {plant.Description}, Watering Frequency: {plant.WateringFrequency}, Sunlight Requirement: {plant.SunlightRequirement}");
			}
			Console.WriteLine("\nPress Enter to continue...");
			Console.ReadLine();
		}
		private void RemovePlant()
		{
			string id = Console.ReadLine();
			Console.Write("Enter Plant ID to remove: ");
			if (!string.IsNullOrWhiteSpace(id))
			{
				plantService.DeleteAsync(id);
				Console.WriteLine("Plant removed. Press Enter to continue...");
			}
			else
			{
				Console.WriteLine("Invalid ID. Press Enter to continue...");
			}
			Console.ReadLine();
		}
	}
}
