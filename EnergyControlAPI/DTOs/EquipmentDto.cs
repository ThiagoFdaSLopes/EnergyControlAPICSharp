using EnergyControlAPI.Models;

namespace EnergyControlAPI.DTOs
{
    public class EquipmentDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public int SectorId { get; set; }

    }
}
