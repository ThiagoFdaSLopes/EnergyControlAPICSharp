// DTO
namespace EnergyControlAPI.DTOs
{
    public class SectorDTO
    {
        public int Id { get; set; }

        /// <summary>
        /// Nome do setor (ex: "Administração", "Produção")
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Número do andar em que o setor está localizado
        /// </summary>
        public int FloorNumber { get; set; }

        /// <summary>
        /// Observações adicionais sobre o setor (opcional)
        /// </summary>
        public string? Description { get; set; }
    }
}
