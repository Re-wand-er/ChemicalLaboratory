namespace ChemicalLaboratory.Application.UseCases.DTOs.UserDTOs
{
    public record UserCreateDTO
    (
        int WorkScheduleId,
        int SystemRoleId,
        string FirstName,
        string MiddleName,
        string LastName,
        string Email,
        string? Sex,
        string? JobPosition,
        string? Login,
        string Password
    );
}
