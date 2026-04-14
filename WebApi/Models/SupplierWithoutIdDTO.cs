namespace ChemicalLaboratory.WebApi.Models
{
    public record SupplierWithoutIdDTO
    (
        string Name,
        string? ContactInfo,
        string? Address
    );
}
