namespace ChemicalLaboratory.Domain.DTOs.ReagentsDTO
{
    public record TopUsedReagentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal TotalUsed { get; set; }
        public string Unit { get; set; } = string.Empty;
        public int UsageCount { get; set; }
    }
}
