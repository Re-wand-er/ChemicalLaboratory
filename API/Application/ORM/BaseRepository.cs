using Microsoft.EntityFrameworkCore;

namespace ChemicalLaboratory.Domain.ORM
{
    public class BaseRepository<T> where T : class
    {
        protected readonly ORMSQLCommand _context;
        protected readonly DbSet<T> _dbSet;
        public BaseRepository(ORMSQLCommand sqlCommand)
        {
            _context = sqlCommand;
            _dbSet = sqlCommand.Set<T>();
        }

        public async Task<IEnumerable<T>> GetValuesAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T item)
        {
            _dbSet.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<T?> GetValueById(int id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
