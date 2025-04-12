using FloralWisdom.Models.Entities;
using FloralWisdom.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloralWisdom.Services.Implementations
{
	public class DiseaseReportService : IDiseaseReportService
	{
		private readonly List<DiseaseReport> _diseases = new();
		public Task AddAsync(DiseaseReport diseaseReport)
		{
			diseaseReport.Id = Guid.NewGuid().ToString();
			_diseases.Add(diseaseReport);
			return Task.CompletedTask;
		}

		public Task DeleteAsync(string id)
		{
			var diseaseReport = _diseases.FirstOrDefault(d => d.Id == id);
			if (diseaseReport != null)
			{
				_diseases.Remove(diseaseReport);			
			}
			return Task.CompletedTask;
		}

		public Task<List<DiseaseReport>> GetAllAsync()
		{
			return Task.FromResult(_diseases.ToList());
		}

		public Task<DiseaseReport?> GetByIdAsync(string id)
		{
			var diseaseReport = _diseases.FirstOrDefault(x => x.Id==id);
			return Task.FromResult(diseaseReport);
		}

		public Task UpdateAsync(DiseaseReport diseaseReport)
		{
			var existing = _diseases.FirstOrDefault(x => x.Id==diseaseReport.Id);
			if (existing == null)
			{
				throw new ArgumentException($"Disease report with ID '{diseaseReport.Id}' not found.");
			}

			existing.RecommendedTreatment = diseaseReport.RecommendedTreatment;
			existing.Diagnosis=diseaseReport.Diagnosis;
			existing.Plant=diseaseReport.Plant;
			existing.PlantId=diseaseReport.PlantId;

			return Task.CompletedTask;
		}
	}
}
