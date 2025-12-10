using ChemicalLaboratory.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ChemicalLaboratory.Domain.ORM
{
    public class ReagentRepository : BaseRepository<ReagentViewModel>
    {
        public ReagentRepository(ORMSQLCommand context) : base(context) { }

        public async Task<IEnumerable<ReagentViewModel>> GetReagent()
        {
            //var reagents = await _context.Reagents.ToListAsync(); // await обязательно

            return await _context.Reagents.ToListAsync();
        }
    }
}
