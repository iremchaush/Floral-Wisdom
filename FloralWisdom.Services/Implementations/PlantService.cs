using FloralWisdom.Data;
using FloralWisdom.Data.Repositories;
using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Interfaces;
using FloralWisdom.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloralWisdom.Services.Implementations
{
	public class PlantService(IRepository<Plant, string> plantRepository) : IPlantService
	{
		public async Task CreatePlantAsync(PlantViewModel plantViewModel)
		{
			var plant = new Plant() {
				Id=Guid.NewGuid().ToString(),
				Name = plantViewModel.Name,
				ScientificName = plantViewModel.ScientificName,
				Description = plantViewModel.Description,
				SunlightRequirement = plantViewModel.SunlightRequirement,
				WateringFrequency = plantViewModel.WateringFrequency
			};
			await plantRepository.AddAsync(plant);
		}

		public async Task DeletePlantAsync(string id)
		{
			Plant plant = await plantRepository
				.GetByIdAsync(id)
				?? throw new ArgumentException($"Plant with id `{id}` not found");

			if (!await plantRepository.DeleteAsync(plant))
			{
				throw new ArgumentException($"Plant with id `{id}` couldn't be removed");
			}
		}

		public async Task<List<Plant>> GetAllAsync()
		{
			return await plantRepository.GetAllAttached().ToListAsync();
		}

		public async Task<Plant?> GetByIdAsync(string id)
		{
			Plant plant = await plantRepository.GetByIdAsync(id)
		?? throw new ArgumentException($"Plant with id `{id}` not found");

			return plant;
		}

		public async Task UpdatePlantAsync(PlantViewModel plantViewModel)
		{
			var existingPlant = await plantRepository
				.GetByIdAsync(plantViewModel.Id)
				?? throw new ArgumentException($"Plant with id `{plantViewModel.Id}` not found");

			existingPlant.Name= plantViewModel.Name;
			existingPlant.ScientificName = plantViewModel.ScientificName;
			existingPlant.Description = plantViewModel.Description;
			existingPlant.WateringFrequency = plantViewModel.WateringFrequency;
			existingPlant.SunlightRequirement = plantViewModel.SunlightRequirement;

			if (!await plantRepository.UpdateAsync(existingPlant))
			{
				throw new ArgumentException($"Plant with id `{plantViewModel.Id}` couldn't be updated");
			}
		}
	}
}
