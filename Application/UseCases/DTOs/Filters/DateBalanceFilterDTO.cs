namespace ChemicalLaboratory.Application.UseCases.DTOs.Filters
{
    public record DateBalanceFilterDTO
    {
        public DateTime Date { get; set; }
        public int? CategoryId { get; set; }
        public bool? BelowMinimumOnly { get; set; }
    }
}
