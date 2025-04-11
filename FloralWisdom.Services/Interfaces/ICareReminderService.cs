using FloralWisdom.Models.Entities;

namespace FloralWisdom.Services.Interfaces
{
    interface ICareReminderService
    {
        Task<List<CareReminder>> GetAllAsync();
        Task<CareReminder?> GetByIdAsync(int id);
        Task AddAsync(CareReminder careReminder);
        Task UpdateAsync(CareReminder careReminder);
        Task DeleteAsync(int id);
    }
}
