using ChemicalLaboratory.Domain.DTOs;

namespace ChemicalLaboratory.Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(int id);

        Task DeleteManyAsync(IEnumerable<int> ids);

        Task SaveChangesAsync();
    }
}
