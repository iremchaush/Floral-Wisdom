using FloralWisdom.Models.Entities;
using FloralWisdom.Services.ViewModels;

namespace FloralWisdom.Services.Interfaces
{
	public interface IPlantService
    {
		Task<List<Plant>> GetAllAsync();
		Task<Plant?> GetByIdAsync(string id);
		Task CreatePlantAsync(PlantViewModel plantViewModel);
		Task UpdatePlantAsync(PlantViewModel plantViewModel);
		Task DeletePlantAsync(string id);
	}
}
