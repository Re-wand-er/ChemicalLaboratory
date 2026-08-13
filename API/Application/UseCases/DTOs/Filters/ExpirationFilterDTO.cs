using ChemicalLaboratory.Domain.Enums;

namespace ChemicalLaboratory.Application.UseCases.DTOs
{
    public record ExpirationFilterDTO
    {
        public int DaysAhead { get; set; } = 90;    
        public ExpirationStatus Status { get; set; } = ExpirationStatus.All;
        public int? CategoryId { get; set; }
        public bool OnlyWithStock { get; set; } = true;
    }
}
