using FloralWisdom.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloralWisdom.Services.ViewModels
{
	public class DiseaseReportViewModel
	{
		public string? Id { get; set; }

		public string? Diagnosis { get; set; }

		public string? RecommendedTreatment { get; set; }

		public string? PlantId { get; set; }

		public virtual Plant? Plant { get; set; }
	}
}
