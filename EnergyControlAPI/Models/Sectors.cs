namespace EnergyControlAPI.Models
{
    public class Sector
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Nome do setor (ex: "Administração", "Produção")
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
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
