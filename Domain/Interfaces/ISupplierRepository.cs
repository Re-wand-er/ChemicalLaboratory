using ChemicalLaboratory.Domain.DTOs;

namespace ChemicalLaboratory.Domain.Interfaces
{
    public interface ISupplierRepository
    {
        Task<List<SuppplierDTO>> GetAllEquipmentAsync();
        Task AddRangeAsync(IEnumerable<SuppplierDTO> equipmentDTOs);
        Task<bool> UpdateAsync();
        Task DeleteAsync(int id);
        Task DeleteAsync(SuppplierDTO equipment);
    }
}
