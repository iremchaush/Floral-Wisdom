using System.ComponentModel.DataAnnotations;


namespace FloralWisdom.Models.Entities
{
    public class User
    {
        [Key]
        public string?   Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
