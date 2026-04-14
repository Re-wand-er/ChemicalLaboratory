namespace ChemicalLaboratory.WebApi.Models
{
    public record class ReagentCreateDTO
    (
        string Name,
        string? ChemicalFormula,
        string Unit,
        decimal CurrentQuantity,
        decimal MinQuantity,
        DateTime? ExpirationDate,
        string? StorageLocation,
        int CategoryId,
        bool IsActive
    );
}
