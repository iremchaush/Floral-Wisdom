using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloralWisdom.Services.Implementations
{
	public class PlantService : IPlantService
	{
		private readonly List<Plant> _plants = new();

		public Task<List<Plant>> GetAllAsync()
		{
			return Task.FromResult(_plants.ToList());
		}

		public Task<Plant?> GetByIdAsync(string id)
		{
			var plant = _plants.FirstOrDefault(p => p.Id == id);
			return Task.FromResult(plant);
		}

		public Task AddAsync(Plant plant)
		{
			// Generate a unique ID if not provided
			plant.Id = Guid.NewGuid().ToString();
			_plants.Add(plant);
			return Task.CompletedTask;
		}

		public Task UpdateAsync(Plant plant)
		{
			var existing = _plants.FirstOrDefault(p => p.Id == plant.Id);
			if (existing == null)
			{
				throw new ArgumentException($"Plant with ID '{plant.Id}' not found.");
			}

			existing.Name = plant.Name;
			existing.ScientificName = plant.ScientificName;
			existing.Description = plant.Description;
			existing.WateringFrequency = plant.WateringFrequency;
			existing.SunlightRequirement = plant.SunlightRequirement;

			return Task.CompletedTask;
		}

		public Task DeleteAsync(string id)
		{
			var plant = _plants.FirstOrDefault(p => p.Id == id);
			if (plant != null)
			{
				_plants.Remove(plant);
			}

			return Task.CompletedTask;
		}
	}
}
