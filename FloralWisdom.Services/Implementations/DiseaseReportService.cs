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
	public class DiseaseReportService : IDiseaseReportService
	{
		private readonly FloralWisdomDbContext context;

		public DiseaseReportService(FloralWisdomDbContext context)
		{
			this.context = context;
		}
		public async Task AddAsync(DiseaseReport diseaseReport)
		{
			await context.DiseaseReports.AddAsync(diseaseReport);
		}
		public async Task SaveChangesAsync()
		{
			await context.SaveChangesAsync();
		}
		public async Task DeleteAsync(string id)
		{
			var diseaseReport = await context.DiseaseReports.FindAsync(id);
			if (diseaseReport != null)
			{
				context.DiseaseReports.Remove(diseaseReport);			
			}
		}

		public async Task<List<DiseaseReport>> GetAllAsync()
		{
			return await context.DiseaseReports.ToListAsync();
		}

		public async Task<DiseaseReport?> GetByIdAsync(string id)
		{
			var diseaseReport = await context.DiseaseReports.FindAsync(id);
			return diseaseReport;
		}

		public async Task UpdateAsync(DiseaseReport diseaseReport)
		{
			var existing = await context.DiseaseReports.FindAsync(diseaseReport.Id);
			if (existing == null)
			{
				throw new ArgumentException($"Disease report with ID '{diseaseReport.Id}' not found.");
			}

			existing.RecommendedTreatment = diseaseReport.RecommendedTreatment;
			existing.Diagnosis=diseaseReport.Diagnosis;
			existing.Plant=diseaseReport.Plant;
			existing.PlantId=diseaseReport.PlantId;
		}
	}
}
