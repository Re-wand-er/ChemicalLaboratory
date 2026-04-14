namespace ChemicalLaboratory.Domain.DTOs.ReagentsDTO
{
    public class ReagentExpirationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? ExpirationDate { get; set; }
        public int DaysRemaining { get; set; } // Разница в днях
    }
}
