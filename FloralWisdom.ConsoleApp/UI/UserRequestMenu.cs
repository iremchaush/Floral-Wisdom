using FloralWisdom.Data;
using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloralWisdom.ConsoleApp.UI
{
    public class UserRequestMenu(
        IUserRequestService userRequestService,
        IPlantService plantService,
        IUserService userService)
    {
        public async Task ShowMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== User Request Menu ===");
                Console.WriteLine("1. Show all user requests");
                Console.WriteLine("2. Add user request");
                Console.WriteLine("3. Delete user request");
                Console.WriteLine("4. Filter user request");
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

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        await FilterAsync();
                        break;

                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                    case ConsoleKey.Escape:
                        return;

                    default:
                        Console.WriteLine("\nInvalid choice. Press 1–4, 0 or ESC.");
                        break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        private async Task ShowAllAsync()
        {
            var requests = await userRequestService.GetAllAsync();
            Console.WriteLine("\n--- All User Requests ---");

            foreach (var r in requests)
            {
                Console.WriteLine($"{r.Name} | {r.User.Username} requested for {r.PlantType} preferably with colour {r.Colour}");
            }
        }

        private async Task AddAsync()
        {
            Console.WriteLine("\n=== User Requests ===");
            var requests = await userRequestService.GetAllAsync();
            foreach (var r in requests)
            {
                Console.WriteLine($"{r.Id}. {r.Name}");
            }

            string userId = ReadString("Enter User ID: ");

            Console.WriteLine("\n=== Available Plants ===");
            var plants = await plantService.GetAllAsync();
            foreach (var p in plants)
            {
                Console.WriteLine($"{p.Id}. {p.Name} ({p.ScientificName}) - {p.Description}");
            }

            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("Plant Type: ");
            var plantType = Console.ReadLine();

            Console.Write("Work Hours: ");
            var workHours = int.Parse(Console.ReadLine());

            Console.Write("Colour: ");
            var colour = Console.ReadLine();

            var userRequest = new UserRequest
            {
				Id = Guid.NewGuid().ToString(),
				Name = name!,
                PlantType = plantType!,
                WorkHours = workHours!,
                Colour = colour!
            };

            await userRequestService.AddAsync(userRequest);
			await userRequestService.SaveChangesAsync();
			Console.WriteLine("User Request recorded.");
        }

        private async Task DeleteAsync()
        {
            await ShowAllAsync();

            string id = ReadString("\nEnter User Request ID to delete: ");
            await userRequestService.DeleteAsync(id);
			await userRequestService.SaveChangesAsync();
			Console.WriteLine("User Request deleted.");
        }

        private async Task FilterAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Filter Requests ===");
            Console.WriteLine("1. By User");
            Console.WriteLine("2. By Name");
            Console.WriteLine("3. By Plant Type");
            Console.WriteLine("4. By Colour");
            Console.WriteLine("5. By Working Hours");
            Console.WriteLine("0. Back");
            Console.Write("Choice: ");

            var filter = Console.ReadLine();

            switch (filter)
            {
                case "1":
                    await FilterByUserAsync();
                    break;
                case "2":
                    await FilterByNameAsync();
                    break;
                case "3":
                    await FilterByPlantTypeAsync();
                    break;
                case "4":
                    await FilterByColourAsync();
                    break;
                case "5":
                    await FilterByWorkHoursAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private async Task FilterByUserAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Filter Sales by User ===");

            var users = await userService.GetAllAsync();
            foreach (var u in users)
            {
                Console.WriteLine($"{u.Id}. {u.Username} - {u.Email}");
            }

            string userId = ReadString("Enter User ID: ");

            var userRequest = await userRequestService.GetAllAsync();
            var filtered = userRequest.Where(ur => ur.UserId == userId).ToList();

            if (filtered.Count == 0)
            {
                Console.WriteLine("No requests found for the selected user.");
            }
            else
            {
                Console.WriteLine($"\nRequests for user ID {userId}:");
                foreach (var u in filtered)
                {
                    Console.WriteLine($"{u.Id}. {u.Name} | {u.User.Username} requested for {u.PlantType} preferably with colour {u.Colour}");
                }
            }
        }

        private async Task FilterByNameAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Filter User Requests by Name ===");

            string name = ReadString("Enter Request Name:");

            var requests = await userRequestService.GetAllAsync();
            var filtered = requests
                .Where(ur => ur.Name == name)
                .ToList();

            if (filtered.Count == 0)
            {
                Console.WriteLine("No requests found with that name.");
            }
            else
            {
                Console.WriteLine($"\nRequests {name}:");
                foreach (var u in filtered)
                {
                    Console.WriteLine($"{u.Id}. {u.Name} | {u.User.Username} requested for {u.PlantType} preferably with colour {u.Colour}");
                }
            }
        }

        private async Task FilterByPlantTypeAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Filter User Requests by Plant Type ===");

            string plantType = ReadString("Enter Plant Type:");

            var requests = await userRequestService.GetAllAsync();
            var filtered = requests
                .Where(ur => ur.PlantType == plantType)
                .ToList();

            if (filtered.Count == 0)
            {
                Console.WriteLine("No requests found with that plant type.");
            }
            else
            {
                Console.WriteLine($"\nRequests for {plantType}:");
                foreach (var u in filtered)
                {
                    Console.WriteLine($"{u.Id}. {u.Name} | {u.User.Username} requested for {u.PlantType} preferably with colour {u.Colour}");
                }
            }
        }

        private async Task FilterByColourAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Filter User Requests by Colour ===");

            string colour = ReadString("Enter Requested Colour:");

            var requests = await userRequestService.GetAllAsync();
            var filtered = requests
                .Where(ur => ur.Colour == colour)
                .ToList();

            if (filtered.Count == 0)
            {
                Console.WriteLine("No requests found with that colour.");
            }
            else
            {
                Console.WriteLine($"\nRequests with colour {colour}:");
                foreach (var u in filtered)
                {
                    Console.WriteLine($"{u.Id}. {u.Name} | {u.User.Username} requested for {u.PlantType} preferably with colour {u.Colour}");
                }
            }
        }

        private async Task FilterByWorkHoursAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Filter User Requests by Work Hours ===");

            int hours = ReadInt("Enter Work Hours:");

            var requests = await userRequestService.GetAllAsync();
            var filtered = requests
                .Where(ur => ur.WorkHours == hours)
                .ToList();

            if (filtered.Count == 0)
            {
                Console.WriteLine("No requests found with that work hours.");
            }
            else
            {
                Console.WriteLine($"\nRequests with {hours} work hours:");
                foreach (var u in filtered)
                {
                    Console.WriteLine($"{u.Id}. {u.Name} | {u.User.Username} requested for {u.PlantType} preferably with colour {u.Colour}");
                }
            }
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
                Console.WriteLine("Invalid string. Please try again.");
            }
            return input;
        }
        private static int ReadInt(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (int.TryParse(input, out value))
                    break;
                Console.WriteLine("Invalid number. Please try again.");
            }
            return value;
        }
    }
}

