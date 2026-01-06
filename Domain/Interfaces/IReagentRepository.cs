using ChemicalLaboratory.Domain.DTOs;

namespace ChemicalLaboratory.Domain.Interfaces
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
