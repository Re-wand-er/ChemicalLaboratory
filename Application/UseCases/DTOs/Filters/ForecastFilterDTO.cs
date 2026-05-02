namespace ChemicalLaboratory.Application.UseCases.DTOs.Filters
{
    public record ForecastFilterDTO
    {
        public int ForecastDays { get; set; } = 90;
        public int? CategoryId { get; set; }

        public int? MaxDaysToZero { get; set; } = 999;

        public bool CriticalOnly { get; set; } = false;

        public bool OnlyReorderNeeded { get; set; } = false;

        public int Multiplier { get; set; } = 3;
    }
}
