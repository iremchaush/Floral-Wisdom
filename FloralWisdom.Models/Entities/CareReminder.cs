using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloralWisdom.Models.Entities
{
    public class CareReminder
    {
        [Key]
        public string? Id { get; set; }

        [Required]
        public string? Remindertype { get; set; }

        [Required]
        public DateTime NextDueDate { get; set; }

        [Required]
        [ForeignKey(nameof(Plant))]
        public string? PlantId { get; set; }

        [Required]
        public virtual Plant? Plant { get; set; }
    }
}
