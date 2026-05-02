namespace ChemicalLaboratory.Domain.DTOs.ReagentsDTO
{
    public record IncomingReportDTO
    {
        public int Id { get; set; }
        public DateTime OperationDate { get; set; }
        public string ReagentName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }
}
