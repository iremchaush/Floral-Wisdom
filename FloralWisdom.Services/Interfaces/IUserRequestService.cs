

using FloralWisdom.Models.Entities;

namespace FloralWisdom.Services.Interfaces
{
    interface IUserRequestService
    {
        Task<List<UserRequest>> GetAllAsync();
        Task<UserRequest?> GetByIdAsync(int id);
        Task AddAsync(UserRequest userRequest);
        Task UpdateAsync(UserRequest userRequest);
        Task DeleteAsync(int id);
    }
}
