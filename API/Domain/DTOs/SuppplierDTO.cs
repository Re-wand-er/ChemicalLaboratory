namespace ChemicalLaboratory.Domain.DTOs
{
    public record SuppplierDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string? Description { get; set; }
        public string Kind { get; set; } = null!;
        public string? Status { get; set; }

    }
}
