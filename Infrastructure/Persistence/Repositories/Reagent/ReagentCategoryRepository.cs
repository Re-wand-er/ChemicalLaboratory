using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
{
    public class ReagentCategoryRepository : BaseRepository<ReagentCategory>, IReagentCategoryRepository
    {
        public ReagentCategoryRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }

        public async Task<IEnumerable<ListItemDTO>> GetAllIdNameAsync() =>
            await _dbSet
                .AsNoTracking()
                .Select(c => new ListItemDTO(c.Id, c.Name))
                .ToListAsync();

    }
}
