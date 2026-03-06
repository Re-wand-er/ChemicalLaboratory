using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Interfaces;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
{
    public class ReagentRepository : BaseRepository<Reagent>, IReagentRepository
    {
        public ReagentRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }

        public Task AddRangeAsync(IEnumerable<Reagent> reagentDTOs)
        {
            throw new NotImplementedException();
        }
    }
}
