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
	public class CareReminderViewModel
	{
		public string? Id { get; set; }

		public string? Remindertype { get; set; }

		public DateTime NextDueDate { get; set; }

		public string? PlantId { get; set; }

		public virtual Plant? Plant { get; set; }
	}
}
