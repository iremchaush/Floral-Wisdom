using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloralWisdom.Services.Implementations
{
	public class UserService : IUserService
	{
		private readonly List<User> _user = new();
		public Task AddAsync(User user)
		{
			user.Id = Guid.NewGuid().ToString();
			_user.Add(user);
			return Task.CompletedTask;
		}

		public Task DeleteAsync(string id)
		{
			var user = _user.FirstOrDefault(x => x.Id == id);
			if (user != null)
			{
				_user.Remove(user);
			}
			return Task.CompletedTask;
		}

		public Task<List<User>> GetAllAsync()
		{
			return Task.FromResult(_user.ToList());
		}

		public Task<User?> GetByIdAsync(string id)
		{
			var user = _user.FirstOrDefault(x => x.Id==id);
			return Task.FromResult(user);
		}

		public Task UpdateAsync(User user)
		{
			var existing = _user.FirstOrDefault(x => x.Id == user.Id);
			if (existing == null)
			{
				throw new ArgumentException($"User with ID '{user.Id}' not found.");
			}

			existing.Username = user.Username;
			existing.Password = user.Password;
			existing.Email = user.Email;

			return Task.CompletedTask;
		}
	}
}
