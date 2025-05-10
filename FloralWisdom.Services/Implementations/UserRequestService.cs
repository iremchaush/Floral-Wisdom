using FloralWisdom.Data;
using FloralWisdom.Data.Repositories;
using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Interfaces;
using FloralWisdom.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace FloralWisdom.Services.Implementations
{
	public class UserRequestService(IRepository<UserRequest, string> userRequestRepository) : IUserRequestService
	{
		public async Task CreateUserRequestAsync(UserRequestViewModel userRequestViewModel)
		{
			var userRequest = new UserRequest()
			{
				Id = Guid.NewGuid().ToString(),
				Name = userRequestViewModel.Name,
				Colour = userRequestViewModel.Colour,
				Plant = userRequestViewModel.Plant,
				PlantType = userRequestViewModel.PlantType,
				User = userRequestViewModel.User,
				UserId = userRequestViewModel.UserId,
				WorkHours = userRequestViewModel.WorkHours,
			};
			await userRequestRepository.AddAsync(userRequest);
		}

		public async Task DeleteUserRequestAsync(string id)
		{
			UserRequest userRequest = await userRequestRepository.GetByIdAsync(id)
			?? throw new ArgumentException($"User request with id `{id}` not found");

			if (!await userRequestRepository.DeleteAsync(userRequest))
			{
				throw new ArgumentException($"User request with id `{id}` couldn't be removed");
			}
		}

		public async Task<List<UserRequest>> GetAllAsync()
		{
			return await userRequestRepository
			.GetAllAttached()
			.Include(r => r.User)
			.ToListAsync();
		}

		public async Task<UserRequest?> GetByIdAsync(string id)
		{
			UserRequest userRequest = await userRequestRepository.GetByIdAsync(id)
			?? throw new ArgumentException($"Disease report with id `{id}` not found");

			return userRequest;
		}

		public async Task UpdateUserRequestAsync(UserRequestViewModel userRequestViewModel)
		{
			var existingRequest = await userRequestRepository
			.GetByIdAsync(userRequestViewModel.Id)
			?? throw new ArgumentException($"User request with id `{userRequestViewModel.Id}` not found");

			existingRequest.Name = userRequestViewModel.Name;
			existingRequest.Colour = userRequestViewModel.Colour;
			existingRequest.Plant = userRequestViewModel.Plant;
			existingRequest.PlantType = userRequestViewModel.PlantType;
			existingRequest.User = userRequestViewModel.User;
			existingRequest.UserId = userRequestViewModel.UserId;
			existingRequest.WorkHours = userRequestViewModel.WorkHours;

			if (!await userRequestRepository.UpdateAsync(existingRequest))
			{
				throw new ArgumentException($"User request with id `{userRequestViewModel.Id}` couldn't be updated");
			}
		}
	}
}
