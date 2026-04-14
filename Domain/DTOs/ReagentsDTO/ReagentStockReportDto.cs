namespace ChemicalLaboratory.Domain.DTOs.ReagentsDTO
{
    public class ReagentStockReportDTO
    {
        public List<ItemDTO> Categories { get; set; } = new();
        public List<ItemDTO> Reagents { get; set; } = new();
    }
}
