namespace EnergyControlAPI.DTOs
{
    public class AuthResponseDto
    {
        public required string Token { get; set; }
        public DateTime Expires { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
    }
}
