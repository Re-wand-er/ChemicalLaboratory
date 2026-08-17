using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        protected readonly DataBaseContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DataBaseContext dataBaseContext)
        {
            _context = dataBaseContext;
            // _context.Set<T> возвращает коллекцию DbSet<T> из _context если она есть
            // если ее нет, то создает новый DbSet<T> с переданным T не являющийся Entity
            // при обращении в бд из такого _dbSet вызовет ошибку 
            _dbSet = _context.Set<T>();
        }

        //public virtual async Task<IEnumerable<T>> GetAllAsync() { return await _dbSet.ToListAsync(); }

        public virtual async Task<IEnumerable<T>> GetAllAsync(bool includeInactive = false)
        {
            IQueryable<T> query = _dbSet;

            if (includeInactive)
                query = query.IgnoreQueryFilters();

            return await query.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null) _dbSet.Remove(entity);
        }

        /// <summary>
        /// Без понятия как работает. Может стать причиной ошибок.
        /// </summary>
        /// <param name="ids">Массив id для удаления</param>
        /// <returns>Ничего не возвращает, а удаляет данные с указанными id</returns>
        public virtual async Task DeleteManyAsync(IEnumerable<int> ids)
        {
            // Здесь находим класс, характерен обобщение T из "скелета" БД. (_context.Model?.FindEntityType(typeof(T))?)
            // Из Fluent API находим наименование его ID и получаем его имя (.FindPrimaryKey()?.Properties[0].Name)
            var primaryKeyName = _context.Model?.FindEntityType(typeof(T))?
                .FindPrimaryKey()?.Properties[0].Name;

            await _dbSet
                .Where(e => ids.Contains(EF.Property<int>(e, primaryKeyName!))) // Берем ID (primaryKeyName) у объекта e
                .ExecuteDeleteAsync(); 
        }

        public virtual async Task SoftDeleteAsync(IEnumerable<int> ids)
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(typeof(T)))
            {
                await _dbSet
                    .Where(e => ids.Contains(e.Id))
                    .IgnoreQueryFilters()
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(e => ((ISoftDeletable)e).IsActive, false)
                        .SetProperty(e => ((ISoftDeletable)e).DeletedAt, DateTime.UtcNow)
                    );
            }
        }

        public virtual async Task RestoreAsync(IEnumerable<int> ids)
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(typeof(T)))
            {
                await _dbSet
                    .Where(e => ids.Contains(e.Id))
                    .IgnoreQueryFilters()
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(e => ((ISoftDeletable)e).IsActive, true)
                        .SetProperty(e => ((ISoftDeletable)e).DeletedAt, (DateTime?)null)
                    );
            }
        }

        public virtual async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
