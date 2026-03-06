using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Interfaces;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
{
    public class ReagentOperationRepository : BaseRepository<ReagentOperation>, IReagentOperationRepository
    {
        public ReagentOperationRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }
    }
}
