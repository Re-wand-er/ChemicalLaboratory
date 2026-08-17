namespace ChemicalLaboratory.Application.UseCases.DTOs
{
    public record ReagentDTO
    (
        int Id,
        string Name,
        string? ChemicalFormula,
        string Unit,
        decimal CurrentQuantity,
        decimal MinQuantity,
        DateTime? ExpirationDate,
        string? StorageLocation,
        int CategoryId,
        string? CategoryName,
        DateTime CreatedAt,
        DateTime? DeletedAt,
        bool IsActive
    );
}
