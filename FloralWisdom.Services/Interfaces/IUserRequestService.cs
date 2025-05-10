

using FloralWisdom.Models.Entities;
using FloralWisdom.Services.ViewModels;

namespace FloralWisdom.Services.Interfaces
{
	public interface IUserRequestService
    {
		Task<List<UserRequest>> GetAllAsync();
		Task<UserRequest?> GetByIdAsync(string id);
		Task CreateUserRequestAsync(UserRequestViewModel userRequestViewModel);
		Task UpdateUserRequestAsync(UserRequestViewModel userRequestViewModel);
		Task DeleteUserRequestAsync(string id);
	}
}
