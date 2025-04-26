using FloralWisdom.Models.Entities;

namespace FloralWisdom.Services.Interfaces
{
	public interface IPlantService
    {
        Task<List<Plant>> GetAllAsync();
        Task<Plant?> GetByIdAsync(string id);
        Task AddAsync(Plant plant);
        Task UpdateAsync(Plant plant);
        Task DeleteAsync(string id);
		Task SaveChangesAsync();
	}
}
