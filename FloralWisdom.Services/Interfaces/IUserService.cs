using FloralWisdom.Models.Entities;


namespace FloralWisdom.Services.Interfaces
{
	public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(string id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);
    }
}
