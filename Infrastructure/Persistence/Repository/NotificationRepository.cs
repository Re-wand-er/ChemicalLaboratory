using ChemicalLaboratory.Domain.Interfaces;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        public Task AddAsync()
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
