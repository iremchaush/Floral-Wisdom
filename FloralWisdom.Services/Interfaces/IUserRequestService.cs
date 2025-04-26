

using FloralWisdom.Models.Entities;

namespace FloralWisdom.Services.Interfaces
{
	public interface IUserRequestService
    {
        Task<List<UserRequest>> GetAllAsync();
        Task<UserRequest?> GetByIdAsync(string id);
        Task AddAsync(UserRequest userRequest);
        Task UpdateAsync(UserRequest userRequest);
        Task DeleteAsync(string id);
		Task SaveChangesAsync();
	}
}
