using Domain.DTOs;

namespace Infrastructure.Persistence.Repository
{
    public class ReagentRepository : IReagentRepository
    {
        public Task<List<ReagentDTO>> GetAllAsync() 
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(List<ReagentDTO> reagentDTOs)
        {
            throw new NotImplementedException();
        }
        public Task<bool> UpdateAsync()
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(ReagentDTO reagent)
        {
            throw new NotImplementedException();
        }
    }
}
