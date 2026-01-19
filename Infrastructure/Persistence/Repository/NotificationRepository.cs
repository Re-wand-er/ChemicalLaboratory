using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Interfaces;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repository
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }

        public Task AddRangeAsync(IEnumerable<Notification> notifications)
        {
            throw new NotImplementedException();
        }
    }
}
