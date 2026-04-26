using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.Domain.Interfaces;
using ChemicalLaboratory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Mapster;
using ChemicalLaboratory.Application.UseCases.DTOs;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }

        public override async Task<User?> GetByIdAsync(int id)
        {
            var user = await _dbSet
                .AsNoTracking()
                //.Include(u => u.SystemRole)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public virtual async Task<User?> GetByLoginAsync(string login) 
            => await _dbSet.FirstOrDefaultAsync(r=>r.Login == login);//.Include(u => u.SystemRole)

        public async Task<IEnumerable<ListItemDTO>> GetAllIdNameAsync() 
            => await _dbSet
                .AsNoTracking()
                .Select(c => new ListItemDTO(c.Id, $"{c.LastName} {c.FirstName} {c.MiddleName}"))
                .ToListAsync();


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
