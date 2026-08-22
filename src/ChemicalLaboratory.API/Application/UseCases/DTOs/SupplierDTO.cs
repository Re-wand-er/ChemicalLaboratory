namespace ChemicalLaboratory.Application.UseCases.DTOs
{
    public record class SupplierDTO
    (
        int Id,
        string Name,
        string? ContactInfo,
        string? Address,
        DateTime? DeletedAt,
        bool IsActive = true
    );
}
