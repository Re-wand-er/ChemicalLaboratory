using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.Domain.Entities;

namespace ChemicalLaboratory.Domain.Interfaces
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        Task AddRangeAsync(IEnumerable<Notification> notifications);
        Task<int> GetUnreadCountAsync(int userId);
        Task<List<NotificationSideBarDTO>> GetUserNotificationsAsync(int userId);
        Task MarkAllAsReadAsync(int userId);
        Task MarkAsReadAsync(int id);
        Task SoftDeleteAsync(int id);
    }
}
