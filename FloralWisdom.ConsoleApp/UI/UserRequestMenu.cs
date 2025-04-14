using FloralWisdom.Data;
using FloralWisdom.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloralWisdom.ConsoleApp.UI
{
	public static void ShowMenu(FloralWisdomDbContext context)
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine("=== User Request Menu ===");
			Console.WriteLine("1. Show all User Requests");
			Console.WriteLine("2. Add User Request");
			Console.WriteLine("3. Edit User Request");
			Console.WriteLine("4. Delete User Request");
			Console.WriteLine("0. Back to main menu");
			Console.Write("Choice: ");

			var choice = Console.ReadLine();

			switch (choice)
			{
				case "1": ShowAll(context); break;
				case "2": Add(context); break;
				case "3": Edit(context); break;
				case "4": Delete(context); break;
				case "0": return;
				default: Console.WriteLine("Invalid option!"); break;
			}

			Console.WriteLine("\nPress Enter to continue...");
			Console.ReadLine();
		}
	}

	private static void ShowAll(FloralWisdomDbContext context)
	{
		var userRequests = context.UserRequest.Include(a => a.User).ToList();

		Console.WriteLine("\n--- All User Requests ---");
		foreach (var userRequest in userRequests)
		{
			Console.WriteLine($"{userRequest.Id}. {userRequest.Name} - {userRequest.PlantType}, {userRequest.WorkHours}, {userRequest.Clour} ({userRequest.User.Name})");
		}
	}

	private static void Add(FloralWisdomDbContext context)
	{
		Console.Write("Name: ");
		var name = Console.ReadLine();

		Console.Write("Plant Type (e.g. ): ");
		var plantType = Console.ReadLine();

		Console.Write("Work Hours: ");
		int workHours = int.Parse(Console.ReadLine());

		Console.Write("Colour (e.g. pink, red, yellow): ");
		string colour = Console.ReadLine();

		Console.WriteLine("\nAvailable Plants:");
		var plants = context.Plants.ToList();
		foreach (var plant in plants)
		{
			Console.WriteLine($"{plant.Id}. {plant.Name}");
		}

		Console.Write("Select Plant ID: ");
		string userId = Console.ReadLine();

		var userRequest = new UserRequest
		{
			Name = name!,
			PlantType = plantType!,
			WorkHours = workHours!,
			Colours = colour!,
			UserId = userId
		};

		context.UserRequests.AddAsync(userRequest);
		context.SaveChanges();

		Console.WriteLine("User Request added.");
	}

	private static void Edit(FloralWisdomDbContext context)
	{

		Console.Write("\nEnter ID to edit: ");
		if (int.TryParse(Console.ReadLine(), out int id))
		{
			var userRequest = context.UserRequests.Find(id);
			if (userRequest == null)
			{
				Console.WriteLine("User Request not found.");
				return;
			}

			Console.Write($"Name ({plant.Name}): ");
			var name = Console.ReadLine();
			Console.Write($"Plant Type ({plant.PlantType}): ");
			var plantType = Console.ReadLine();
			Console.Write($"Work Hours ({plant.WorkHours}): ");
			var workHours = Console.ReadLine();
			Console.Write($"Colour ({plant.Colour}): ");
			var colour = Console.ReadLine();

			Console.WriteLine("\nAvailable Plants:");
			var plants = context.Plants.ToList();
			foreach (var plant in plants)
			{
				Console.WriteLine($"{plant.Id}. {plant.Name}");
			}

			Console.Write($"User Request ID ({userRequest.Id}): ");
			string userRequestId = Console.ReadLine();

			userRequest.Name = string.IsNullOrWhiteSpace(name) ? userRequest.Name : name;
			userRequest.PlantType = string.IsNullOrWhiteSpace(plantType) ? userRequest.PlantType : plantType;
			userRequest.WorkHours = string.TryParse(Console.ReadLine(), out int workHours) ? userRequest.WorkHours : workHours;
			userRequest.Colours = string.IsNullOrWhiteSpace(colour) ? userRequest.Colours : colour;
			userRequest.UserId = userRequestId != null ? userRequestId : userRequestId;

			context.SaveChanges();
			Console.WriteLine("User Request updated.");
		}
	}

	private static void Delete(FloralWisdomDbContext context)
	{
		string id = Console.ReadLine();
		Console.Write("\nEnter ID to delete: ");
		if (string.IsNullOrWhiteSpace(id))
		{
			var userRequest = context.UserRequests.Find(id);
			if (userRequest == null)
			{
				Console.WriteLine("User Request not found.");
				return;
			}

			context.UserRequests.Remove(userRequest);
			context.SaveChanges();
			Console.WriteLine("User Request deleted.");
		}
	}
}

