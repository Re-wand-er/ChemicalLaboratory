using Domain.DTOs;

namespace Infrastructure.Persistence.Repository
{
    interface IEquipmentRepository
    {
        Task<List<EquipmentDTO>> GetAllEquipmentAsync();
        Task AddRangeAsync(IEnumerable<EquipmentDTO> equipmentDTOs);
        Task<bool> UpdateAsync();
        Task DeleteAsync(int id);
        Task DeleteAsync(EquipmentDTO equipment);
    }
}
