using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Interfaces;

namespace FloralWisdom.ConsoleApp.UI
{
    class UserMenu(IUserService userService)
    {
        public async Task ShowMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== User Menu ===");
                Console.WriteLine("1. Show all users");
                Console.WriteLine("2. Add user");
                Console.WriteLine("3. Edit user");
                Console.WriteLine("4. Delete user");
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
                    default: Console.WriteLine("Invalid choice!"); break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        private async Task ShowAllAsync()
        {
            var users = await userService.GetAllAsync();
            Console.WriteLine("\n--- All Users ---");
            foreach (var u in users)
            {
                Console.WriteLine($"{u.Id}. {u.Username} | {u.Email}");
            }
        }

        private async Task AddAsync()
        {
            Console.Write("Username: ");
            var name = Console.ReadLine();

            Console.Write("Email: ");
            var email = Console.ReadLine();
            
            Console.Write("Password: ");
            var password = Console.ReadLine();

            var user = new User
            {
                Username = name!,
                Email = email!,
                Password = password!
            };

            await userService.AddAsync(user);
            Console.WriteLine("User added.");
        }

        private async Task EditAsync()
        {
            await ShowAllAsync();

            Console.Write("\nEnter ID to edit: ");
            string id = Console.ReadLine();
            if (!string.IsNullOrEmpty(id))
            {
                var user = await userService.GetByIdAsync(id);
                if (user == null)
                {
                    Console.WriteLine("User not found.");
                    return;
                }

                Console.Write($"Name ({user.Username}): ");
                var name = Console.ReadLine();
                Console.Write($"Email ({user.Email}): ");
                var email = Console.ReadLine();

                user.Username = string.IsNullOrWhiteSpace(name) ? user.Username : name;
                user.Email = string.IsNullOrWhiteSpace(email) ? user.Email : email;

                await userService.UpdateAsync(user);
                Console.WriteLine("User updated.");
            }
        }

        private async Task DeleteAsync()
        {
            await ShowAllAsync();

            Console.Write("\nEnter ID to delete: ");
            string id = Console.ReadLine();
            if (!string.IsNullOrEmpty(id))
            {
                await userService.DeleteAsync(id);
                Console.WriteLine("User deleted.");
            }
        }
    }
}
