namespace ChemicalLaboratory.Domain.DTOs.ReagentsDTO
{
    public record ReagentExpirationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? ExpirationDate { get; set; }
        public string DaysRemaining { get; set; } = string.Empty;// Разница в днях
    }
}
