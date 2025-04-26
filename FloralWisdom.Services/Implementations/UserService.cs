using FloralWisdom.Data;
using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloralWisdom.Services.Implementations
{
	public class UserService : IUserService
	{
		private readonly FloralWisdomDbContext context;

		public UserService(FloralWisdomDbContext context)
		{
			this.context = context;
		}

		public async Task AddAsync(User user)
		{
			await context.Users.AddAsync(user);
		}

		public async Task SaveChangesAsync()
		{
			await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(string id)
		{
			var user = await context.Users.FindAsync(id);
			if (user != null)
			{
				context.Users.Remove(user);
			}
		}

		public async Task<List<User>> GetAllAsync()
		{
			return await context.Users.ToListAsync();
		}

		public async Task<User?> GetByIdAsync(string id)
		{
			var user = await context.Users.FindAsync(id);
			return user;
		}

		public async Task UpdateAsync(User user)
		{
			var existing = await context.Users.FindAsync(user.Id);
			if (existing == null)
			{
				throw new ArgumentException($"User with ID '{user.Id}' not found.");
			}

			existing.Username = user.Username;
			existing.Password = user.Password;
			existing.Email = user.Email;
		}
	}
}
