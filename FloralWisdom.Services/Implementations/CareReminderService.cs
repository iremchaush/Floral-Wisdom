using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloralWisdom.Services.Implementations
{
	public class CareReminderService : ICareReminderService
	{
		private readonly List<CareReminder> _careReminders = new();
		public Task AddAsync(CareReminder careReminder)
		{
			careReminder.Id = Guid.NewGuid().ToString();
			_careReminders.Add(careReminder);
			return Task.CompletedTask;
		}

		public Task DeleteAsync(string id)
		{
			var careReminder = _careReminders.FirstOrDefault(x => x.Id == id);
			if (careReminder != null)
			{
				_careReminders.Remove(careReminder);
			}
			return Task.CompletedTask;
		}

		public Task<List<CareReminder>> GetAllAsync()
		{
			return Task.FromResult(_careReminders.ToList());
		}

		public Task<CareReminder?> GetByIdAsync(string id)
		{
			var careReminder = _careReminders.FirstOrDefault(x => x.Id==id);
			return Task.FromResult(careReminder);
		}

		public Task UpdateAsync(CareReminder careReminder)
		{
			var existing = _careReminders.FirstOrDefault(x => x.Id==careReminder.Id);
			if (existing==null)
			{
				throw new ArgumentException($"Care reminder with ID '{careReminder.Id}' not found.");
			}

			existing.NextDueDate=careReminder.NextDueDate;
			existing.Remindertype = careReminder.Remindertype;
			existing.Plant=careReminder.Plant;
			existing.PlantId=careReminder.PlantId;

			return Task.CompletedTask;
		}
	}
}
