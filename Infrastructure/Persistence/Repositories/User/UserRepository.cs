using ChemicalLaboratory.Application.Interfaces;
using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }
        
        public virtual async Task<User?> GetByLoginAsync(string login) 
            => await _dbSet.FirstOrDefaultAsync(r=>r.Login == login);

        public virtual async Task<bool> IfExistByEmailAsync(string email)
            => await _dbSet.AnyAsync(u => u.Email == email);

        public virtual async Task UpdatePasswordAsync(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                user.PasswordHash = password; 
                await _context.SaveChangesAsync();
            }
        }
    }
}
