namespace ChemicalLaboratory.Domain.DTOs.ReagentsDTO
{
    public class ReagentPredictionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal CurrentQuantity { get; set; }
        public decimal MinQuantity { get; set; }
        public string Unit { get; set; } = null!;
        public decimal AvgDailyConsumption { get; set; } 
        public DateTime? ExpirationDate { get; set; }
        public int? DaysToExpiry { get; set; }
    }
}
