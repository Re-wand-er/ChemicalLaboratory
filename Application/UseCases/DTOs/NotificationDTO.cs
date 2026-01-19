namespace ChemicalLaboratory.Application.UseCases.DTOs
{
    public record class NotificationDTO
    (
        int Id,
        int ReagentId,
        string NotificationType,
        string Message,
        DateTime CreatedAt,
        bool IsRead = false
    );
}
