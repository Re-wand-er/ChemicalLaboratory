namespace ChemicalLaboratory.Application.UseCases.DTOs
{
    public record class NotificationDTO
    (
        int Id,
        int ReagentId,
        int? UserId,
        string NotificationType,
        string Title,
        string Message,
        DateTime CreatedAt,
        DateTime? DeletedAt,
        bool IsRead = false
    );
}
