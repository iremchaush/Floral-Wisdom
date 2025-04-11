using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloralWisdom.Models.Entities
{
    public class UserPlant
    {
        [Required]
        [ForeignKey(nameof(User))]
        public string ?UserId { get; set; }

        [Required]
        public virtual User ?User { get; set; }

        [Required]
        [ForeignKey(nameof(Plant))]
        public string ?PlantId { get; set; }

        [Required]
        public Plant ?Plant { get; set; }
    }
}
