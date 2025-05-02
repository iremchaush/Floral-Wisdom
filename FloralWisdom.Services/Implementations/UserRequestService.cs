using FloralWisdom.Data;
using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FloralWisdom.Services.Implementations
{
	public class UserRequestService : IUserRequestService
	{
		private readonly FloralWisdomDbContext context;

		public UserRequestService(FloralWisdomDbContext context)
		{
			this.context = context;
		}

		public async Task AddAsync(UserRequest userRequest)
		{
			await context.UserRequests.AddAsync(userRequest);
		}

		public async Task SaveChangesAsync()
		{
			await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(string id)
		{
			var request = await context.UserRequests.FindAsync(id);
			if (request != null)
			{
				context.UserRequests.Remove(request);
			}
		}

		public async Task<List<UserRequest>> GetAllAsync()
		{
			return await context.UserRequests.ToListAsync();
		}

		public async Task<UserRequest?> GetByIdAsync(string id)
		{
			var userRequest = await context.UserRequests.FindAsync(id);
			return userRequest;
		}

		public async Task UpdateAsync(UserRequest userRequest)
		{
			var existing = await context.UserRequests.FindAsync(userRequest.Id);
			if (existing == null)
			{
				throw new ArgumentException($"User request with ID '{userRequest.Id}' not found.");
			}

			existing.WorkHours = userRequest.WorkHours;
			existing.Colour = userRequest.Colour;
			existing.Name = userRequest.Name;
			existing.User=userRequest.User;
			existing.Plant=userRequest.Plant;
			existing.UserId = userRequest.UserId;
		}
	}
}
