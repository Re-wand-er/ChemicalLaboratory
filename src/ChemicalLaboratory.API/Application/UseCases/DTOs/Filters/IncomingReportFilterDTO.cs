namespace ChemicalLaboratory.Application.UseCases.DTOs.Filters
{
    public record IncomingReportFilterDTO
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? CategoryId { get; set; }
        public int? ReagentId { get; set; }
        public decimal? MinQuantity { get; set; }
    }
}
