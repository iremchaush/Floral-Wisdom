using FloralWisdom.Models.Entities;
using FloralWisdom.Services.ViewModels;
namespace FloralWisdom.Services.Interfaces
{
    public interface ICareReminderService
    {
        Task<List<CareReminder>> GetAllAsync();
        Task<CareReminder?> GetByIdAsync(string id);
        Task CreateCareReminderAsync(CareReminderViewModel careReminderViewModel);
        Task UpdateCareReminderAsync(CareReminderViewModel careReminderViewModel);
        Task DeleteCareReminderAsync(string id);
    }
}
