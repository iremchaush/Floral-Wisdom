using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloralWisdom.Models.Entities
{
    public class Plant
    {
        [Key]
        public string? Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string? ScientificName { get; set; }

        [Required]
        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public int WateringFrequency { get; set; }

        [Required]
        public string? SunlightRequirement { get; set; }

    }
}
