namespace ChemicalLaboratory.Application.UseCases.DTOs.UserDTOs
{
    public record UserUpdateDTO
    (
        int Id,
        int IdWorkSchedule,
        string FirstName,
        string MiddleName,
        string LastName,
        string Email,
        string? Sex,
        string? SystemRole,
        string? JobPosition,
        string Login,
        bool IsActive
    );
}
