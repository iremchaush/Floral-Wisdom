using FloralWisdom.Models.Entities;

namespace FloralWisdom.Services.Interfaces
{
    interface IDiseaseReportService
    {
        Task<List<DiseaseReport>> GetAllAsync();
        Task<DiseaseReport?> GetByIdAsync(string id);
        Task AddAsync(DiseaseReport diseaseReport);
        Task UpdateAsync(DiseaseReport diseaseReport);
        Task DeleteAsync(string id);
    }
}
