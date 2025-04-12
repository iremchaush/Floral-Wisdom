using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloralWisdom.Services.Implementations
{
	public class UserRequestService : IUserRequestService
	{
		private readonly List<UserRequest> _userRequests = new();
		public Task AddAsync(UserRequest userRequest)
		{
			userRequest.Id = Guid.NewGuid().ToString();
			_userRequests.Add(userRequest);
			return Task.CompletedTask;
		}

		public Task DeleteAsync(string id)
		{
			var request = _userRequests.FirstOrDefault(x => x.Id == id);
			if (request != null)
			{
				_userRequests.Remove(request);
			}

			return Task.CompletedTask;
		}

		public Task<List<UserRequest>> GetAllAsync()
		{
			return Task.FromResult(_userRequests.ToList());
		}

		public Task<UserRequest?> GetByIdAsync(string id)
		{
			var userRequest = _userRequests.FirstOrDefault(x => x.Id == id);
			return Task.FromResult(userRequest);
		}

		public Task UpdateAsync(UserRequest userRequest)
		{
			var existing = _userRequests.FirstOrDefault(x => x.Id==userRequest.Id);
			if (existing == null)
			{
				throw new ArgumentException($"User request with ID '{userRequest.Id}' not found.");
			}

			existing.WorkHours = userRequest.WorkHours;
			existing.Colours = userRequest.Colours;
			existing.Name = userRequest.Name;
			existing.User=userRequest.User;
			existing.Plant=userRequest.Plant;
			existing.UserId = userRequest.UserId;

			return Task.CompletedTask;
		}
	}
}
