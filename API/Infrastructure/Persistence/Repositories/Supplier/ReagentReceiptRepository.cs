using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Interfaces;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
{
    public class ReagentReceiptRepository : BaseRepository<ReagentReceipt>, IReagentReceiptRepository
    {
        public ReagentReceiptRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }
    }
}
