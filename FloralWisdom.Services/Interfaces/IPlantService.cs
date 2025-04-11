using FloralWisdom.Models.Entities;

namespace FloralWisdom.Services.Interfaces
{
    interface IPlantService
    {
        Task<List<Plant>> GetAllAsync();
        Task<Plant?> GetByIdAsync(int id);
        Task AddAsync(Plant plant);
        Task UpdateAsync(Plant plant);
        Task DeleteAsync(int id);
    }
}
