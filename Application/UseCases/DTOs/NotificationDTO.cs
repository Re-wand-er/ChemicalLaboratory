namespace ChemicalLaboratory.Application.UseCases.DTOs
{
    public record class NotificationDTO
    (
        int Id,
        int ReagentId,
        string ReagentName,
        int? UserId,
        string UserName,
        // string? FirstName,
        // string? MiddleName,
        // string? LastName,
        string NotificationType,
        string Title,
        string Message,
        DateTime CreatedAt,
        DateTime? DeletedAt,
        bool IsRead = false
    );
}