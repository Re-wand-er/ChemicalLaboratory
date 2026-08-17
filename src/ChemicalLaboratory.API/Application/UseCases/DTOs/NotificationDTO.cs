namespace ChemicalLaboratory.Application.UseCases.DTOs
{
    public record class NotificationDTO
    (
        int Id,
        int ReagentId,
        string ReagentName,
        int? UserId,
        string UserName,
        string NotificationType,
        string Title,
        string Message,
        string? FilePath,
        DateTime CreatedAt,
        DateTime? DeletedAt,
        bool IsRead = false
    );
}