using FloralWisdom.Models.Entities;
using FloralWisdom.Services.ViewModels;

namespace FloralWisdom.Services.Interfaces
{
	public interface IDiseaseReportService
    {
        Task<List<DiseaseReport>> GetAllAsync();
        Task<DiseaseReport?> GetByIdAsync(string id);
        Task CreateDiseaseReportAsync(DiseaseReportViewModel diseaseReport);
        Task UpdateDiseaseReportAsync(DiseaseReportViewModel diseaseReport);
        Task DeleteDiseaseReportAsync(string id);
	}
}
