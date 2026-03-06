namespace ChemicalLaboratory.Application.UseCases.DTOs
{
    public record class ReagentDTO
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
        DateTime CreatedAt,
        bool IsActive
    );
}
