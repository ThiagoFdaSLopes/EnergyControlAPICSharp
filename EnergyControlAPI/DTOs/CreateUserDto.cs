﻿using System.ComponentModel.DataAnnotations;

namespace EnergyControlAPI.DTOs
{
    public class CreateUserDto
    {
        [Required, StringLength(200)]
        public required string Name { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required, MinLength(6)]
        public required string Password { get; set; }

        [Required]
        public required string Role { get; set; }
    }
}
