namespace ChemicalLaboratory.Application.UseCases.DTOs
{
    public record UserReadDTO
    (
        int Id,
        int IdWorkSchedule,
        int SystemRoleId, 
        string FirstName,
        string MiddleName,
        string LastName,
        string Email,
        string? Sex,
        string? SystemRoleName,
        string? JobPosition,
        string Login,
        bool IsActive
    );
}
