using ChemicalLaboratory.Domain.Interfaces;
using ChemicalLaboratory.Domain.Entities;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
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
