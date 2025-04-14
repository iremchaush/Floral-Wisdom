using FloralWisdom.Models;
using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Implementations;
using FloralWisdom.Services.Interfaces;

namespace FloralWisdom.ConsoleApp.UI
{
    public class PlantsMenu(IPlantService plantService)
    {
        public async Task ShowMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Plant Menu ===");
                Console.WriteLine("1. Show all plants");
                Console.WriteLine("2. Add plant");
                Console.WriteLine("3. Edit plant");
                Console.WriteLine("4. Delete plant");
                Console.WriteLine("0. Back to main menu");
                Console.Write("Choice: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": await ShowAllAsync(); break;
                    case "2": await AddAsync(); break;
                    case "3": await EditAsync(); break;
                    case "4": await DeleteAsync(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option!"); break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        private async Task ShowAllAsync()
        {
            var plants = await plantService.GetAllAsync();
            Console.WriteLine("\n--- All Plants ---");
            foreach (var plant in plants)
            {
                Console.WriteLine($"{plant.Id}. {plant.Name} ({plant.ScientificName}) - {plant.Description} | {plant.WateringFrequency} | {plant.SunlightRequirement}");
            }
        }

        private async Task AddAsync()
        {
            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("Scientific Name: ");
            var sciName = Console.ReadLine();

            Console.Write("Description: ");
            var description = Console.ReadLine();

            Console.Write("Watering Frequency: ");
            int.TryParse(Console.ReadLine(), out int water);

            Console.Write("Sunlight Requirement: ");
            var sunlight = Console.ReadLine();

            var plant = new Plant
            {
                Name = name!,
                ScientificName = sciName!,
                Description = description!,
                WateringFrequency = water!,
                SunlightRequirement = sunlight!
            };

            await plantService.AddAsync(plant);
            Console.WriteLine("Plant added.");
        }

        private async Task EditAsync()
        {
            await ShowAllAsync();

            Console.Write("\nEnter ID to edit: ");
            string id = Console.ReadLine();
            if (!string.IsNullOrEmpty(id))
            {
                var plant = await plantService.GetByIdAsync(id);
                if (plant == null)
                {
                    Console.WriteLine("Plant not found.");
                    return;
                }

                Console.Write($"Name ({plant.Name}): ");
                var name = Console.ReadLine();

                Console.Write($"Scientific Name ({plant.ScientificName}): ");
                var sciName = Console.ReadLine();

                Console.Write($"Description ({plant.Description}): ");
                var description = Console.ReadLine();

                Console.Write($"Water Frequency ({plant.WateringFrequency}): ");
                int.TryParse(Console.ReadLine(), out int water);

                Console.Write($"Sunlight Requirement ({plant.SunlightRequirement}): ");
                var sunlight = Console.ReadLine();

                plant.Name = string.IsNullOrWhiteSpace(name) ? plant.Name : name;
                plant.ScientificName = string.IsNullOrWhiteSpace(sciName) ? plant.ScientificName : sciName;
                plant.Description = string.IsNullOrWhiteSpace(description) ? plant.Description : description;
                plant.SunlightRequirement = string.IsNullOrWhiteSpace(sunlight) ? plant.SunlightRequirement : sunlight;
                if (water > 0) plant.WateringFrequency = water;

                await plantService.UpdateAsync(plant);
                Console.WriteLine("Plant updated.");
            }
        }

        private async Task DeleteAsync()
        {
            await ShowAllAsync();

            Console.Write("\nEnter ID to delete: ");
            string id = Console.ReadLine();
            if (!string.IsNullOrEmpty(id))
            {
                await plantService.DeleteAsync(id);
                Console.WriteLine("Plant deleted.");
            }
        }
    }

}
