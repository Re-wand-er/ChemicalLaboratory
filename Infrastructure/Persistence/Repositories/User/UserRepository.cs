using ChemicalLaboratory.Domain.DTOs;
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

        public async Task<IEnumerable<ListItemDTO>> GetAllIdNameAsync() 
            => await _dbSet
                .AsNoTracking()
                .Select(c => new ListItemDTO(c.Id, $"{c.LastName} {c.FirstName} {c.MiddleName}"))
                .ToListAsync();

        //public new async Task<UserDTO?> GetByIdAsync(int id)
        //    => await _dbSet
        //        .Select(u => new UserDTO
        //        {
        //            Id = u.Id,
        //            IdWorkSchedule = u.IdWorkSchedule, 
        //            FirstName = u.FirstName,
        //            MiddleName = u.MiddleName,
        //            LastName = u.LastName,
        //            Email = u.Email,
        //            Sex = u.Sex,
        //            SystemRole = u.SystemRole,
        //            JobPosition = u.JobPosition,
        //            Login = u.Login,
        //            //PasswordHash = u.PasswordHash,
        //            IsActive = u.IsActive
        //        })
        //        .FirstOrDefaultAsync(u => u.Id == id);


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
