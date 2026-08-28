namespace ChemicalLaboratory.Application.UseCases.DTOs
{
    public record UserReadDTO
    (
        int Id,
        int WorkScheduleId,
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

        // string WorkShift,
        // string StartTime,
        // string EndTime
    );
}
