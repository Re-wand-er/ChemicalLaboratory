using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.Domain.Entities;

namespace ChemicalLaboratory.Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        //Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(bool includeInactive = false);
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(int id);
        Task DeleteManyAsync(IEnumerable<int> ids);
        Task SoftDeleteAsync(IEnumerable<int> ids);
        Task RestoreAsync(IEnumerable<int> ids);
        Task SaveChangesAsync();
    }
}
