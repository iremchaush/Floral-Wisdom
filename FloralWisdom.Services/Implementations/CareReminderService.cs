
using FloralWisdom.Data.Repositories;
using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Interfaces;
using FloralWisdom.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FloralWisdom.Services.Implementations
{
	public class CareReminderService(
	IRepository<CareReminder, string> careReminderRepository)
	: ICareReminderService
	{
		public async Task CreateCareReminderAsync(CareReminderViewModel careReminderViewModel)
		{
			var careReminder = new CareReminder()
			{
				Id = Guid.NewGuid().ToString(),
				Remindertype = careReminderViewModel.Remindertype,
				NextDueDate = careReminderViewModel.NextDueDate,
				PlantId = careReminderViewModel.PlantId,
				Plant = careReminderViewModel.Plant	
			};
			await careReminderRepository
				.AddAsync(careReminder);
		}

		public async Task DeleteCareReminderAsync(string id)
		{
			CareReminder careReminder = await careReminderRepository
				.GetByIdAsync(id)
				?? throw new ArgumentException($"Care reminder with id `{id}` not found");

			if (!await careReminderRepository.DeleteAsync(careReminder))
			{
				throw new ArgumentException($"Care reminder with id `{id}` couldn't be removed");
			}
		}

		public async Task<List<CareReminder>> GetAllAsync()
		{
			return await careReminderRepository
					.GetAllAttached()
					.Include(r => r.Plant)
					.ToListAsync();
		}

		public async Task<CareReminder?> GetByIdAsync(string id)
		{
			CareReminder careReminder = await careReminderRepository.GetByIdAsync(id)
			?? throw new ArgumentException($"Care reminder with id `{id}` not found");

			return careReminder;
		}

		public async Task UpdateCareReminderAsync(CareReminderViewModel careReminderViewModel)
		{
			var existingReminder = await careReminderRepository
				.GetByIdAsync(careReminderViewModel.Id)
				?? throw new ArgumentException($"Care reminder with id `{careReminderViewModel.Id}` not found");

			existingReminder.Remindertype = careReminderViewModel.Remindertype;
			existingReminder.NextDueDate = careReminderViewModel.NextDueDate;
			existingReminder.PlantId = careReminderViewModel.PlantId;

			if (!await careReminderRepository.UpdateAsync(existingReminder))
			{
				throw new ArgumentException($"Care reminder with id `{careReminderViewModel.Id}` couldn't be updated");
			}
		}
	}
}
