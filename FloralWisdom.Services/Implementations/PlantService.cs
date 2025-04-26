using FloralWisdom.Data;
using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloralWisdom.Services.Implementations
{
	public class PlantService : IPlantService
	{
		private readonly FloralWisdomDbContext context;

		public PlantService(FloralWisdomDbContext context)
		{
			this.context = context;
		}

		public async Task<List<Plant>> GetAllAsync()
		{
			return await context.Plants.ToListAsync();
		}

		public async Task<Plant?> GetByIdAsync(string id)
		{
			var plant = await context.Plants.FindAsync(id);
			return plant;
		}

		public async Task AddAsync(Plant plant)
		{
			await context.Plants.AddAsync(plant);
		}

		public async Task SaveChangesAsync()
		{
			await context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Plant plant)
		{
			var existing = await context.Plants.FindAsync(plant.Id);

			if (existing == null)
			{
				throw new ArgumentException($"Plant with ID '{plant.Id}' not found.");
			}

			existing.Name = plant.Name;
			existing.ScientificName = plant.ScientificName;
			existing.Description = plant.Description;
			existing.WateringFrequency = plant.WateringFrequency;
			existing.SunlightRequirement = plant.SunlightRequirement;
		}

		public async Task DeleteAsync(string id)
		{
			var plant = await context.Plants.FindAsync(id);
			if (plant != null)
			{
				context.Plants.Remove(plant);
			}
		}
	}
}
