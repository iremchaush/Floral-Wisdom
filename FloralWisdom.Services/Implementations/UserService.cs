using FloralWisdom.Data;
using FloralWisdom.Data.Repositories;
using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Interfaces;
using FloralWisdom.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloralWisdom.Services.Implementations
{
	public class UserService(IRepository<User, string> userRepository) : IUserService
	{

		public async Task CreateUserAsync(UserViewModel userViewModel)
		{
			var user = new User()
			{
				Id = Guid.NewGuid().ToString(),
				Username = userViewModel.Username,
				Email = userViewModel.Email,
				Password = userViewModel.Password
			};
			await userRepository.AddAsync(user);
		}

		public async Task DeleteUserAsync(string id)
		{
			User user = await userRepository.GetByIdAsync(id)
			?? throw new ArgumentException($"User with id `{id}` not found");

			if (!await userRepository.DeleteAsync(user))
			{
				throw new ArgumentException($"User with id `{id}` couldn't be removed");
			}
		}

		public async Task<List<User>> GetAllAsync()
		{
			return await userRepository.GetAllAttached().ToListAsync();
		}

		public async Task<User?> GetByIdAsync(string id)
		{
			User user = await userRepository.GetByIdAsync(id)
			?? throw new ArgumentException($"User with id `{id}` not found");

			return user;
		}

		public async Task UpdateUserAsync(UserViewModel userViewModel)
		{
			var existingUser= await userRepository
			.GetByIdAsync(userViewModel.Id)
			?? throw new ArgumentException($"User with id `{userViewModel.Id}` not found");

			existingUser.Username = userViewModel.Username;
			existingUser.Email = userViewModel.Email;
			existingUser.Password = userViewModel.Password;

			if (!await userRepository.UpdateAsync(existingUser))
			{
				throw new ArgumentException($"User with id `{userViewModel.Id}` couldn't be updated");
			}
		}
	}
}
