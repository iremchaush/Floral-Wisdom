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
	public class UserPlantViewModel
	{
		public string? UserId { get; set; }

		public virtual User? User { get; set; }

		public string? PlantId { get; set; }

		public Plant? Plant { get; set; }
	}
}
