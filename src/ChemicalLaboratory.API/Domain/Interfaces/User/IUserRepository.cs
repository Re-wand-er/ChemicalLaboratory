using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.Domain.Entities;

namespace ChemicalLaboratory.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByLoginAsync(string login);

        Task<IEnumerable<ListItemDTO>> GetAllIdNameAsync();
        //new Task<UserDTO?> GetByIdAsync(int id);
        Task<bool> IfExistByEmailAsync(string email);
        Task UpdatePasswordAsync(string email, string password);
    }
}
