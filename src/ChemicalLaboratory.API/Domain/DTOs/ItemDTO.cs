namespace ChemicalLaboratory.Domain.DTOs
{
    public record ItemDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
    }
}
