using ChemicalLaboratory.Domain.Entities;

namespace ChemicalLaboratory.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        //Task<PeopleDTO> GetAllByIdAsync(int id);
        //Task<WorkSchedule> GetAllWorkScheduledAsync();
        Task<User?> GetByLoginAsync(string login);

        Task<bool> IfExistByEmailAsync(string email);
        Task UpdatePasswordAsync(string email, string password);
    }
}
