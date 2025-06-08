using System.ComponentModel.DataAnnotations;

namespace EnergyControlAPI.DTOs
{
    public class UpdateUserDto
    {
        [Required, StringLength(200)]
        public required string Name { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Role { get; set; }
    }
}
