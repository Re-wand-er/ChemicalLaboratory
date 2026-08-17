namespace ChemicalLaboratory.WebApi.Models
{
    public record ChangePasswordDTO
    (
        string Email,
        string Code,
        string Password,
        string Confirm
    );
}
