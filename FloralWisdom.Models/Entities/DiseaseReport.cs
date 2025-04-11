using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloralWisdom.Models.Entities
{
    public class DiseaseReport
    {
        [Key]
        public string? Id { get; set; }


        //Description/Image Path


        [Required]
        [MaxLength(100)]
        public string? Diagnosis { get; set; }

        [Required]
        [MaxLength(100)]
        public string? RecommendedTreatment { get; set; }

        [Required]
        [ForeignKey(nameof(Plant))]
        public string? PlantId { get; set; }

        [Required]
        public virtual Plant? Plant { get; set; }
    }
}
