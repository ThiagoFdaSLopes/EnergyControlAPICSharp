using System.ComponentModel.DataAnnotations.Schema;

namespace EnergyControlAPI.Models
{
    public class EquipmentModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public int SectorId { get; set; }

        // Propriedade de navegação para o relacionamento com Sector
        public Sector Sector { get; set; } = null!;
    }
}
