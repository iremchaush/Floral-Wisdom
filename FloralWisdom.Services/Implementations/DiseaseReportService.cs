using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Interfaces;
using FloralWisdom.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using FloralWisdom.Data.Repositories;
namespace FloralWisdom.Services.Implementations
{
	public class DiseaseReportService(IRepository<DiseaseReport,string> diseaseReportRepository) : IDiseaseReportService
	{
		public async Task CreateDiseaseReportAsync(DiseaseReportViewModel diseaseReportViewModel)
		{
			var diseaseReport = new DiseaseReport()
			{
				Id = Guid.NewGuid().ToString(),
				Diagnosis = diseaseReportViewModel.Diagnosis,
				RecommendedTreatment=diseaseReportViewModel.RecommendedTreatment,
				PlantId =	diseaseReportViewModel.PlantId,
				Plant = diseaseReportViewModel.Plant
			};
			await diseaseReportRepository
				.AddAsync(diseaseReport);
		}

		public async Task DeleteDiseaseReportAsync(string id)
		{
			DiseaseReport diseaseReport = await diseaseReportRepository
				.GetByIdAsync(id)
				?? throw new ArgumentException($"Disease report with id `{id}` not found");

			if (!await diseaseReportRepository.DeleteAsync(diseaseReport))
			{
				throw new ArgumentException($"Disease report with id `{id}` couldn't be removed");
			}
		}
		 
		public async Task<List<DiseaseReport>> GetAllAsync()
		{
			return await diseaseReportRepository
					.GetAllAttached()
					.Include(r => r.Plant)
					.ToListAsync();
		}

		public async Task<DiseaseReport?> GetByIdAsync(string id)
		{
			DiseaseReport diseaseReport = await diseaseReportRepository.GetByIdAsync(id)
			?? throw new ArgumentException($"Disease report with id `{id}` not found");

			return diseaseReport; 
		}

		public async Task UpdateDiseaseReportAsync(DiseaseReportViewModel diseaseReportViewModel)
		{
			var existingReport = await diseaseReportRepository
				.GetByIdAsync(diseaseReportViewModel.Id)
				?? throw new ArgumentException($"Disease report with id `{diseaseReportViewModel.Id}` not found");

			existingReport.Diagnosis = diseaseReportViewModel.Diagnosis;
			existingReport.RecommendedTreatment = diseaseReportViewModel.RecommendedTreatment;
			existingReport.PlantId = diseaseReportViewModel.PlantId;

			if (!await diseaseReportRepository.UpdateAsync(existingReport))
			{
				throw new ArgumentException($"Disease report with id `{diseaseReportViewModel.Id}` couldn't be updated");
			}
		}
	}
}
