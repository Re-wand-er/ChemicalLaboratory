namespace ChemicalLaboratory.Domain.DTOs.ReagentsDTO
{
    public class ReagentTurnoverDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal TotalConsumption { get; set; }
        public decimal AverageStock { get; set; }      
        public decimal TurnoverRatio { get; set; }    
    }
}
