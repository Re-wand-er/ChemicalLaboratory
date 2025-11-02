using EFCore.DTOs;

namespace EFCore.Services
{
    internal interface IReagentService
    {
        Task<List<ReagentDTO>> GetAllAsync();
        Task AddRangeAsync(List<ReagentDTO> reagentDTOs);
        Task<bool> UpdateAsync();
        Task DeleteAsync(int id);
        Task DeleteAsync(ReagentDTO reagent);
    }
}
