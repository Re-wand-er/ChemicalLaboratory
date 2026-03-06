namespace ChemicalLaboratory.Application.UseCases.DTOs
{
    public record class SupplierDTO
    (
        int Id,
        string Name,
        string? ContactInfo,
        string? Address
    );
}
