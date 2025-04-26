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
	public class CareReminderService : ICareReminderService
	{
		private readonly FloralWisdomDbContext context;

		public CareReminderService(FloralWisdomDbContext context)
		{
			this.context = context;
		}
		public async Task AddAsync(CareReminder careReminder)
		{
			await context.CareReminders.AddAsync(careReminder);
		}
		public async Task SaveChangesAsync()
		{
			await context.SaveChangesAsync();
		}
		public async Task DeleteAsync(string id)
		{
			var careReminder = await context.CareReminders.FindAsync(id);
			if (careReminder != null)
			{
				context.CareReminders.Remove(careReminder);
			}
		}

		public async Task<List<CareReminder>> GetAllAsync()
		{
			return await context.CareReminders.ToListAsync();
		}

		public async Task<CareReminder?> GetByIdAsync(string id)
		{
			var careReminder = await context.CareReminders.FindAsync(id);
			return careReminder;
		}

		public async Task UpdateAsync(CareReminder careReminder)
		{
			var existing = await context.CareReminders.FindAsync(careReminder.Id);
			if (existing==null)
			{
				throw new ArgumentException($"Care reminder with ID '{careReminder.Id}' not found.");
			}

			existing.NextDueDate=careReminder.NextDueDate;
			existing.Remindertype = careReminder.Remindertype;
			existing.Plant=careReminder.Plant;
			existing.PlantId=careReminder.PlantId;
		}
	}
}
