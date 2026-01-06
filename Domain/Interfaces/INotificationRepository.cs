namespace ChemicalLaboratory.Domain.Interfaces
{
    public interface INotificationRepository
    {
        Task AddAsync();
        Task AddRangeAsync();
        Task<bool> UpdateAsync();
        //Task DeleteAsync(PeopleDTO entity);
        Task DeleteAsync(int id);
        Task<List<T>> GetAllAsync<T>();
    }
}
