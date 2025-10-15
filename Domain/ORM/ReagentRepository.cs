using ChemicalLaboratory.Models.NewModels;
using Microsoft.EntityFrameworkCore;

namespace ChemicalLaboratory.Domain.ORM
{
    public class ReagentRepository : BaseRepository<Reagent>
    {
        public ReagentRepository(ORMSQLCommand context)  : base(context) { }

        public async Task<IEnumerable<Reagent>> GetReagent()
        {
            //var reagents = await _context.Reagents.ToListAsync(); // await обязательно

            return await _context.Reagents.ToListAsync();
        }
    }
}
