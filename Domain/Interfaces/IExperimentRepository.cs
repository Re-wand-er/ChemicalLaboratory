using Domain.DTOs;

namespace Infrastructure.Persistence.Repository
{
    public interface IExperimentRepository
    {
        Task<ExperimentDTO> AddAsync(ExperimentDTO entity);
        Task<ExperimentDTO> AddRangeAsync(ExperimentDTO entity);
        Task<bool> UpdateAsync(ExperimentDTO entity);
        Task DeleteAsync(int id);
        Task DeleteEquipmentAsync(int id);
        Task DeleteReagentAsync(int id);
        Task<List<ExperimentDTO>> GetAllAsync();

    }
}
