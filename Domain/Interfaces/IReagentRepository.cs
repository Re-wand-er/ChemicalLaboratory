using Domain.DTOs;

namespace Infrastructure.Persistence.Repository
{
    public interface IReagentRepository
    {
        Task<List<ReagentDTO>> GetAllAsync();
        Task AddRangeAsync(List<ReagentDTO> reagentDTOs);
        Task<bool> UpdateAsync();
        Task DeleteAsync(int id);
        Task DeleteAsync(ReagentDTO reagent);
    }
}
