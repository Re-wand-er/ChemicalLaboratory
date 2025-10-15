using EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Services
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<TEntity> DeleteAsync(int id);
    }
}
