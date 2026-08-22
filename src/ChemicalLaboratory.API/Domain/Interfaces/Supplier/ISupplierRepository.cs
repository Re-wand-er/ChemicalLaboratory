using ChemicalLaboratory.Domain.Entities;

namespace ChemicalLaboratory.Domain.Interfaces
{
    public interface ISupplierRepository : IBaseRepository<Supplier>
    {
        Task AddRangeAsync(IEnumerable<Supplier> equipmentDTOs);
    }
}
