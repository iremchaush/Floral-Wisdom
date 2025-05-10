using FloralWisdom.Models.Entities;
using FloralWisdom.Services.ViewModels;


namespace FloralWisdom.Services.Interfaces
{
	public interface IUserService
    {
		Task<List<User>> GetAllAsync();
		Task<User?> GetByIdAsync(string id);
		Task CreateUserAsync(UserViewModel userViewModel);
		Task UpdateUserAsync(UserViewModel userViewModel);
		Task DeleteUserAsync(string id);
	}
}
