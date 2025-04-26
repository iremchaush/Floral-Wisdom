using FloralWisdom.Models.Entities;

namespace FloralWisdom.Services.Interfaces
{
    public interface ICareReminderService
    {
        Task<List<CareReminder>> GetAllAsync();
        Task<CareReminder?> GetByIdAsync(string id);
        Task AddAsync(CareReminder careReminder);
        Task UpdateAsync(CareReminder careReminder);
        Task DeleteAsync(string id);
        Task SaveChangesAsync();
    }
}
