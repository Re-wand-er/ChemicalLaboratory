using ChemicalLaboratory.Domain.Entities;

namespace ChemicalLaboratory.Domain.Interfaces
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        Task AddRangeAsync(IEnumerable<Notification> notifications);
    }
}
