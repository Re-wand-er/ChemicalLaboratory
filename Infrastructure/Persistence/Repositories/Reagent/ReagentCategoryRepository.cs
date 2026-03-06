using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Interfaces;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
{
    public class ReagentCategoryRepository : BaseRepository<ReagentCategory>, IReagentCategoryRepository
    {
        public ReagentCategoryRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }
    }
}
