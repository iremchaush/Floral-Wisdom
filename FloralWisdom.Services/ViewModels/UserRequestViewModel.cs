using FloralWisdom.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloralWisdom.Services.ViewModels
{
	public class UserRequestViewModel
	{
		public string? Id { get; set; }

		public string Name { get; set; } = null!;

		public int WorkHours { get; set; }

		public string PlantType { get; set; } = string.Empty;

		public string Colour { get; set; } = string.Empty;

		public string UserId { get; set; } = string.Empty;

		public virtual User? User { get; set; }

		public virtual ICollection<Plant> Plant { get; set; } = new List<Plant>();
	}
}
