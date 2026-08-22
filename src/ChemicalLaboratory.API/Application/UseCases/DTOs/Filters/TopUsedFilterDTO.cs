namespace ChemicalLaboratory.Application.UseCases.DTOs
{
    public record TopUsedFilterDTO
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int Top { get; set; } = 5;
        public int? CategoryId { get; set; }
        public decimal? MinUsage { get; set; }
    }
}
